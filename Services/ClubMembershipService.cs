using AutoMapper;
using RunningGroupAPI.Data.Enum;
using RunningGroupAPI.DTOs.ClubMembership;
using RunningGroupAPI.Interfaces.Repositories;
using RunningGroupAPI.Interfaces.Services;
using RunningGroupAPI.Models;

namespace RunningGroupAPI.Services;

public class ClubMembershipService : IClubMembershipService
{
	private readonly IClubMembershipRepository _membershipRepository;
	private readonly IMapper _mapper;

	public ClubMembershipService(IClubMembershipRepository memebershipRepository, IMapper mapper)
	{
		_membershipRepository = memebershipRepository;
		_mapper = mapper;
	}

	public async Task<bool> AddUserToClub(AddUserToClubDTO addUserToClubDTO)
	{
		ClubMembership membership = _mapper.Map<ClubMembership>(addUserToClubDTO);
		membership.JoinDate = DateTime.Now;
		return await _membershipRepository.AddUserToClub(membership);
	}
}