using System.Linq.Expressions;

namespace Rere.Infrastructure.Extension;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        var parameter = expr1.Parameters[0];

        var visitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);

        var body = Expression.AndAlso(expr1.Body, visitor.Visit(expr2.Body)!);

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    private class ReplaceExpressionVisitor(Expression oldValue, Expression newValue) : ExpressionVisitor
    {
        public override Expression? Visit(Expression? node)
        {
            return node == oldValue ? newValue : base.Visit(node);
        }
    }
}