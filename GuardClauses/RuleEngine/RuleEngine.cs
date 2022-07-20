using System;
namespace GuardClauses.RuleEngine
{
	public static class RuleEngine
	{
		public static T Get<T>()
		{
			return (T)Activator.CreateInstance(typeof(T));
		}
	}
}

