namespace Vatsim.Vatis.Profiles;

public interface IPreset
{
    string Name { get; set; }
    string AirportConditions { get; set; }
    string Notams { get; set; }
    string Template { get; set; }
    ExternalGenerator ExternalGenerator { get; set; }
}