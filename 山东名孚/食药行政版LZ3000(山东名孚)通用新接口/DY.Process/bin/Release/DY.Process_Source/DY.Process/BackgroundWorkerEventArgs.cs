namespace DY.Process
{
    using System;
    using System.Runtime.CompilerServices;

    public class BackgroundWorkerEventArgs : EventArgs
    {
        public Exception BackGroundException { get; set; }
    }
}

