using System;
using GuardClauses.Abstract;
using GuardClauses.RuleEngine.Abstract;
using GuardClauses.RuleEngine.Concrete;

namespace GuardClauses.Concrete
{
    public class GuardClause<T, T1> : GuardClause<T>, IGuardClause<T, T1>
        where T : class, new()
        where T1 : class, new()
    {
        private T1 _instance1;

        public GuardClause(T instance, T1 instance1)
            : base(instance)
        {
            _instance1 = instance1;
        }

        public GuardClause(T instance, T1 instance1, string exceptionMessagePrefix)
            : base(instance, exceptionMessagePrefix)
        {
            _instance1 = instance1;
        }

        public IGuardClause<T, T1> RunBlockElse(Action<T, T1> action)
        {
            if (_isElseBlocActive)
            {
                action(_instance, _instance1);
                _isElseBlocActive = false;
            }
            return this;
        }

        public new IGuardClause<T, T1> RunBlockElse(Action action)
        {
            if (_isElseBlocActive)
            {
                action();
                _isElseBlocActive = false;
            }
            return this;
        }

        public IIfElseBlockGuardClause<T, T1> RunBlockIf(Func<T, T1, bool> func, Action<T, T1> action)
        {
            if (func(_instance, _instance1))
            {
                action(_instance, _instance1);
            }
            else
            {
                _isElseBlocActive = true;
            }
            return this;
        }

        public IIfElseBlockGuardClause<T, T1> RunBlockIf(Func<T, T1, bool> func, Action action)
        {
            if (func(_instance, _instance1))
            {
                action();
            }
            else
            {
                _isElseBlocActive = true;
            }
            return this;
        }

        public IGuardClause<T, T1> RunIf(Func<T, T1, bool> func, Action action)
        {
            if (func(_instance, _instance1))
            {
                action();
            }
            return this;
        }

        public IGuardClause<T, T1> RunIf(Func<T, T1, bool> func, Action<T> action)
        {
            if (func(_instance, _instance1))
            {
                action(_instance);
            }
            return this;
        }

        public IGuardClause<T, T1> RunIf(Func<T, T1, bool> func, Action<T1> action)
        {
            if (func(_instance, _instance1))
            {
                action(_instance1);
            }
            return this;
        }

        public IGuardClause<T, T1> RunIf(Func<T, T1, bool> func, Action<T, T1> action)
        {
            if (func(_instance, _instance1))
            {
                action(_instance, _instance1);
            }
            return this;
        }

        public IGuardClause<T, T1> ThrowExceptionIf(Func<T, T1, bool> func, string exceptionMessage, bool prefix = true)
        {
            return ThrowExceptionIf<Exception>(func, exceptionMessage, prefix);
        }

        public IGuardClause<T, T1> ThrowExceptionIf(Func<T, T1, bool> func)
        {
            return ThrowExceptionIf(func, "Houston,we have a problem");
        }

        public IGuardClause<T, T1> ThrowExceptionIf<TException>(Func<T, T1, bool> func, string exceptionMessage, bool prefix = true) where TException : Exception
        {
            if (func(_instance, _instance1))
            {
                var message = GetExceptionMessageByPrefix(exceptionMessage, prefix);
#pragma warning disable CS8597 // Thrown value may be null.
                throw Activator.CreateInstance(typeof(TException), message) as TException;
#pragma warning restore CS8597 // Thrown value may be null.
            }
            return this;
        }

        public IGuardClause<T, T1> RunCatchIf(Func<T, T1, bool> func, Action action)
        {
            try
            {
                ThrowExceptionIf(func);
            }
            catch
            {
                action();
            }
            return this;
        }

        public IGuardClause<T, T1> RunCatchIf(Func<T, T1, bool> func, Action<T, T1> action)
        {
            try
            {
                ThrowExceptionIf(func);
            }
            catch
            {
                action(_instance, _instance1);
            }
            return this;
        }

        public IGuardClause<T, T1> RunCatchIf(Action<T, T1> throwException, Action action)
        {
            try
            {
                throwException(_instance, _instance1);
            }
            catch
            {
                action();
            }
            return this;
        }

        public IGuardClause<T, T1> RunCatchIf(Action<T, T1> throwException, Action<T, T1> action)
        {
            try
            {
                throwException(_instance, _instance1);
            }
            catch
            {
                action(_instance, _instance1);
            }
            return this;
        }

    }
}

