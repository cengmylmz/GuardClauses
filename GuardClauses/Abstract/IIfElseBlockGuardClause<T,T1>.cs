using System;
namespace GuardClauses.Abstract
{
    public interface IIfElseBlockGuardClause<T, T1>
        where T : class, new()
        where T1 : class, new()
    {
        IGuardClause<T, T1> RunBlockElse(Action<T, T1> action);
        IGuardClause<T, T1> RunBlockElse(Action action);
    }
}

