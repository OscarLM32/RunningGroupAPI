using AutoMapper;
using RunningGroupAPI.DTOs.Club;
using RunningGroupAPI.Interfaces.Repositories;
using RunningGroupAPI.Interfaces.Services;

namespace RunningGroupAPI.Services;

public class ClubService : IClubService
{
    private readonly IMapper _mapper;
    private readonly IClubRepository _clubRepository;

	public ClubService(IMapper mapper, IClubRepository clubRepository)
	{
		_mapper = mapper;
		_clubRepository = clubRepository;
	}

	public Task<IEnumerable<ClubDTO>> GetAllClubsAsync()
	{
		throw new NotImplementedException();
	}

	public Task<ClubDTO> GetClubByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<ClubDTO>> GetClubsByCityAsync(string city)
	{
		throw new NotImplementedException();
	}

	public bool AddClub(CreateClubDTO ceateClubDto)
	{
		throw new NotImplementedException();
	}
	public bool UpdateClub(int id, UpdateClubDTO updateClubDto)
	{
		throw new NotImplementedException();
	}
	
	public bool RemoveClub(int id)
	{
		throw new NotImplementedException();
	}

}