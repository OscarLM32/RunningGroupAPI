namespace RunningGroupAPI.Helpers;

public class CloudinarySettings
{

    public string CloudName {get; set;}
	public string ApiKey { get; set; }
	public string ApiSecret { get; set; }
    
	public CloudinarySettings(string cloudName, string apiKey, string apiSecret)
    {
        CloudName = cloudName;
        ApiKey = apiKey;
        ApiSecret = apiSecret;
    }
}