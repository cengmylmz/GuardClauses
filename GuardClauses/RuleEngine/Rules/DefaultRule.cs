using System;
namespace GuardClauses.RuleEngine.Rules
{
    public class DefaultRule<T> : RuleBase<T>
    {
        public override string ErrorMessage { get; set; } = "Default";

        public override bool IsMatch(T value)
        {
            return true;
        }
    }
}

