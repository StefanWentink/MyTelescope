namespace MyTelescope.App.Utilities.Models
{
    using MyTelescope.App.Utilities.Interfaces;
    using SWE.BasicType.Utilities;

    public class BatchContainer : IBatchContainer
    {
        public int MaxBatchSize { get; }
        private int BatchSize { get; }
        private int StepSize { get; }
        private int InitialBatchSize { get; }
        private int InitialStepSize { get; }

        public BatchContainer()
            : this(100, 20, 10, 30, 20)
        {
        }

        public BatchContainer(
            int maxBatchSize,
            int batchSize,
            int stepSize,
            int initialBatchSize,
            int initialStepSize)
        {
            MaxBatchSize = maxBatchSize;
            BatchSize = batchSize;
            StepSize = stepSize;
            InitialBatchSize = initialBatchSize;
            InitialStepSize = initialStepSize;
        }

        public int GetBatchSize()
        {
            return CompareUtilities.Min(BatchSize, MaxBatchSize);
        }

        public int GetStepSize()
        {
            return CompareUtilities.Min(StepSize, MaxBatchSize);
        }

        public int GetInitialBatchSize()
        {
            return CompareUtilities.Min(InitialBatchSize, MaxBatchSize);
        }

        public int GetInitialStepSize()
        {
            return CompareUtilities.Min(InitialStepSize, MaxBatchSize);
        }
    }
}