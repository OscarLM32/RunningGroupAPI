using AutoMapper;
using CloudinaryDotNet.Actions;
using RunningGroupAPI.DTOs.Club;
using RunningGroupAPI.Interfaces.Repositories;
using RunningGroupAPI.Interfaces.Services;
using RunningGroupAPI.Models;

namespace RunningGroupAPI.Services;

public class ClubService : IClubService
{
	private readonly IMapper _mapper;
	private readonly IClubRepository _clubRepository;
	private readonly IPhotoService _photoService;

	public ClubService(IMapper mapper, IClubRepository clubRepository, IPhotoService photoService)
	{
		_mapper = mapper;
		_clubRepository = clubRepository;
		_photoService = photoService;
	}

	public async Task<IEnumerable<ClubDTO>> GetAllClubsAsync()
	{
		var clubs = await _clubRepository.GetAllClubsAsync();
		return _mapper.Map<IEnumerable<ClubDTO>>(clubs);
	}

	public async Task<ClubDTO> GetClubByIdAsync(int id)
	{
		var club = await _clubRepository.GetClubByIdAsync(id);
		return club != null ? _mapper.Map<ClubDTO>(club) : null;
	}

	public async Task<IEnumerable<ClubDTO>> GetClubsByCityAsync(string city)
	{
		var clubs = await _clubRepository.GetClubsByCityAsync(city);
		return _mapper.Map<IEnumerable<ClubDTO>>(clubs);
	}

	public int AddClub(CreateClubDTO createClubDto)
	{
		ImageUploadResult imageUpload = _photoService.AddPhotoAsync(createClubDto.Image).Result; 
		if(imageUpload == null || imageUpload.Error != null) return -1;
		
		var club = _mapper.Map<Club>(createClubDto);
		club.Image = imageUpload.Url.ToString();
		
		return _clubRepository.AddClub(club);
	}
	public bool UpdateClub(int id, UpdateClubDTO updateClubDto)
	{
		var ogClub = _clubRepository.GetClubByIdAsync(id).Result;
		if(ogClub == null) return false;
		
		var updatedClub = _mapper.Map(updateClubDto, ogClub);
		if(updateClubDto.Image != null)
		{
			var result = _photoService.AddPhotoAsync(updateClubDto.Image).Result;
			if(result == null || result.Error != null) return false;
			
			updatedClub.Image = result.Url.ToString();
		}
		
		return _clubRepository.UpdateClub(updatedClub);
	}
	
	public bool RemoveClub(int id)
	{
		return _clubRepository.RemoveClub(id);
	}

}