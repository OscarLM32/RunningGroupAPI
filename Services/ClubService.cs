using System.Security.Claims;
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

	public async Task<int> AddClub(CreateClubDTO createClubDto, ClaimsPrincipal user)
	{
		// Extract user ID from ClaimsPrincipal
		string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (string.IsNullOrEmpty(userId))
		{
			return -1; 
		}

		ImageUploadResult imageUpload;
		try
		{
			imageUpload = await _photoService.AddPhotoAsync(createClubDto.Image);
			if (imageUpload == null || imageUpload.Error != null) return -1;
		}
		catch
		{
			return -1;
		}
				
		var club = _mapper.Map<Club>(createClubDto);
		club.Image = imageUpload.Url.ToString();
		//club.AppUserOwnerId = userId;
		
		return await _clubRepository.AddClub(club);
	}
	
	public async Task<bool> UpdateClub(int id, UpdateClubDTO updateClubDto)
	{
		var ogClub = await _clubRepository.GetClubByIdAsync(id);
		if(ogClub == null) return false;
		
		var updatedClub = _mapper.Map(updateClubDto, ogClub);
		if(updateClubDto.Image != null)
		{
			try
			{
				var result = await _photoService.AddPhotoAsync(updateClubDto.Image);
				if (result == null || result.Error != null) return false;
				
				updatedClub.Image = result.Url.ToString();
			}
			catch
			{
				return false;
			}
		}
		
		return await _clubRepository.UpdateClub(updatedClub);
	}
	
	public async Task<bool> RemoveClub(int id)
	{
		return await _clubRepository.RemoveClub(id);
	}
	
	/*public async Task<bool> IsClubOwner(string userId, int clubId)
	{
		var club = await _clubRepository.GetClubByIdAsync(clubId);
		if(club == null) return false;
		
		return club.AppUserOwnerId == userId;
	}*/

}