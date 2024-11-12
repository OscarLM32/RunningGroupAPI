using AutoMapper;
using RunningGroupAPI.DTOs.ClubMembership;
using RunningGroupAPI.Models;

namespace RunningGroupAPI.Helpers.AutoMappers;

public class ClubMembershipProfile : Profile
{
	public ClubMembershipProfile()
	{
		CreateMap<AddUserToClubDTO, ClubMembership>();
		
		CreateMap<ClubMembership, ClubMembershipDTO>()
			.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AppUserId));
	}
}