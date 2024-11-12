using AutoMapper;
using RunningGroupAPI.Data.Enum;
using RunningGroupAPI.DTOs.ClubMembership;
using RunningGroupAPI.Interfaces.Services;
using RunningGroupAPI.Models;
using RunningGroupAPI.Repositories;

namespace RunningGroupAPI.Services;

public class ClubMembershipService : IClubMembershipService
{
	private UnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public ClubMembershipService(UnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<IEnumerable<ClubMembershipDTO>> GetAllMembershipsAsync()
	{
		var memberships = await _unitOfWork.ClubMembershipRepository.GetAsync();
		return _mapper.Map<IEnumerable<ClubMembershipDTO>>(memberships);
	}

	public async Task<IEnumerable<ClubMembershipDTO>> GetUserMembershipsAsync(string userId)
	{
		var memberships = await _unitOfWork.ClubMembershipRepository.GetAsync( cm => cm.AppUserId == userId);
		return _mapper.Map<IEnumerable<ClubMembershipDTO>>(memberships);
	}

	public async Task<IEnumerable<ClubMembershipDTO>> GetClubMembershipsAsync(string clubId)
	{
		var memberships = await _unitOfWork.ClubMembershipRepository.GetAsync( cm => cm.ClubId == clubId);
		return _mapper.Map<IEnumerable<ClubMembershipDTO>>(memberships);
	}
	
	
	public async Task<bool> AddUserToClub(AddUserToClubDTO addUserToClubDTO)
	{
		ClubMembership membership = _mapper.Map<ClubMembership>(addUserToClubDTO);
		membership.JoinDate = DateTime.Now;
		_unitOfWork.ClubMembershipRepository.Add(membership);
		return await _unitOfWork.SaveChangesAsync();
	}
	
	public async Task<bool> IsOwner(string clubId, string userId)
	{
		var membership = await _unitOfWork.ClubMembershipRepository.GetByIdAsync(clubId, userId);
		if(membership == null) return false;
		
		return membership.Role == ClubRole.Owner;
	}

}