namespace GuardClauses.Abstract
{
    public interface IGuardClause<T, T1> : IGuardClause<T>, IIfElseBlockGuardClause<T, T1>
        where T : class, new()
        where T1 : class, new()
    {
        IGuardClause<T, T1> RunIf(Func<T, T1, bool> func, Action action);
        IGuardClause<T, T1> RunIf(Func<T, T1, bool> func, Action<T> action);
        IGuardClause<T, T1> RunIf(Func<T, T1, bool> func, Action<T1> action);
        IGuardClause<T, T1> RunIf(Func<T, T1, bool> func, Action<T, T1> action);
        IIfElseBlockGuardClause<T, T1> RunBlockIf(Func<T, T1, bool> func, Action<T, T1> action);
        IIfElseBlockGuardClause<T, T1> RunBlockIf(Func<T, T1, bool> func, Action action);
        IGuardClause<T, T1> ThrowExceptionIf(Func<T, T1, bool> func);
        IGuardClause<T, T1> ThrowExceptionIf(Func<T, T1, bool> func, string errorMessage, bool prefix = true);
        IGuardClause<T, T1> ThrowExceptionIf<TException>(Func<T, T1, bool> func, string errorMessage, bool prefix = true) where TException : Exception;

        IGuardClause<T, T1> RunCatchIf(Func<T, T1, bool> func, Action<T, T1> action);
        IGuardClause<T, T1> RunCatchIf(Func<T, T1, bool> func, Action action);
        IGuardClause<T, T1> RunCatchIf(Action<T, T1> throwException, Action<T, T1> action);
        IGuardClause<T, T1> RunCatchIf(Action<T, T1> throwException, Action action);

    }

}