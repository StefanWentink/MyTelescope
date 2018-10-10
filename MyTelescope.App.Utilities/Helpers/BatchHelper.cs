namespace MyTelescope.App.Utilities.Helpers
{
    using System.Collections.Generic;
    using Constants;
    using MyTelescope.Utilities.Models.Sort;

    public static class BatchHelper
    {
        public static IEnumerable<SortModel> GetSortModels(int requestedRecordCount)
        {
            var batchSize = 
                requestedRecordCount == default(int) 
                ? BatchConstants.InitialBatchSize 
                : BatchConstants.BatchSize;

            var toRequestedRecordCount = requestedRecordCount + batchSize;

            var loopRequestedRecordCount = requestedRecordCount;

            while (loopRequestedRecordCount < toRequestedRecordCount)
            {
                var result = GetSortModel(loopRequestedRecordCount);
                loopRequestedRecordCount = result.RecordRequestNumber;
                yield return result;
            }
        }

        public static SortModel GetSortModel(int requestedRecordCount)
        {
            var skip = GetSkip(requestedRecordCount);
            var take = GetTake(requestedRecordCount);
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

        internal static int GetTake(int skip)
        {
            if (skip <= default(int))
            {
                return BatchConstants.InitialStepSize;
            }

            return BatchConstants.StepSize;
        }
    }
}
