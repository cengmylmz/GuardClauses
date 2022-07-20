using System;
using GuardClauses.Abstract;
using GuardClauses.RuleEngine.Abstract;
using GuardClauses.RuleEngine.Concrete;

namespace GuardClauses.Concrete
{
    public class GuardClause<T> : IGuardClause<T>
        where T : class, new()
    {
        protected T _instance;
        protected bool _isElseBlocActive;
        protected string ExceptionMessagePrefix = string.Empty;
        private string _exceptionMessage = string.Empty;

        protected string ExceptionMessage
        {
            get => string.IsNullOrEmpty(ExceptionMessagePrefix) ? _exceptionMessage : $"{ExceptionMessagePrefix}.{_exceptionMessage}";
            set => _exceptionMessage = value;
        }

        public GuardClause(T instance)
            : this(instance, string.Empty)
        {
        }

        public GuardClause(T instance, string exceptionMessagePrefix)
        {
            _instance = instance;
            _isElseBlocActive = false;
            ExceptionMessagePrefix = exceptionMessagePrefix;
        }

        public IIfElseBlockGuardClause<T> RunBlockIf(Func<T, bool> func, Action<T> action)
        {
            if (func(_instance))
            {
                action(_instance);
            }
            else
            {
                _isElseBlocActive = true;
            }
            return this;
        }

        public IGuardClause<T> RunBlockElse(Action<T> action)
        {
            if (_isElseBlocActive)
            {
                action(_instance);
                _isElseBlocActive = false;
            }
            return this;
        }

        public IGuardClause<T> RunIf(Func<T, bool> func, Action action)
        {
            if (func(_instance))
            {
                action();
            }
            return this;
        }

        public IGuardClause<T> RunIf(Func<T, bool> func, Action<T> action)
        {
            if (func(_instance))
            {
                action(_instance);
            }
            return this;
        }

        public IGuardClause<T> ThrowExceptionIf(Func<T, bool> func)
        {
            return ThrowExceptionIf(func, "Houston,we have a problem", prefix: true);
        }

        public IGuardClause<T> ThrowExceptionIf(Func<T, bool> func, string exceptionMessage, bool prefix = true)
        {
            return ThrowExceptionIf<Exception>(func, exceptionMessage);
        }

        public IGuardClause<T> ThrowExceptionIf<TException>(Func<T, bool> func, string exceptionMessage, bool prefix = true)
            where TException : Exception
        {
            if (func(_instance))
            {

                var message = GetExceptionMessageByPrefix(exceptionMessage, prefix);

#pragma warning disable CS8597 // Thrown value may be null.
                throw Activator.CreateInstance(typeof(TException), message) as TException;
#pragma warning restore CS8597 // Thrown value may be null.
            }
            return this;
        }

        public IGuardClause<T> ThrowExceptionIf<TException>(Func<T, bool> func, TException exception)
            where TException : Exception
        {
            if (func(_instance))
            {
                throw exception;
            }
            return this;
        }

        public IGuardClause<T> ThrowExceptionIf<TException>(Func<T, bool> func, (string exceptionMessage, string code, object[] argument) exception)
            where TException : Exception
        {
            if (func(_instance))
            {
                var exceptionInstance = GetExceptionInstance<TException>(exception);
                throw exceptionInstance;
            }
            return this;
        }

        public IGuardClause<T> ThrowExceptionByRuleEngine(RuleEngineService<RuleEngineBuilder<T>, T> builder, string exceptionMessage = null)
        {
            var result = builder.ApplyRules(_instance);
            if (string.IsNullOrEmpty(exceptionMessage))
            {
                exceptionMessage = string.Join("_", builder.GetErrorMessages());
            }

            return ThrowExceptionIf<Exception>(p => !result, exceptionMessage);
        }




        public IIfElseBlockGuardClause<T> RunBlockIf(Func<T, bool> func, Action action)
        {
            if (func(_instance))
            {
                action();
            }
            else
            {
                _isElseBlocActive = true;
            }
            return this;
        }

        public IGuardClause<T> RunBlockElse(Action action)
        {
            if (_isElseBlocActive)
            {
                action();
                _isElseBlocActive = false;
            }
            return this;
        }

        public IGuardClause<T> RunCatchIf(Func<T, bool> func, Action<T> action)
        {
            try
            {
                ThrowExceptionIf(func);
            }
            catch
            {
                action(_instance);
            }
            return this;
        }

        public IGuardClause<T> RunCatchIf(Func<T, bool> func, Action action)
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

        public IGuardClause<T> RunCatchIf(Action<T> throwException, Action action)
        {
            try
            {
                throwException(_instance);
            }
            catch
            {
                action();
            }
            return this;
        }

        public IGuardClause<T> RunCatchIf(Action<T> throwException, Action<T> action)
        {
            try
            {
                throwException(_instance);
            }
            catch
            {
                action(_instance);
            }
            return this;
        }

        protected string GetExceptionMessageByPrefix(string exceptionMessage, bool prefix)
        {
            if (prefix)
            {
                ExceptionMessage = exceptionMessage;
                return ExceptionMessage;
            }
            return exceptionMessage;
        }

        protected TException GetExceptionInstance<TException>((string exceptionMessage, string code, object[] argument) exception)
            where TException : Exception
        {
            var ex = Activator.CreateInstance(typeof(TException), exception.exceptionMessage, exception.code, exception.argument) as TException;
            return ex;
        }


        public T GetValue()
        {
            return _instance;
        }
    }
}

