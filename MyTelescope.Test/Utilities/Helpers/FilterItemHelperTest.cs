namespace MyTelescope.Test.Utilities.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Linq.Expressions;
    using Base;
    using Data;
    using MyTelescope.Utilities.Enums;
    using MyTelescope.Utilities.Helpers.Filter;
    using MyTelescope.Utilities.Models.Filter;
    using Xunit;

    public class FilterItemHelperTest : IClassFixture<CustomFixture>
    {
        private static DataModel Model { get; } = new DataModel
        {
            DataString = nameof(DataModel.DataString),
            DataInt = 1,
            DataNullableInt = 1,
            DataDouble = 2.1,
            DataNullableDouble = 2.1,
            DataDateTimeOffset = new DateTimeOffset(new DateTime(2017, 3, 25, 1, 0, 0), TimeSpan.Zero),
            DataNullableDateTimeOffset = new DateTimeOffset(new DateTime(2017, 3, 25, 1, 0, 0), TimeSpan.Zero),
            DataGuid = new Guid("00000000-0000-0000-0000-000000000001"),
            DataNullableGuid = new Guid("00000000-0000-0000-0000-000000000001"),
            DataBool = true,
            DataNullableBool = true,
        };

        [Theory]
        [InlineData(nameof(DataModel.DataString), ColumnType.StringColumn, FilterType.Equal, nameof(DataModel.DataString), "other")]
        [InlineData(nameof(DataModel.DataString), ColumnType.StringColumn, FilterType.Contains, nameof(DataModel.DataString), "other")]

        [InlineData(nameof(DataModel.DataInt), ColumnType.IntColumn, FilterType.Equal, 1, 0)]
        [InlineData(nameof(DataModel.DataInt), ColumnType.IntColumn, FilterType.GreaterOrEqual, 1, 2)]
        [InlineData(nameof(DataModel.DataNullableInt), ColumnType.IntColumn, FilterType.Equal, 1, 0)]

        [InlineData(nameof(DataModel.DataDouble), ColumnType.DoubleColumn, FilterType.Equal, 2.1, 2.0)]
        [InlineData(nameof(DataModel.DataDouble), ColumnType.DoubleColumn, FilterType.GreaterOrEqual, 2.1, 2.2)]
        [InlineData(nameof(DataModel.DataNullableDouble), ColumnType.DoubleColumn, FilterType.Equal, 2.1, 2.0)]

        [InlineData(nameof(DataModel.DataDateTimeOffset), ColumnType.DateTimeOffsetColumn, FilterType.Equal, "2017-3-25 01:00:00 +00:00", "2017-3-25 00:00:00 +00:00")]
        [InlineData(nameof(DataModel.DataDateTimeOffset), ColumnType.DateTimeOffsetColumn, FilterType.GreaterOrEqual, "2017-3-25 01:00:00 +00:00", "2017-3-25 01:00:01 +00:00")]
        [InlineData(nameof(DataModel.DataNullableDateTimeOffset), ColumnType.DateTimeOffsetColumn, FilterType.GreaterOrEqual, "2017-3-25 01:00:00 +00:00", "2017-3-25 01:00:01 +00:00")]

        [InlineData(nameof(DataModel.DataGuid), ColumnType.GuidColumn, FilterType.Equal, "00000000-0000-0000-0000-000000000001", "00000000-0000-0000-0000-000000000002")]
        [InlineData(nameof(DataModel.DataNullableGuid), ColumnType.GuidColumn, FilterType.Equal, "00000000-0000-0000-0000-000000000001", "00000000-0000-0000-0000-000000000002")]

        [InlineData(nameof(DataModel.DataBool), ColumnType.BoolColumn, FilterType.Equal, true, false)]
        [InlineData(nameof(DataModel.DataNullableBool), ColumnType.BoolColumn, FilterType.Equal, true, false)]
        public void GetExpressionStringTest(string columnName, ColumnType columnType, FilterType filterType, object valueTrue, object valueFalse)
        {
            var filterTrue = new FilterItemModel(columnName, columnType, filterType, valueTrue);
            var filterFalse = new FilterItemModel(columnName, columnType, filterType, valueFalse);
            var expressionTrue = FilterItemHelper.ToExpression<DataModel>(filterTrue);
            var expressionFalse = FilterItemHelper.ToExpression<DataModel>(filterFalse);
            var models = new List<DataModel> { Model };

            Assert.Single(models.Where(expressionTrue.Compile()));
            Assert.Empty(models.Where(expressionFalse.Compile()));
        }

        [Fact]
        public void GetCollectionExpressionSingleTest()
        {
            var filter = new FilterItemModel(nameof(DataModel.DataInt), ColumnType.IntColumn, FilterType.In, new List<object> { 1 });
            Expression<Func<DataModel, int>> paramSelector = x => x.DataInt;
            int DataTypeFunc(object x) => (int) x;
            var expression = filter.CollectionExpression(paramSelector, DataTypeFunc);

            Assert.Null(expression);
        }

        [Fact]
        public void GetCollectionExpressionCollectionTest()
        {
            var filter = new FilterItemModel(nameof(DataModel.DataInt), ColumnType.IntColumn, new List<object> { 1, 2, 3 });
            Expression<Func<DataModel, int>> paramSelector = x => x.DataInt;
            int DataTypeFunc(object x) => (int)x;
            var expression = filter.CollectionExpression(paramSelector, DataTypeFunc);

            Assert.NotNull(expression);

            var models = new List<DataModel> { Model };
            Assert.Single(models.Where(expression.Compile()));
        }

        [Fact]
        public void GetCollectionExpressionThrowsTest()
        {
            Expression<Func<DataModel, int>> paramSelector = x => x.DataInt;
            int DataTypeFunc(object x) => (int)x;

            var filter = new FilterItemModel(nameof(DataModel.DataInt), ColumnType.IntColumn, new List<object>());
            Assert.Throws<ArgumentException>(() => filter.CollectionExpression(paramSelector, DataTypeFunc));

            filter = new FilterItemModel(nameof(DataModel.DataInt), ColumnType.IntColumn, FilterType.In, 2);
            Assert.Throws<ArgumentException>(() => filter.CollectionExpression(paramSelector, DataTypeFunc));
        }
    }
}