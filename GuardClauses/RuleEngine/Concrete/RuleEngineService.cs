using System;
using GuardClauses.RuleEngine.Abstract;
using GuardClauses.RuleEngine.Rules;

namespace GuardClauses.RuleEngine.Concrete
{
    public class RuleEngineService<T, TValue>
        where T : RuleEngineBuilder<TValue>
    {
        private List<RuleBase<TValue>> _rules = new();
        private List<string> errorMessages = new();

        public RuleEngineService(List<RuleBase<TValue>> rules, List<string> errorMessages)
        {
            _rules = rules;
            this.errorMessages = errorMessages;
        }

        public bool ApplyRules(TValue value)
        {
            var result = true;
            foreach (var rule in _rules)
            {
                var isMatch = rule.IsMatch(value);

                if (!isMatch)
                    errorMessages.Add(rule.ErrorMessage);

                result = result && isMatch;

            }
            return result;
        }

        public List<string> GetErrorMessages()
        {
            return errorMessages;
        }
    }
}

