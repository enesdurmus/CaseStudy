using Case.Systems.SimpleIoCContainer.Core;
using Case.TournamentsModule.Enums;
using Case.TournamentsModule.Interfaces;
using Case.TournamentsModule.Strategies.MatchStrategies;
using IServiceProvider = Case.Systems.SimpleIoCContainer.Interfaces.IServiceProvider;

namespace Case.TournamentsModule.Factory
{
    public class MatchRulesStrategyFactory
    {
        //private readonly Dictionary<MatchRule, Type> strategies;
        private readonly IServiceProvider serviceProvider;

        public MatchRulesStrategyFactory()
        {
            serviceProvider = IoCContainer.Instance;

            //strategies = new Dictionary<MatchRule, Type>
            //{
            //    { MatchRule.DEFAULT, typeof(DefaultMatchRuleStrategy) },
            //    { MatchRule.BEST_OF_3, typeof(BestOf3MatchRuleStrategy) }
            //};
        }

        public IMatchRuleStrategy CreateMatchRuleStrategy(MatchRule matchRule)
        {
            return matchRule switch
            {
                MatchRule.DEFAULT => serviceProvider.GetService<DefaultMatchRuleStrategy>(),
                MatchRule.BEST_OF_3 => serviceProvider.GetService<BestOf3MatchRuleStrategy>(),
                _ => throw new ArgumentException("Invalid match rule strategy"),
            };

            //if (strategies.TryGetValue(matchRule, out var matchRuleStrategy))
            //{
            //    return (IMatchRuleStrategy) serviceProvider.GetService(matchRuleStrategy);
            //}
            //throw new ArgumentException($"Strategy '{matchRule}' not found");
        }
    }
}
