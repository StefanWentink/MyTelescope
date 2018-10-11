namespace MyTelescope.App.Utilities.EventArgs
{
    using System;

    public class EndOfCollectionEventArgs : EventArgs
    {
        public EndOfCollectionEventArgs(int count)
        {
            Count = count;
        }

        public int Count { get; }
    }
}