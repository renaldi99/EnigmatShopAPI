namespace EnigmatShopAPI.Repositories
{
    public interface IPersistence
    {
        Task<int>? SaveChangesAsync();
        Task? BeginTransactionAsync();
        Task? CommitTransactionAsync();
        Task? RollbackTransactionAsync();
    }
}
