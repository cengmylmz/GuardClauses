using System;
namespace GuardClauses.RuleEngine.Rules
{
	public abstract class RuleBase<T>
	{
		public abstract bool IsMatch(T value);
		public abstract string ErrorMessage { get; set; }

	}
}

