using RunningGroupAPI.Models;

namespace RunningGroupAPI.Interfaces;

public interface IClubRepository
{
	public Task<IEnumerable<Club>> GetAllClubsAsync();
	public Task<Club> GetClubByIdAsync(int id);
	public Task<Club> GetClubByIdNoTrackingAsync(int id);
	public Task<IEnumerable<Club>> GetClubsByCityAsync(string city);
	
	public bool AddClub(Club club);
	public bool UpdateClub(Club club);
	public bool RemoveClub(int id);
	public bool Save();

}