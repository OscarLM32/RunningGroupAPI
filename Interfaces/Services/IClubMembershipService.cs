using RunningGroupAPI.Data.Enum;
using RunningGroupAPI.DTOs.ClubMembership;

namespace RunningGroupAPI.Interfaces.Services;

public interface IClubMembershipService
{
	public Task<bool> AddUserToClub(AddUserToClubDTO addUserToClubDTO);
}