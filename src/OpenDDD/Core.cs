using System;
using System.Linq;
using System.Reflection;
using OpenDDD.Attributes;
using OpenDDD.Reflection;
using OpenDDD.RemoteQueue;

namespace OpenDDD
{
    public class Core
    {
        private ITypeInstantiator _instantiator;
        private Type[] _applicationServices;
        private EventProcessor _eventProcessor;

        private static Core _current;

        internal static Core Current
        {
            get
            {
                if (_current == null)
                    throw new Exceptions.CoreNotInitialized();

                return _current;
            }
            set { _current = value; }
        }

        public Core(
            IDomainAssemblyProvider domainAssembliesProvider,
            ITypeInstantiator instantiator,
            IHandlerDecisionMaker handlerDecisionMaker,
            IRemoteQueue messageQueue)
        {
            Current = this;
            _instantiator = instantiator;

            _applicationServices = InterfacesFinder.FindCommandHandlers(domainAssembliesProvider.GetDomainAssemblies());
            var eventHandlers = EventHandlerFinder.FindEventHandlers(domainAssembliesProvider.GetDomainAssemblies());

            _eventProcessor = new EventProcessor(messageQueue, eventHandlers, handlerDecisionMaker, _instantiator);

            var dependencies =
                InterfacesFinder.FindExternalDependencies(domainAssembliesProvider.GetDomainAssemblies());

            ValidateRequiredImplementations(_applicationServices, dependencies);
        }

        private void ValidateRequiredImplementations(Type[] serviceTypes, Type[] externalDependenciesInterfaces)
        {
            serviceTypes.Select(type => _instantiator.Instantiate(type)).ToArray();

            externalDependenciesInterfaces.Select(type => _instantiator.Instantiate(type)).ToArray();
        }

        public TResult ExecuteWithResult<TCommand,TResult>(TCommand command)
        {
            var method = new MethodFinder(_applicationServices).FindByCommandAndReturnType<TCommand, TResult>();
            var serviceType = _instantiator.Instantiate(method.DeclaringType);

            try
            {
                Func<TResult> methodToBeExecuted = () => (TResult) method.Invoke(serviceType, new object[] {command});

                if (ContainsUnitOfWork(method))
                    return RunInsideUnitOfWork(methodToBeExecuted);

                return methodToBeExecuted();
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;

                throw ex;
            }
        }

        public void Execute<TCommand>(TCommand command)
        {
            var method = new MethodFinder(_applicationServices).FindByCommand<TCommand>();
            var serviceType = _instantiator.Instantiate(method.DeclaringType);

            try
            {
                Action methodToBeExecuted = () => method.Invoke(serviceType, new object[] {command});

                if (ContainsUnitOfWork(method))
                    RunInsideUnitOfWork(methodToBeExecuted);
                else
                    methodToBeExecuted();
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;

                throw ex;
            }
        }

        public void ExecuteEvent(Event @event)
        {
            _eventProcessor.Process(@event);
        }

        public TService Resolve<TService>()
        {
            return _instantiator.Instantiate<TService>();
        }

        private static TResult RunInsideUnitOfWork<TResult>(Func<TResult> func)
        {
            TResult result;
            using (var uow = new UnitOfWork())
            {
                result = func.Invoke();
                uow.Commit();
            }
            return result;
        }

        private static void RunInsideUnitOfWork(Action action)
        {
            using (var uow = new UnitOfWork())
            {
                action();

                uow.Commit();
            }
        }

        private static bool ContainsUnitOfWork(MethodInfo method)
        {
            var uowAttribute = method.GetCustomAttribute<UnitOfWorkAttribute>();

            return uowAttribute != null;
        }

    }
}
