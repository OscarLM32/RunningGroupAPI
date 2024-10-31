using AutoMapper;
using RunningGroupAPI.DTOs.Club;
using RunningGroupAPI.Models;

namespace RunningGroupAPI.Helpers.AutoMappers;

public class ClubMappingProfile : Profile
{
	public ClubMappingProfile()
	{
		CreateMap<CreateClubDTO, Club>()
			.ForMember(dest => dest.Image, opt => opt.Ignore())  // Ignore image if handled separately
			.ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserOwnerId));

		CreateMap<Club, ClubDTO>();

		CreateMap<UpdateClubDTO, Club>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Ignore nulls to prevent overwriting
	}
}