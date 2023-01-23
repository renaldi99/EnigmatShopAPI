namespace EnigmatShopAPI.Repositories
{
    public class Persistence : IPersistence
    {
        private readonly AppDbContext? _appDbContext;

        public Persistence(AppDbContext? appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task? BeginTransactionAsync()
        {
            await _appDbContext?.Database?.BeginTransactionAsync();
        }

        public async Task? CommitTransactionAsync()
        {
            await _appDbContext.Database.CommitTransactionAsync();
        }

        public async Task? RollbackTransactionAsync()
        {
            await _appDbContext.Database.RollbackTransactionAsync();
        }

        public async Task<int>? SaveChangesAsync()
        {
            var Affected = await _appDbContext?.SaveChangesAsync();
            return Affected;
        }
    }
}
