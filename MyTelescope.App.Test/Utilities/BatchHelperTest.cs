namespace MyTelescope.App.Test.Utilities
{
    using System.Linq;
    using App.Utilities.Constants;
    using App.Utilities.Helpers;
    using MyTelescope.Test.Base;
    using Xunit;
    using CustomFixture = Base.CustomFixture;

    public class BatchHelperTest : IClassFixture<CustomFixture>
    {
        [Theory]
        [InlineData(0, BatchConstants.InitialBatchSize, BatchConstants.InitialStepSize, BatchConstants.StepSize)]
        [InlineData(1, BatchConstants.BatchSize, BatchConstants.StepSize, BatchConstants.StepSize)]
        public void GetSortModelsTest(int value, int expectedBatchSize, int expectedFirstStepSize, int expectedNextStepSize)
        {
            var actualList = BatchHelper.GetSortModels(value).ToList();
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
        [InlineData(-10, 0, BatchConstants.InitialStepSize)]
        [InlineData(0, 0, BatchConstants.InitialStepSize)]
        [InlineData(10, 10, BatchConstants.StepSize)]
        public void GetSortModel(int value, int expectedSkip, int expectedTake)
        {
            var actual = BatchHelper.GetSortModel(value);
            Assert.Equal(expectedSkip, actual.Skip);
            Assert.Equal(expectedTake, actual.Take);
            Assert.Equal(expectedSkip + expectedTake, actual.RecordRequestNumber);
        }
    }
}
