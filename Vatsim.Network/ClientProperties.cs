namespace Vatsim.Network
{
	public class ClientProperties
	{
		public string Name { get; set; }
		public Version Version { get; set; }
		public string ClientHash { get; set; }

		public ClientProperties(string name, Version ver, string hash)
		{
			Name = name;
			Version = ver;
			ClientHash = hash;
		}

		public override string ToString()
		{
			return string.Format("{0} {1}", Name, Version);
		}
	}
}