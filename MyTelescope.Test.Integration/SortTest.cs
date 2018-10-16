namespace MyTelescope.Test.Integration
{
    using Api.DataLayer.Context;
    using App.Test.Base;
    using Core.Utilities.Helpers;
    using Ef.Utilities.Helpers;
    using MyTelescope.SolarSystem.Models.CelestialObject;
    using MyTelescope.Utilities.Helpers.Reflection;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using Xunit;

    public class SortTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void SortFunctionTest()
        {
            var query = new MyTelescopeContext().GetNoTrackingQuery<CelestialObjectPosition>();

            double Func(CelestialObjectPosition x) => x.Declination;
            Expression<Func<CelestialObjectPosition, object>> expression = x => x.Declination;
            Expression<Func<CelestialObjectPosition, object>> memberSelector = ReflectionHelper.MemberSelector<CelestialObjectPosition>("Declination");

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var orderedExpression = query.OrderByDescending(expression);
            LogHelper.LogInformation($"Ordered expression.order {stopwatch.ElapsedMilliseconds}.");
            var takeExpression = orderedExpression.Skip(2000).Take(200);
            LogHelper.LogInformation($"Ordered expression.take {stopwatch.ElapsedMilliseconds}.");
            var resultExpression = takeExpression.ToList();
            LogHelper.LogInformation($"Ordered expression.result {stopwatch.ElapsedMilliseconds}.");
            stopwatch.Stop();
            var elapsedExpression = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            var orderedMemberSelector = query.OrderByDescending(memberSelector);
            LogHelper.LogInformation($"Ordered memberSelector.order {stopwatch.ElapsedMilliseconds}.");
            var takeMemberSelector = orderedMemberSelector.Skip(2000).Take(200);
            LogHelper.LogInformation($"Ordered memberSelector.take {stopwatch.ElapsedMilliseconds}.");
            var resultMemberSelector = takeMemberSelector.ToList();
            LogHelper.LogInformation($"Ordered memberSelector.result {stopwatch.ElapsedMilliseconds}.");
            stopwatch.Stop();
            var elapsedMemberSelector = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            var orderedFunc = query.AsEnumerable().OrderByDescending(Func);
            LogHelper.LogInformation($"Ordered func.order {stopwatch.ElapsedMilliseconds}.");
            var takeFunc = orderedFunc.Skip(2000).Take(200);
            LogHelper.LogInformation($"Ordered func.take {stopwatch.ElapsedMilliseconds}.");
            var resultFunc = takeFunc.ToList();
            LogHelper.LogInformation($"Ordered func.result {stopwatch.ElapsedMilliseconds}.");
            stopwatch.Stop();
            var elapsedFunc = stopwatch.ElapsedMilliseconds;

            Assert.Equal(resultFunc.Count, resultExpression.Count);
            Assert.Equal(resultFunc[0].Declination, resultExpression[0].Declination);
            Assert.Equal(resultFunc.Last().Declination, resultExpression.Last().Declination);

            Assert.Equal(resultFunc[0].Declination, resultMemberSelector[0].Declination);
            Assert.Equal(resultFunc.Last().Declination, resultMemberSelector.Last().Declination);

            Assert.True(elapsedFunc > elapsedExpression);
            Assert.True(elapsedFunc > elapsedMemberSelector);
        }

        [Fact]
        public void SortFunctionMemoryTest()
        {
            var query = new MyTelescopeContext().GetNoTrackingQuery<CelestialObjectPosition>();

            double Func(CelestialObjectPosition x) => x.Declination;
            Expression<Func<CelestialObjectPosition, object>> expressionObject = x => x.Declination;
            Expression<Func<CelestialObjectPosition, double>> expressionTyped = x => x.Declination;
            Expression<Func<CelestialObjectPosition, object>> memberSelector = ReflectionHelper.MemberSelector<CelestialObjectPosition>("Declination");

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var ordered = query.OrderByDescending(x => x.Declination);
            LogHelper.LogInformation($"Ordered memberSelector.order {stopwatch.ElapsedMilliseconds}.");
            var take = ordered.Skip(2000).Take(200);
            LogHelper.LogInformation($"Ordered memberSelector.take {stopwatch.ElapsedMilliseconds}.");
            var result = take.ToList();
            LogHelper.LogInformation($"Ordered memberSelector.result {stopwatch.ElapsedMilliseconds}.");
            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            var orderedMemberSelector = query.OrderByDescending(memberSelector);
            LogHelper.LogInformation($"Ordered memberSelector.order {stopwatch.ElapsedMilliseconds}.");
            var takeMemberSelector = orderedMemberSelector.Skip(2001).Take(200);
            LogHelper.LogInformation($"Ordered memberSelector.take {stopwatch.ElapsedMilliseconds}.");
            var resultMemberSelector = takeMemberSelector.ToList();
            LogHelper.LogInformation($"Ordered memberSelector.result {stopwatch.ElapsedMilliseconds}.");
            stopwatch.Stop();
            var elapsedMemberSelector = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            var orderedExpressionObject = query.OrderByDescending(expressionObject);
            LogHelper.LogInformation($"Ordered expression.order {stopwatch.ElapsedMilliseconds}.");
            var takeExpressionObject = orderedExpressionObject.Skip(2002).Take(200);
            LogHelper.LogInformation($"Ordered expression.take {stopwatch.ElapsedMilliseconds}.");
            var resultExpressionObject = takeExpressionObject.ToList();
            LogHelper.LogInformation($"Ordered expression.result {stopwatch.ElapsedMilliseconds}.");
            stopwatch.Stop();
            var elapsedExpressionObject = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            var orderedExpressionTyped = query.OrderByDescending(expressionTyped);
            LogHelper.LogInformation($"Ordered expression.order {stopwatch.ElapsedMilliseconds}.");
            var takeExpressionTyped = orderedExpressionTyped.Skip(2003).Take(200);
            LogHelper.LogInformation($"Ordered expression.take {stopwatch.ElapsedMilliseconds}.");
            var resultExpressionTyped = takeExpressionTyped.ToList();
            LogHelper.LogInformation($"Ordered expression.result {stopwatch.ElapsedMilliseconds}.");
            stopwatch.Stop();
            var elapsedExpressionTyped = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            var orderedFunc = query.AsEnumerable().OrderByDescending(Func);
            LogHelper.LogInformation($"Ordered func.order {stopwatch.ElapsedMilliseconds}.");
            var takeFunc = orderedFunc.Skip(2004).Take(200);
            LogHelper.LogInformation($"Ordered func.take {stopwatch.ElapsedMilliseconds}.");
            var resultFunc = takeFunc.ToList();
            LogHelper.LogInformation($"Ordered func.result {stopwatch.ElapsedMilliseconds}.");
            stopwatch.Stop();
            var elapsedFunc = stopwatch.ElapsedMilliseconds;

            Assert.True(elapsedFunc > elapsedExpressionObject, $"Func {elapsedFunc} ms vs Expression {elapsedExpressionObject } ms.");
            Assert.True(elapsedFunc > elapsedExpressionTyped, $"Func {elapsedFunc} ms vs Expression {elapsedExpressionTyped } ms.");
            Assert.True(elapsedFunc > elapsedMemberSelector, $"Func {elapsedFunc} ms vs MemberSelector {elapsedMemberSelector} ms.");
        }
    }
}