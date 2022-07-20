using System.Linq.Expressions;
using GuardClauses.RuleEngine.Abstract;
using GuardClauses.RuleEngine.Concrete;

namespace GuardClauses.Abstract
{
    public interface IGuardClause<T> : IIfElseBlockGuardClause<T>
        where T : class, new()
    {
        IGuardClause<T> RunIf(Func<T, bool> func, Action action);
        IGuardClause<T> RunIf(Func<T, bool> func, Action<T> action);
        IIfElseBlockGuardClause<T> RunBlockIf(Func<T, bool> func, Action<T> action);
        IIfElseBlockGuardClause<T> RunBlockIf(Func<T, bool> func, Action action);
        IGuardClause<T> ThrowExceptionIf(Func<T, bool> func);
        IGuardClause<T> ThrowExceptionIf(Func<T, bool> func, string errorMessage, bool prefix = true);
        IGuardClause<T> ThrowExceptionIf<TException>(Func<T, bool> func, string errorMessage, bool prefix = true) where TException : Exception;
        IGuardClause<T> ThrowExceptionIf<TException>(Func<T, bool> func, TException exception)
            where TException : Exception;

        IGuardClause<T> ThrowExceptionIf<TException>(Func<T, bool> func, (string exceptionMessage, string code, object[] argument) exception)
            where TException : Exception;

        IGuardClause<T> RunCatchIf(Func<T, bool> func, Action<T> action);
        IGuardClause<T> RunCatchIf(Func<T, bool> func, Action action);
        IGuardClause<T> RunCatchIf(Action<T> throwException, Action<T> action);
        IGuardClause<T> RunCatchIf(Action<T> throwException, Action action);

        IGuardClause<T> ThrowExceptionByRuleEngine(RuleEngineService<RuleEngineBuilder<T>, T> builder, string exceptionMessage = null);

        T GetValue();
    }
}