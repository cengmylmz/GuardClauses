using System;
namespace GuardClauses
{
    public static class Guard
    {

        public static IGuardClause<T> SetValue<T>(T instance)
            where T : class, new()
        {
            return new GuardClause<T>(instance);
        }

        public static IGuardClause<T, T1> SetValue<T, T1>(T instance, T1 instance1)
            where T : class, new()
            where T1 : class, new()
        {
            return new GuardClause<T, T1>(instance, instance1);
        }

        public static IGuardClause<T> SetValue<T>(T instance, string exceptionMessagePrefix)
            where T : class, new()
        {
            return new GuardClause<T>(instance, exceptionMessagePrefix);
        }

        public static IGuardClause<T, T1> SetValue<T, T1>(T instance, T1 instance1, string exceptionMessagePrefix)
            where T : class, new()
            where T1 : class, new()
        {
            return new GuardClause<T, T1>(instance, instance1, exceptionMessagePrefix);
        }
    }
}

