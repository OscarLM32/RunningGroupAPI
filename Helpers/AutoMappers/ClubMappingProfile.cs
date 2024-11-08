using AutoMapper;
using RunningGroupAPI.DTOs.Club;
using RunningGroupAPI.Models;

namespace RunningGroupAPI.Helpers.AutoMappers;

public class ClubMappingProfile : Profile
{
	public ClubMappingProfile()
	{
		CreateMap<Club, ClubDTO>();
		
		CreateMap<CreateClubDTO, Club>()
			.ForMember(dest => dest.Image, opt => opt.Ignore());  // Ignore image, handled separately


		CreateMap<UpdateClubDTO, Club>()
			.ForMember(dest => dest.Image, opt => opt.Ignore())  // Ignore image, handled separately
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Ignore nulls to prevent overwriting
	}
}