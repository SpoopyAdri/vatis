using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Vatsim.Vatis.Common;

public static class CoreUtils
{
	public static async Task AwaitTimeout(this Task task, int timeout)
	{
		if (task == await Task.WhenAny(task, Task.Delay(timeout)))
		{
			await task;
			return;
		}
		throw new TimeoutException();
	}

	public static string GetCheckSum(this string filePath)
	{
		using var crypto = SHA256.Create();
		using FileStream fileStream = File.OpenRead(filePath);
		return crypto.ComputeHash(fileStream).ToHex();
	}

	public static string ToHex(this byte[] bytes)
	{
		StringBuilder result = new StringBuilder(bytes.Length * 2);
		for (int i = 0; i < bytes.Length; i++)
			result.Append(bytes[i].ToString("x2"));
		return result.ToString();
	}
}