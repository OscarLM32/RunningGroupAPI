using System.Security.Claims;
using RunningGroupAPI.DTOs.Club;

namespace RunningGroupAPI.Interfaces.Services;

public interface IClubService
{
    public Task<IEnumerable<ClubDTO>> GetAllClubsAsync();
    public Task<ClubDTO> GetClubByIdAsync(int id);
    public Task<IEnumerable<ClubDTO>> GetClubsByCityAsync(string city);
    public Task<int> AddClub(CreateClubDTO createClubDto, ClaimsPrincipal user);
    public Task<bool> UpdateClub(int id, UpdateClubDTO updateClubDto);
    public Task<bool> RemoveClub(int id);
}