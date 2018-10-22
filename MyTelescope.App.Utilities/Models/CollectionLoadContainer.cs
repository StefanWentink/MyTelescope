﻿namespace MyTelescope.App.Utilities.Models
{
    public class CollectionLoadContainer<TViewModel> : CollectionContainer<TViewModel>
        where TViewModel : class
    {
        private bool _endOfCollection;

        private readonly object _endOfCollectionLock = new object();

        private bool _endOfCollectionCommunicated;

        private readonly object _endOfCollectionCommunicatedLock = new object();

        private int _recordRequestNumber;

        private readonly object _recordRequestNumberLock = new object();

        private int _runningTaskCount;

        private readonly object _runningTaskCountLock = new object();

        public CollectionLoadContainer()
        {
            ClearCollection();
        }

        public void Reset()
        {
            ClearCollection();
            SetEndOfCollection(false);
            SetEndOfCollectionCommunicated(false);
            SetRecordRequestNumber(0);
        }

        protected void SetEndOfCollection(bool endOfCollection)
        {
            lock (_endOfCollectionLock)
            {
                _endOfCollection = endOfCollection;
            }
        }

        public void SetEndOfCollection()
        {
            SetEndOfCollection(true);
        }

        public bool GetEndOfCollection()
        {
            lock (_endOfCollectionLock)
            {
                return _endOfCollection;
            }
        }

        public bool GetEndOfCollectionWithNoRunningTasks()
        {
            return GetEndOfCollection() && NoRunningTask();
        }

        public bool GetEndOfCollectionWithNoRunningTasksAndEndOfCollectionNotCommunicated()
        {
            return GetEndOfCollectionWithNoRunningTasks() && !GetEndOfCollectionCommunicated();
        }

        protected void SetEndOfCollectionCommunicated(bool endOfCollectionCommunicated)
        {
            lock (_endOfCollectionCommunicatedLock)
            {
                _endOfCollectionCommunicated = endOfCollectionCommunicated;
            }
        }

        public void SetEndOfCollectionCommunicated()
        {
            SetEndOfCollectionCommunicated(true);
        }

        public bool GetEndOfCollectionCommunicated()
        {
            lock (_endOfCollectionCommunicatedLock)
            {
                return _endOfCollectionCommunicated;
            }
        }

        protected void SetRecordRequestNumber(int recordRequestNumber)
        {
            lock (_recordRequestNumberLock)
            {
                _recordRequestNumber = recordRequestNumber;
            }
        }

        public void AddRecordRequestNumber(int recordRequestNumber)
        {
            lock (_recordRequestNumberLock)
            {
                if (recordRequestNumber == int.MaxValue)
                {
                    _recordRequestNumber = int.MaxValue;
                    SetEndOfCollection();
                    return;
                }

                _recordRequestNumber += recordRequestNumber;
            }
        }

        public int GetRecordRequestNumber()
        {
            lock (_recordRequestNumberLock)
            {
                return _recordRequestNumber;
            }
        }

        protected bool NoRunningTask()
        {
            lock (_runningTaskCountLock)
            {
                return _runningTaskCount == 0;
            }
        }

        public void AddRunningTask()
        {
            lock (_runningTaskCountLock)
            {
                _runningTaskCount++;
            }
        }

        public int RunningTaskCount()
        {
            lock (_runningTaskCountLock)
            {
                return _runningTaskCount;
            }
        }

        public void RemoveRunningTask()
        {
            lock (_runningTaskCountLock)
            {
                if (_runningTaskCount > 0)
                {
                    _runningTaskCount--;
                }
            }
        }
    }
}