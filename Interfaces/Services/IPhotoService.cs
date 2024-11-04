using CloudinaryDotNet.Actions;

namespace RunningGroupAPI.Interfaces.Services;

public interface IPhotoService
{
	Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
	Task<DeletionResult> DeletePhotoAsync(string url); 
}