using RunningGroupAPI.Data;
using RunningGroupAPI.Models;

namespace RunningGroupAPI.Repositories;

public class UnitOfWork : IDisposable
{
	private AppDbContext _context;

	public GenericRepository<Club> ClubRepository { get; private set; }
	public GenericRepository<ClubMembership> ClubMembershipRepository { get; private set; }

	
	public UnitOfWork(AppDbContext context)
	{
		_context = context;
		ClubRepository = new(context);
		ClubMembershipRepository = new(context);
	}
	
	public async Task<bool> SaveChangesAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}

	public void Dispose()
	{
		_context.Dispose();
	}
}