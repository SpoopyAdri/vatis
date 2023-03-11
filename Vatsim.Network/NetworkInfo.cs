using Newtonsoft.Json.Linq;
using System.Net;

namespace Vatsim.Network
{
	public class NetworkInfo
	{
        public static async Task<List<NetworkServerInfo>> DownloadServerList(string statusUrl)
        {
            try
            {
                // download status file
                var statusFile = await new HttpClient().GetStringAsync(statusUrl);
                if (!string.IsNullOrEmpty(statusFile))
                {
                    var statusJson = JObject.Parse(statusFile);
                    if (statusJson != null && statusJson.HasValues)
                    {
                        var serverUrls = (JArray)statusJson["data"]["servers"];
                        if (serverUrls != null && serverUrls.HasValues)
                        {
                            // pick random server list URL
                            int random = new Random().Next(serverUrls.Count);
                            var randomServerListUrl = serverUrls[random].ToString();

                            // download server list from random URL
                            var serverList = await new HttpClient().GetStringAsync(randomServerListUrl);
                            if (!string.IsNullOrEmpty(serverList))
                            {
                                var serverListJson = JArray.Parse(serverList);

                                if (serverListJson != null && serverListJson.HasValues)
                                {
                                    List<NetworkServerInfo> list = new();

                                    foreach (var server in serverListJson)
                                    {
                                        var name = server["name"].ToString();
                                        var hostname = server["hostname_or_ip"].ToString();

                                        list.Add(new NetworkServerInfo
                                        {
                                            Name = name,
                                            Address = hostname,
                                        });
                                    }

                                    return list;
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            return null;
        }
    }
}