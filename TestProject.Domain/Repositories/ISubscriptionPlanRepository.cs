using TestProject.Domain.Entities;

namespace TestProject.Domain.Repositories
{
    public interface ISubscriptionPlanRepository
    {
        SubscriptionPlan GetSubscriptionPlanByDays(int days);
    }
}
