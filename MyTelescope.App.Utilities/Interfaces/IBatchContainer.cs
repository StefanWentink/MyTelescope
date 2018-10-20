namespace MyTelescope.App.Utilities.Interfaces
{
    public interface IBatchContainer
    {
        int MaxBatchSize { get; }

        int GetBatchSize();

        int GetStepSize();

        int GetInitialBatchSize();

        int GetInitialStepSize();
    }
}