using System.Text;

namespace Vatsim.Network.PDU
{
	[Serializable]
	public enum AddATCPositionType
	{
		Normal,
		ATIS
	}

	public static class AddATCPositionTypeExtensions
	{
		public static string Prefix(this AddATCPositionType type)
		{
			switch (type)
			{
				case AddATCPositionType.Normal: return "#AA";
				case AddATCPositionType.ATIS: return "#AB";
				default: throw new ApplicationException($"Unknown AddATC position type: {type}.");
			}
		}
	}

    public class PDUAddATC : PDUBase
	{
		public AddATCPositionType Type { get; set; }
		public string RealName { get; set; }
		public string CID { get; set; }
		public string Password { get; set; }
		public NetworkRating Rating { get; set; }
		public ProtocolRevision ProtocolRevision { get; set; }

		public PDUAddATC(AddATCPositionType type, string callsign, string realName, string cid, string password, NetworkRating rating, ProtocolRevision proto)
			: base(callsign, "")
		{
			Type = type;
			RealName = realName;
			CID = cid;
			Password = password;
			Rating = rating;
			ProtocolRevision = proto;
		}

		public override string Serialize()
		{
			StringBuilder msg = new StringBuilder(Type.Prefix());
			msg.Append(From);
			msg.Append(DELIMITER);
			msg.Append(SERVER_CALLSIGN);
			msg.Append(DELIMITER);
			msg.Append(RealName);
			msg.Append(DELIMITER);
			msg.Append(CID);
			msg.Append(DELIMITER);
			msg.Append(Password);
			msg.Append(DELIMITER);
			msg.Append((int)Rating);
			msg.Append(DELIMITER);
			msg.Append((int)ProtocolRevision);
			return msg.ToString();
		}

		public static PDUAddATC Parse(AddATCPositionType type, string[] fields)
		{
			if (fields.Length < 6) throw new PDUFormatException("Invalid field count.", Reassemble(fields));
			try
			{
				return new PDUAddATC(
					type,
					fields[0],
					fields[2],
					fields[3],
					"",
					(NetworkRating)int.Parse(fields[5]),
					ProtocolRevision.Unknown
				);
			}
			catch (Exception ex)
			{
				throw new PDUFormatException("Parse error.", Reassemble(fields), ex);
			}
		}
	}
}