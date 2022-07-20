using System;
using GuardClauses.RuleEngine.Concrete;
using GuardClauses.RuleEngine.Rules;

namespace GuardClauses.RuleEngine.Abstract
{
    public abstract class RuleEngineBuilder<T> : IRuleEngineBuilder<T>
    {
        protected List<RuleBase<T>> Rules = new();
        protected List<string> ErrorMessages = new();

        public RuleEngineService<RuleEngineBuilder<T>, T> Build()
        {
            Rules.Add(new DefaultRule<T>());

            return new RuleEngineService<RuleEngineBuilder<T>, T>(Rules, ErrorMessages);
        }
    }
}

