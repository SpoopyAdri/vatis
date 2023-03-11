namespace Vatsim.Network
{
    public class NetworkEventArgs : EventArgs
	{
		public object UserData { get; }

		public NetworkEventArgs(object userData)
		{
			UserData = userData;
		}
	}
}
