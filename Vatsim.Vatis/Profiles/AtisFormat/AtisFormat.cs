using Vatsim.Vatis.Profiles.AtisFormat.Nodes;

namespace Vatsim.Vatis.Profiles.AtisFormat;
public class AtisFormat
{
    public ObservationTime ObservationTime { get; set; } = new();
    public SurfaceWind SurfaceWind { get; set; } = new();
    public Visibility Visibility { get; set; } = new();
    public PresentWeather PresentWeather { get; set; } = new();
    public Clouds Clouds { get; set; } = new();
    public Temperature Temperature { get; set; } = new();
    public Dewpoint Dewpoint { get; set; } = new();
    public Altimeter Altimeter { get; set; } = new();
    public TransitionLevel TransitionLevel { get; set; } = new();
    public ClosingStatement ClosingStatement { get; set; } = new();
}