using System.Linq.Expressions;
using FluentAssertions;
using NUnit.Framework;
using Rere.Extension;

namespace Rere.Core.Tests.Extensions;

[TestFixture]
public class ExpressionExtensionsTests
{
    [Test]
    public void AndAlso_ShouldCombineTwoExpressions()
    {
        // Arrange
        Expression<Func<int, bool>> expr1 = x => x > 5;
        Expression<Func<int, bool>> expr2 = x => x < 10;

        // Act
        var combinedExpression = expr1.AndAlso(expr2);

        // Assert
        var compiled = combinedExpression.Compile();
        compiled(6).Should().BeTrue(); // 6 > 5 && 6 < 10
        compiled(4).Should().BeFalse(); // 4 > 5 && 4 < 10
        compiled(11).Should().BeFalse(); // 11 > 5 && 11 < 10
    }

    [Test]
    public void AndAlso_ShouldHandleNullValues()
    {
        Expression<Func<string, bool>> expr1 = s => s != null;
        Expression<Func<string, bool>> expr2 = s => s.Length > 3;

        var combinedExpression = expr1.AndAlso(expr2);

        var compiled = combinedExpression.Compile();
        compiled("Hello").Should().BeTrue(); // "Hello" != null && "Hello".Length > 3
        compiled("Hi").Should().BeFalse(); // "Hi" != null && "Hi".Length > 3
        compiled(null).Should().BeFalse(); // null != null && null.Length > 3 (short-circled)
    }

    [Test]
    public void AndAlso_ShouldCombineWithMultipleExpressions()
    {
        Expression<Func<int, bool>> expr1 = x => x > 0;
        Expression<Func<int, bool>> expr2 = x => x < 100;
        Expression<Func<int, bool>> expr3 = x => x % 2 == 0;

        var combinedExpression = expr1.AndAlso(expr2).AndAlso(expr3);

        var compiled = combinedExpression.Compile();
        compiled(50).Should().BeTrue(); // 50 > 0 && 50 < 100 && 50 % 2 == 0
        compiled(101).Should().BeFalse(); // 101 > 0 && 101 < 100 && 101 % 2 == 0
        compiled(-1).Should().BeFalse(); // -1 > 0 && -1 < 100 && -1 % 2 == 0
        compiled(3).Should().BeFalse(); // 3 > 0 && 3 < 100 && 3 % 2 == 0
    }
}