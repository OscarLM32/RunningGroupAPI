using System.Security.Claims;
using AutoMapper;
using CloudinaryDotNet.Actions;
using RunningGroupAPI.DTOs.Club;
using RunningGroupAPI.Helpers.Extensions;
using RunningGroupAPI.Interfaces.Services;
using RunningGroupAPI.Models;
using RunningGroupAPI.Repositories;

namespace RunningGroupAPI.Services;

public class ClubService : IClubService
{
	private readonly IMapper _mapper;
	private readonly UnitOfWork _unitOfWork;
	private readonly IPhotoService _photoService;
	private readonly IHttpContextAccessor _httpContext;

	public ClubService(IMapper mapper, UnitOfWork unitOfWork, IPhotoService photoService, IHttpContextAccessor httpContext)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
		_photoService = photoService;
		_httpContext = httpContext;
	}

	public async Task<IEnumerable<ClubDTO>> GetAllClubsAsync()
	{
		var clubs = await _unitOfWork.ClubRepository.GetAsync();
		return _mapper.Map<IEnumerable<ClubDTO>>(clubs);
	}

	public async Task<ClubDTO> GetClubByIdAsync(string id)
	{
		var club = await _unitOfWork.ClubRepository.GetByIdAsync(id);
		return club != null ? _mapper.Map<ClubDTO>(club) : null;
	}

	public async Task<IEnumerable<ClubDTO>> GetClubsByCityAsync(string city)
	{
		var clubs = await _unitOfWork.ClubRepository.GetAsync(c => c.City == city);
		return _mapper.Map<IEnumerable<ClubDTO>>(clubs);
	}

	public async Task<string> AddClub(CreateClubDTO createClubDto)
	{
		string userId = _httpContext.GetUserId();
		if (string.IsNullOrEmpty(userId))
		{
			return string.Empty;
		}

		ImageUploadResult imageUpload;
		try
		{
			imageUpload = await _photoService.AddPhotoAsync(createClubDto.Image);
			if (imageUpload == null || imageUpload.Error != null) return string.Empty;
		}
		catch
		{
			return string.Empty;
		}
				
		var club = _mapper.Map<Club>(createClubDto);
		club.Id = Guid.NewGuid().ToString();
		club.Image = imageUpload.Url.ToString();
		
		ClubMembership cMembership = new(club.Id, userId, Data.Enum.ClubRole.Owner);
		
		_unitOfWork.ClubRepository.Add(club);
		_unitOfWork.ClubMembershipRepository.Add(cMembership);
		
		if(await _unitOfWork.SaveChangesAsync())
		{
			return club.Id; 
		}
		else
		{
			return string.Empty;
		}
	}
	
	public async Task<bool> UpdateClub(string id, UpdateClubDTO updateClubDto)
	{
		var ogClub = await _unitOfWork.ClubRepository.GetByIdAsync(id);
		if(ogClub == null) return false;
		
		Club updatedClub = _mapper.Map(updateClubDto, ogClub);
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
		
		_unitOfWork.ClubRepository.Update(updatedClub);
		return await _unitOfWork.SaveChangesAsync();
	}
	
	public async Task<bool> RemoveClub(string id)
	{
		_unitOfWork.ClubMembershipRepository.DeleteRange(cm => cm.ClubId == id);
		_unitOfWork.ClubRepository.Delete(id);
		
		return await _unitOfWork.SaveChangesAsync();
	}

}