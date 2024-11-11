using RunningGroupAPI.Models;

namespace RunningGroupAPI.Interfaces.Repositories;

public interface IClubRepository
{
	public Task<IEnumerable<Club>> GetAllClubsAsync();
	public Task<Club> GetClubByIdAsync(string id);
	public Task<Club> GetClubByIdNoTrackingAsync(string id);
	public Task<IEnumerable<Club>> GetClubsByCityAsync(string city);
	
	public Task<string> AddClub(Club club);
	public Task<bool> UpdateClub(Club club);
	public Task<bool> RemoveClub(string id);

}