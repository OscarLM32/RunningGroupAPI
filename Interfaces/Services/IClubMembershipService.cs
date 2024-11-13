using RunningGroupAPI.Data.Enum;
using RunningGroupAPI.DTOs.ClubMembership;
using RunningGroupAPI.Models;

namespace RunningGroupAPI.Interfaces.Services;

public interface IClubMembershipService
{
	public Task<IEnumerable<ClubMembershipDTO>> GetAllMembershipsAsync();
	public Task<IEnumerable<ClubMembershipDTO>> GetUserMembershipsAsync(string userId);
	public Task<IEnumerable<ClubMembershipDTO>> GetClubMembershipsAsync(string clubId);
	public Task<ClubMembershipDTO> GetMembershipAsync(string clubId, string userId);

	public Task<bool> AddUserToClub(AddUserToClubDTO addUserToClubDTO);
	public Task<bool> UpdateUserRole(UpdateUserClubRoleDTO updateUserClubRoleDTO);
	public Task<bool> DeleteUserFromClub(DeleteUserFromClubDTO deleteUserFromClubDTO);
	public Task<bool> IsOwner(string userId, string clubId);
}