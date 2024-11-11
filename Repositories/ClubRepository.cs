using Microsoft.EntityFrameworkCore;
using RunningGroupAPI.Data;
using RunningGroupAPI.Interfaces.Repositories;
using RunningGroupAPI.Models;

namespace RunningGroupAPI.Repositories;

public class ClubRepository : IClubRepository
{
	private readonly AppDbContext _dbContext;

	public ClubRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<IEnumerable<Club>> GetAllClubsAsync()
	{
		return await _dbContext.Clubs.ToListAsync();
	}

	public async Task<Club> GetClubByIdAsync(string id)
	{
		return await _dbContext.Clubs.FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task<Club> GetClubByIdNoTrackingAsync(string id)
	{
		return await _dbContext.Clubs.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task<IEnumerable<Club>> GetClubsByCityAsync(string city)
	{
		return await _dbContext.Clubs.Where(c => c.City == city).ToListAsync();
	}

	public async Task<string> AddClub(Club club)
	{
		_dbContext.Clubs.Add(club);
		
		if(await SaveAsync())
		{
			return club.Id;
		}
		return null;
	}

	public async Task<bool> UpdateClub(Club club)
	{
		_dbContext.Clubs.Update(club);
		return await SaveAsync();
	}
	
	public async Task<bool> RemoveClub(string id)
	{
		var removeClub = await GetClubByIdAsync(id);
		_dbContext.Clubs.Remove(removeClub);
		return await SaveAsync();
	}

	private async Task<bool> SaveAsync()
	{
		return await _dbContext.SaveChangesAsync() > 0;
	}
}