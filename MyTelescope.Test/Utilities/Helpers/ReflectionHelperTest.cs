namespace MyTelescope.Test.Utilities.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Data;
    using MyTelescope.Utilities.Helpers.Reflection;
    using MyTelescope.Utilities.Models;
    using Xunit;

    public class ReflectionHelperTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void ReflectionHelperMemberSelectorTest()
        {
            double expected = 1.23;
            var model = new DegreeModel(expected);

            var memberSelectorTyped = ReflectionHelper.MemberSelector<DegreeModel, double>(nameof(DegreeModel.Degrees));
            var func = memberSelectorTyped.Compile();
            var actual = func.Invoke(model);
            Assert.Equal(expected, actual);

            var memberSelectorObject = ReflectionHelper.MemberSelector<DegreeModel>(nameof(DegreeModel.Degrees));
            actual = (double) memberSelectorObject.Compile().Invoke(model);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReflectionHelperSortTest()
        {
            var expectedSmall = 1.23;
            var expectedMedium = 2.34;
            var expectedLarge = 3.45;

            var list = new List<DegreeModel>
            {
                new DegreeModel(expectedSmall),
                new DegreeModel(expectedLarge),
                new DegreeModel(expectedMedium)
            };

            double Func(DegreeModel x) => x.Degrees;
            var memberSelectorTyped = ReflectionHelper.MemberSelector<DegreeModel, double>(nameof(DegreeModel.Degrees));
            var memberSelectorObject = ReflectionHelper.MemberSelector<DegreeModel>(nameof(DegreeModel.Degrees));

            var funcList = list.AsQueryable().AsEnumerable().OrderByDescending(Func).ToList();
            var memberSelectorTypedList = list.AsQueryable().OrderByDescending(memberSelectorTyped).ToList();
            var memberSelectorObjectList = list.AsQueryable().OrderByDescending(memberSelectorObject).ToList();

            Assert.Equal(expectedLarge, funcList.First().Degrees);
            Assert.Equal(expectedLarge, memberSelectorTypedList.First().Degrees);
            Assert.Equal(expectedLarge, memberSelectorObjectList.First().Degrees);

            Assert.Equal(expectedSmall, funcList.Last().Degrees);
            Assert.Equal(expectedSmall, memberSelectorTypedList.Last().Degrees);
            Assert.Equal(expectedSmall, memberSelectorObjectList.Last().Degrees);
        }

        [Theory]
        [InlineData(nameof(RefelectionNullablePropertyModel.NullableString), true)]
        [InlineData(nameof(RefelectionNullablePropertyModel.NonNullableDateTimeOffset), false)]
        [InlineData(nameof(RefelectionNullablePropertyModel.NonNullableGuid), false)]
        [InlineData(nameof(RefelectionNullablePropertyModel.NonNullableInt), false)]
        [InlineData(nameof(RefelectionNullablePropertyModel.NullableDateTimeOffset), true)]
        [InlineData(nameof(RefelectionNullablePropertyModel.NullableGuid), true)]
        [InlineData(nameof(RefelectionNullablePropertyModel.NullableInt), true)]
        public void ReflectionHelperIsNullableTest(string propertyName, bool expectedNullable)
        {
            var actual = ReflectionHelper.IsNullable<RefelectionNullablePropertyModel>(propertyName);
            Assert.Equal(expectedNullable, actual);
        }
    }
}
