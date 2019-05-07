namespace WebAnalytics.DAL.Repositories.Interfaces
{
    public interface IUniqueUsersCounterRepository
    {
        int Get();
        void Increment();
    }
}