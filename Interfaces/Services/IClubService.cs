using RunningGroupAPI.DTOs.Club;

namespace RunningGroupAPI.Interfaces.Services;

public interface IClubService
{
    public Task<IEnumerable<ClubDTO>> GetAllClubsAsync();
    public Task<ClubDTO> GetClubByIdAsync(int id);
    public Task<IEnumerable<ClubDTO>> GetClubsByCityAsync(string city);
    public bool AddClub(CreateClubDTO createClubDto);
    public bool UpdateClub(int id, UpdateClubDTO updateClubDto);
    public bool RemoveClub(int id);
}