namespace Vatsim.Vatis.Network;

public class PasswordTokenResponse
{
    public bool success { get; set; }
    public string error_msg { get; set; }
    public string token { get; set; }
}
