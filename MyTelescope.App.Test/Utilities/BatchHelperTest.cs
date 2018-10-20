namespace MyTelescope.App.Test.Utilities
{
    using App.Utilities.Helpers;
    using MyTelescope.App.Utilities.Models;
    using System.Linq;
    using Xunit;
    using CustomFixture = Base.CustomFixture;

    public class BatchHelperTest : IClassFixture<CustomFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void GetSortModelsTest(int value)
        {
            var batchContainer = new BatchContainer();

            int expectedBatchSize = value == 0 ? batchContainer.GetInitialBatchSize() : batchContainer.GetBatchSize();
            int expectedFirstStepSize = value == 0 ? batchContainer.GetInitialStepSize() : batchContainer.GetStepSize();
            int expectedNextStepSize = batchContainer.GetStepSize();

            var actualList = batchContainer.GetSortModels(value, true).ToList();
            Assert.Equal(2, actualList.Count);
            var actualBatchSize = actualList.Max(x => x.RecordRequestNumber) - actualList.Min(x => x.Skip);
            Assert.Equal(expectedBatchSize, actualBatchSize);

            var actualFirstBatch = actualList.OrderBy(x => x.Skip).First();
            Assert.Equal(value, actualFirstBatch.Skip);
            Assert.Equal(expectedFirstStepSize, actualFirstBatch.Take);

            var actualLastBatch = actualList.OrderByDescending(x => x.Skip).First();
            Assert.Equal(value + actualFirstBatch.Take, actualLastBatch.Skip);
            Assert.Equal(expectedNextStepSize, actualLastBatch.Take);
        }

        [Theory]
        [InlineData(-10, 0, 20, true)]
        [InlineData(0, 0, 20, true)]
        [InlineData(10, 10, 10, true)]
        [InlineData(-10, 0, 100, false)]
        [InlineData(0, 0, 100, false)]
        [InlineData(10, 10, 100, false)]
        public void GetSortModel(int value, int expectedSkip, int expectedTake, bool batched)
        {
            var batchContainer = new BatchContainer();
            var actual = batchContainer.GetSortModel(value, batched);
            Assert.Equal(expectedSkip, actual.Skip);
            Assert.Equal(expectedTake, actual.Take);
            Assert.Equal(expectedSkip + expectedTake, actual.RecordRequestNumber);
        }
    }
}