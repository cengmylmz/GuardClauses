
using GuardClauses.RuleEngine.Concrete;

namespace GuardClauses.RuleEngine.Abstract
{
	public interface IRuleEngineBuilder<T>
	{
		RuleEngineService<RuleEngineBuilder<T>, T> Build();
	}
}

