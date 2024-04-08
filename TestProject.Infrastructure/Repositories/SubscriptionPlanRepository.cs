using TestProject.Domain.Entities;
using TestProject.Domain.Repositories;
using TestProject.Infrastructure.Context;

namespace TestProject.Infrastructure.Repositories
{
    public class SubscriptionPlanRepository : ISubscriptionPlanRepository
    {
        private readonly TestProjectDbContext _testProjectDbContext;

        public SubscriptionPlanRepository(TestProjectDbContext testProjectDbContext)
        {
            _testProjectDbContext = testProjectDbContext;
        }

        public SubscriptionPlan GetSubscriptionPlanByDays(int days)
        {
            return _testProjectDbContext.SubscriptionPlans.FirstOrDefault(x => x.Days == days);
        }
    }
}
