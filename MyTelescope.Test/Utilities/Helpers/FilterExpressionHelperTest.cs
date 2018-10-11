namespace MyTelescope.Test.Utilities.Helpers
{
    using Base;
    using MyTelescope.Utilities.Enums;
    using MyTelescope.Utilities.Helpers.Filter;
    using System;
    using System.Linq.Expressions;
    using Xunit;

    public class FilterExpressionHelperTest : IClassFixture<CustomFixture>
    {
        [Theory]
        [InlineData(FilterType.Equal, "qwe", "qwe", "qqwe")]
        [InlineData(FilterType.NotEqual, "qwe", "qqwe", "qwe")]
        [InlineData(FilterType.Contains, "qwe", "qqwee", "abcde")]
        [InlineData(FilterType.NotContains, "qwe", "abcde", "qqwee")]
        public void GetExpressionStringTest(FilterType filter, string value, string valueTrue, string valueFalse)
        {
            AssertGetExpressionTest(filter, value, valueTrue, valueFalse, FilterExpressionHelper.GetExpression);
        }

        [Theory]
        [InlineData(FilterType.GreaterOrEqual)]
        [InlineData(FilterType.LessOrEqual)]
        [InlineData(FilterType.In)]
        public void GetExpressionStringThrows(FilterType filter)
        {
            AssertGetExpressionThrowsTest<string>(filter, FilterExpressionHelper.GetExpression);
        }

        [Theory]
        [InlineData(FilterType.Equal, 12, 12, 13)]
        [InlineData(FilterType.NotEqual, -12, -13, -12)]
        [InlineData(FilterType.GreaterOrEqual, -12, -11, -13)]
        [InlineData(FilterType.LessOrEqual, 12, 11, 13)]
        public void GetExpressionIntTest(FilterType filter, int value, int valueTrue, int valueFalse)
        {
            AssertGetExpressionTest(filter, value, valueTrue, valueFalse, FilterExpressionHelper.GetExpression);
            AssertGetExpressionTest(filter, value, valueTrue, valueFalse, FilterExpressionHelper.GetNullableExpression);
        }

        [Theory]
        [InlineData(FilterType.Contains)]
        [InlineData(FilterType.NotContains)]
        [InlineData(FilterType.In)]
        public void GetExpressionIntThrows(FilterType filter)
        {
            AssertGetExpressionThrowsTest<int>(filter, FilterExpressionHelper.GetExpression);
            AssertGetNullableExpressionThrowsTest<int, int?>(filter, FilterExpressionHelper.GetNullableExpression);
        }

        [Theory]
        [InlineData(FilterType.Equal, 12.6, 12.6, 13.6)]
        [InlineData(FilterType.NotEqual, -12.6, -13.6, -12.6)]
        [InlineData(FilterType.GreaterOrEqual, -12.6, -11.6, -13.6)]
        [InlineData(FilterType.LessOrEqual, 12.6, 11.6, 13.6)]
        public void GetExpressionDoubleTest(FilterType filter, double value, double valueTrue, double valueFalse)
        {
            AssertGetExpressionTest(filter, value, valueTrue, valueFalse, FilterExpressionHelper.GetExpression);
            AssertGetExpressionTest(filter, value, valueTrue, valueFalse, FilterExpressionHelper.GetNullableExpression);
        }

        [Theory]
        [InlineData(FilterType.Contains)]
        [InlineData(FilterType.NotContains)]
        [InlineData(FilterType.In)]
        public void GetExpressionDoubleThrows(FilterType filter)
        {
            AssertGetExpressionThrowsTest<double>(filter, FilterExpressionHelper.GetExpression);
            AssertGetNullableExpressionThrowsTest<double, double?>(filter, FilterExpressionHelper.GetNullableExpression);
        }

        [Theory]
        [InlineData(FilterType.Equal, 12, 12, 13)]
        [InlineData(FilterType.NotEqual, -12, -13, -12)]
        [InlineData(FilterType.GreaterOrEqual, -12, -11, -13)]
        [InlineData(FilterType.LessOrEqual, 12, 11, 13)]
        public void GetExpressionDateTimeOffsetTest(FilterType filter, int value, int valueTrue, int valueFalse)
        {
            var dateTimeOffset = new DateTimeOffset(new DateTime(2018, 3, 25, 0, 59, 59));
            var dateTimeOffsetValue = dateTimeOffset.AddSeconds(value);
            var dateTimeOffsetTrue = dateTimeOffset.AddSeconds(valueTrue);
            var dateTimeOffsetFalse = dateTimeOffset.AddSeconds(valueFalse);

            AssertGetExpressionTest(filter, dateTimeOffsetValue, dateTimeOffsetTrue, dateTimeOffsetFalse, FilterExpressionHelper.GetExpression);
            AssertGetExpressionTest(filter, dateTimeOffsetValue, dateTimeOffsetTrue, dateTimeOffsetFalse, FilterExpressionHelper.GetNullableExpression);
        }

        [Theory]
        [InlineData(FilterType.Contains)]
        [InlineData(FilterType.NotContains)]
        [InlineData(FilterType.In)]
        public void GetExpressionDateTimeOffsetThrows(FilterType filter)
        {
            AssertGetExpressionThrowsTest<DateTimeOffset>(filter, FilterExpressionHelper.GetExpression);
            AssertGetNullableExpressionThrowsTest<DateTimeOffset, DateTimeOffset?>(filter, FilterExpressionHelper.GetNullableExpression);
        }

        [Theory]
        [InlineData(FilterType.Equal, "8d437b6a-50a5-415f-9ba2-9d6d18e2d49f", "8d437b6a-50a5-415f-9ba2-9d6d18e2d49f", "33e9e2a5-0cdc-4578-b18e-fc4dfa43a342")]
        [InlineData(FilterType.NotEqual, "8d437b6a-50a5-415f-9ba2-9d6d18e2d49f", "33e9e2a5-0cdc-4578-b18e-fc4dfa43a342", "8d437b6a-50a5-415f-9ba2-9d6d18e2d49f")]
        public void GetExpressionGuidTest(FilterType filter, string value, string valueTrue, string valueFalse)
        {
            var guidValue = new Guid(value);
            var guidTrue = new Guid(valueTrue);
            var guidFalse = new Guid(valueFalse);

            AssertGetExpressionTest(filter, guidValue, guidTrue, guidFalse, FilterExpressionHelper.GetExpression);
            AssertGetExpressionTest(filter, guidValue, guidTrue, guidFalse, FilterExpressionHelper.GetNullableExpression);
        }

        [Theory]
        [InlineData(FilterType.Contains)]
        [InlineData(FilterType.NotContains)]
        [InlineData(FilterType.GreaterOrEqual)]
        [InlineData(FilterType.LessOrEqual)]
        [InlineData(FilterType.In)]
        public void GetExpressionGuidThrows(FilterType filter)
        {
            AssertGetExpressionThrowsTest<Guid>(filter, FilterExpressionHelper.GetExpression);
            AssertGetNullableExpressionThrowsTest<Guid, Guid?>(filter, FilterExpressionHelper.GetNullableExpression);
        }

        [Theory]
        [InlineData(FilterType.Equal, true, true, false)]
        [InlineData(FilterType.NotEqual, false, true, false)]
        public void GetExpressionBoolTest(FilterType filter, bool value, bool valueTrue, bool valueFalse)
        {
            AssertGetExpressionTest(filter, value, valueTrue, valueFalse, FilterExpressionHelper.GetExpression);
            AssertGetExpressionTest(filter, value, valueTrue, valueFalse, FilterExpressionHelper.GetNullableExpression);
        }

        [Theory]
        [InlineData(FilterType.Contains)]
        [InlineData(FilterType.NotContains)]
        [InlineData(FilterType.GreaterOrEqual)]
        [InlineData(FilterType.LessOrEqual)]
        [InlineData(FilterType.In)]
        public void GetExpressionBoolThrows(FilterType filter)
        {
            AssertGetExpressionThrowsTest<bool>(filter, FilterExpressionHelper.GetExpression);
            AssertGetNullableExpressionThrowsTest<bool, bool?>(filter, FilterExpressionHelper.GetNullableExpression);
        }

        private void AssertGetExpressionTest<T, TNullable>(FilterType filter, T value, TNullable valueTrue, TNullable valueFalse, Func<FilterType, T, Expression<Func<TNullable, bool>>> expressionFunc)
        {
            var expression = expressionFunc(filter, value);
            var actualTrue = expression.Compile().Invoke(valueTrue);
            var actualFalse = expression.Compile().Invoke(valueFalse);
            Assert.True(actualTrue);
            Assert.False(actualFalse);
        }

        private void AssertGetExpressionThrowsTest<T>(FilterType filter, Func<FilterType, T, Expression<Func<T, bool>>> expressionFunc)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => expressionFunc(filter, default(T)));
        }

        private void AssertGetNullableExpressionThrowsTest<T, TNullable>(FilterType filter, Func<FilterType, T, Expression<Func<TNullable, bool>>> expressionFunc)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => expressionFunc(filter, default(T)));
        }
    }
}