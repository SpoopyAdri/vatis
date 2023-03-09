namespace Vatsim.Vatis.Core;

internal static class EventTopics
{
    public const string SessionStarted = "SessionStarted";
    public const string SessionEnded = "SessionEnded";
    public const string AppConfigUpdated = "AppConfigUpdated";
    public const string RefreshAtisComposites = "RefreshAtisComposites";
    public const string AtisCompositeDeleted = "AtisCompositeDeleted";
    public const string PerformVersionCheck = "PerformVersionCheck";
    public const string MinifiedWindowClosed = "MinifiedWindowClosed";
    public const string RefreshMinifiedWindow = "RefreshMinifiedWindow";
    public const string AtisUpdateAcknowledged = "AtisUpdateAcknowledged";
}