using System;
namespace GuardClauses.Abstract
{
    public interface IIfElseBlockGuardClause<T>
        where T : class, new()
    {
        IGuardClause<T> RunBlockElse(Action<T> action);
        IGuardClause<T> RunBlockElse(Action action);
    }
}

