namespace MyTelescope.App.Utilities.Helpers
{
    using MyTelescope.App.Utilities.Interfaces;
    using MyTelescope.Utilities.Models.Sort;
    using System.Collections.Generic;

    public static class BatchHelper
    {
        public static IEnumerable<SortModel> GetSortModels(this IBatchContainer batchContainer, int requestedRecordCount, bool batched)
        {
            var batchSize =
                batched
                    ? requestedRecordCount == default(int)
                        ? batchContainer.GetInitialBatchSize()
                        : batchContainer.GetBatchSize()
                    : batchContainer.MaxBatchSize;

            var toRequestedRecordCount = requestedRecordCount + batchSize;

            var loopRequestedRecordCount = requestedRecordCount;

            while (loopRequestedRecordCount < toRequestedRecordCount)
            {
                var result = batchContainer.GetSortModel(loopRequestedRecordCount, batched);
                loopRequestedRecordCount = result.RecordRequestNumber;
                yield return result;
            }
        }

        public static SortModel GetSortModel(this IBatchContainer batchContainer, int requestedRecordCount, bool batched)
        {
            var skip = GetSkip(requestedRecordCount);
            var take = batched
                ? batchContainer.GetTake(requestedRecordCount)
                : batchContainer.MaxBatchSize;
            return new SortModel(skip, take);
        }

        internal static int GetSkip(int skip)
        {
            if (skip <= default(int))
            {
                return default(int);
            }

            return skip;
        }

        internal static int GetTake(this IBatchContainer batchContainer, int skip)
        {
            if (skip <= default(int))
            {
                return batchContainer.GetInitialStepSize();
            }

            return batchContainer.GetStepSize();
        }
    }
}