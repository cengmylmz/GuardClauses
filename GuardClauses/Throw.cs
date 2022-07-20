using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace GuardClauses
{
    public static class Throw
    {
        public static void IfTrue<T>(Expression<Func<T, bool>> func, T value, string exceptionMessage)
            => ThrowExceptionIf(func, func.Compile()(value), exceptionMessage);

        public static void IfTrue(Expression<Func<bool>> func, string exceptionMessage)
            => ThrowExceptionIf(func, exceptionMessage);

        public static void IfFalse<T>(Expression<Func<T, bool>> func, T value, [NotNull] string exceptionMessage)
            => ThrowExceptionIf(func, !func.Compile()(value), exceptionMessage);

        public static void IfTrue(bool isThrowException, string exceptionMessage)
            => ThrowExceptionIf(isThrowException, exceptionMessage);

        public static void IfFalse<T>(bool isThrowException, string exceptionMessage)
            => ThrowExceptionIf(isThrowException, exceptionMessage);

        public static void IfTrue<T, T1>(Func<T, T1, bool> func, T value, T1 value1, string exceptionMessage)
            => ThrowExceptionIf(func(value, value1), exceptionMessage);

        public static void IfFalse<T, T1>(Func<T, T1, bool> func, T value, T1 value1, string exceptionMessage)
            => ThrowExceptionIf(!func(value, value1), exceptionMessage);

        public static void IfNullOrEmpty<T>(Expression<Func<T, string>> func, T value, string? exceptionMessage)
            => ThrowExceptionIf(func, string.IsNullOrEmpty(func.Compile()(value)), exceptionMessage);

        public static void IfNullOrEmpty<T>(Expression<Func<T, string>> func, T value, string? exceptionMessage, string? template)
            => ThrowExceptionIf(func, string.IsNullOrEmpty(func.Compile()(value)), exceptionMessage, template);

        public static void IfNotNull<T, Tkey>(Expression<Func<T, Tkey>> func, T value, string? exceptionMessage, string? template)
            => ThrowExceptionIf(func, (func.Compile()(value)) != null, exceptionMessage, template);


        public static void IfNull<T, TKey>(Expression<Func<T?, TKey?>> func, T? value, string? exceptionMessage)
            => ThrowExceptionIf(func, func.Compile()(value) is null, exceptionMessage);

        public static void IfGreaterThan<T>(Expression<Func<T, int>> func, int compareTo, T value, string? exceptionMessage)
            => ThrowExceptionIf(func, (func.Compile()(value)) > compareTo, exceptionMessage);

        public static void IfGreaterThanZero<T>(Expression<Func<T, int>> func, T value, string? exceptionMessage)
            => IfGreaterThan(func, 0, value, exceptionMessage);

        public static void IfLessThan<T>(Expression<Func<T, int>> func, int compareTo, T value, string? exceptionMessage)
            => ThrowExceptionIf(func, func.Compile()(value) < compareTo, exceptionMessage);

        public static void IfLessThanZero<T>(Expression<Func<T, int>> func, T value, string? exceptionMessage)
            => IfLessThan(func, 0, value, exceptionMessage);



        public static void IfNullOrEmpty<T>(Expression<Func<T, string>> func, T value)
            => IfNullOrEmpty(func, value, exceptionMessage: null);

        public static void IfNull<T, TKey>(Expression<Func<T?, TKey?>> func, T? value)
            => IfNull(func, value, exceptionMessage: null);

        public static void IfGreaterThan<T>(Expression<Func<T, int>> func, int compareTo, T value)
            => IfGreaterThan(func, compareTo, value, exceptionMessage: null);

        public static void IfGreaterThanZero<T>(Expression<Func<T, int>> func, T value)
            => IfGreaterThanZero(func, value, exceptionMessage: null);

        public static void IfLessThan<T>(Expression<Func<T, int>> func, int compareTo, T value)
            => IfLessThan(func, compareTo, value, exceptionMessage: null);

        public static void IfLessThanZero<T>(Expression<Func<T, int>> func, T value)
            => IfLessThanZero(func, value, exceptionMessage: null);

        private static void ThrowExceptionIf<T, TKey>(Expression<Func<T, TKey>>? func, bool isThrowException, string? exceptionMessage, string template = null)
        {
            if (isThrowException)
            {
                if (func is not null && string.IsNullOrEmpty(exceptionMessage))
                {
                    var expression = (MemberExpression)func.Body;
                    exceptionMessage = !string.IsNullOrEmpty(template) ? string.Format(template, expression.Member.Name) : expression.Member.Name;
                }

                throw new Exception(exceptionMessage);
            }
        }

        private static void ThrowExceptionIf(bool isThrowException, string? exceptionMessage)
        {
            if (isThrowException)
            {
                throw new Exception(exceptionMessage);
            }
        }

        private static void ThrowExceptionIf(Expression<Func<bool>> func, string? exceptionMessage)
        {
            if (func.Compile()())
            {
                throw new Exception(exceptionMessage);
            }
        }
    }
}

