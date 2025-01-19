namespace LibraryLoans.Core.Monitors;

public interface IDatabaseMonitor
{
    Task<bool> IsDatabaseAlive();
}
