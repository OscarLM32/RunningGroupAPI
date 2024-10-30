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

	public async Task<Club> GetClubByIdAsync(int id)
	{
		return await _dbContext.Clubs.FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task<Club> GetClubByIdNoTrackingAsync(int id)
	{
		return await _dbContext.Clubs.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task<IEnumerable<Club>> GetClubsByCityAsync(string city)
	{
		return await _dbContext.Clubs.Where(c => c.City == city).ToListAsync();
	}

	public bool AddClub(Club club)
	{
		_dbContext.Clubs.Add(club);
		return Save();
	}

	public bool UpdateClub(Club club)
	{
		_dbContext.Clubs.Update(club);
		return Save();
	}
	
	public bool RemoveClub(int id)
	{
		var removeClub = GetClubByIdAsync(id).Result;
		_dbContext.Clubs.Remove(removeClub);
		return Save();
	}

	public bool Save()
	{
		return _dbContext.SaveChanges() >= 0;
	}
}