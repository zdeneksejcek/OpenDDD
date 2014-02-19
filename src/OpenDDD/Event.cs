﻿using System;

namespace OpenDDD
{
    public abstract class Event : AssertionConcern
    {
        public DateTime OccuredOn { get; private set; }
        protected Event()
        {
            OccuredOn = DateTime.UtcNow;
        }
    }
}