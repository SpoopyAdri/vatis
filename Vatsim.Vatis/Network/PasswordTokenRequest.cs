namespace Vatsim.Vatis.Network;

public class PasswordTokenRequest
{
    public string cid { get; set; }
    public string password { get; set; }
    public PasswordTokenRequest(string id, string pass)
    {
        cid = id;
        password = pass;
    }
}