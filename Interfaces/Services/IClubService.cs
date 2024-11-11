using System.Security.Claims;
using RunningGroupAPI.DTOs.Club;

namespace RunningGroupAPI.Interfaces.Services;

public interface IClubService
{
	public Task<IEnumerable<ClubDTO>> GetAllClubsAsync();
	public Task<ClubDTO> GetClubByIdAsync(string id);
	public Task<IEnumerable<ClubDTO>> GetClubsByCityAsync(string city);
	
	public Task<string> AddClub(CreateClubDTO createClubDto);
	public Task<bool> UpdateClub(string id, UpdateClubDTO updateClubDto);
	public Task<bool> RemoveClub(string id);

	//public Task<bool> IsClubOwner(string userId, int clubId);
}