namespace SmartAnalytics.Services
{
    public interface ITaskService
    {
        string Command { get; }

        string Name { get; }

        void Exec(string[] args);
    }
}
