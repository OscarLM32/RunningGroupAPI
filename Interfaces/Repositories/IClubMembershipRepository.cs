using RunningGroupAPI.Models;

namespace RunningGroupAPI.Interfaces.Repositories;

public interface IClubMembershipRepository
{
	public Task<bool> AddUserToClub(ClubMembership clubMembership);
}