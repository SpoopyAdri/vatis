using System;
using System.Collections.Generic;
using Concentus.Enums;
using Concentus.Structs;
using GeoVR.Shared;

namespace Vatsim.Vatis.AudioForVatsim;

public static class AtisBotHelper
{
	private const int FRAME_SIZE = 960;
	private const int SAMPLE_RATE = 48000;
	private const double BYTES_PER_SECOND = 96000.0;
	private const int BIT_RATE = 8192;
	private const int MAX_OPUS_PACKET_LENGTH = 1275;
	private static byte[] encodedBuffer = new byte[MAX_OPUS_PACKET_LENGTH];
	private static OpusEncoder opusEncoder = new OpusEncoder(SAMPLE_RATE, 1, OpusApplication.OPUS_APPLICATION_VOIP);

	public static PutBotRequestDto AddBotRequest(byte[] audioData, uint frequency, double latDeg, double lonDeg, double altM)
	{
		short[] audioBuffer = ConvertBytesTo16BitPCM(audioData);

		opusEncoder.Bitrate = BIT_RATE;
		Array.Clear(encodedBuffer, 0, encodedBuffer.Length);

		int segmentCount = (int)Math.Floor((double)audioBuffer.Length / FRAME_SIZE);
		int bufferOffset = 0;
		List<byte[]> opusData = new List<byte[]>();

		for (int i = 0; i < segmentCount; i++)
		{
			int len = opusEncoder.Encode(audioBuffer, bufferOffset, FRAME_SIZE, encodedBuffer, 0, encodedBuffer.Length);
			byte[] trimmedBuffer = new byte[len];
			Buffer.BlockCopy(encodedBuffer, 0, trimmedBuffer, 0, len);
			opusData.Add(trimmedBuffer);
			bufferOffset += FRAME_SIZE;
		}

		short[] lastPacketBuffer = new short[FRAME_SIZE];
		int remainderSamples = audioBuffer.Length - segmentCount * FRAME_SIZE;
		Buffer.BlockCopy(audioBuffer, bufferOffset, lastPacketBuffer, 0, remainderSamples);
		int lenRemainder = opusEncoder.Encode(lastPacketBuffer, 0, FRAME_SIZE, encodedBuffer, 0, encodedBuffer.Length);
		byte[] trimmedBufferRemainder = new byte[lenRemainder];
		Buffer.BlockCopy(encodedBuffer, 0, trimmedBufferRemainder, 0, lenRemainder);
		opusData.Add(trimmedBufferRemainder);

		return new PutBotRequestDto
		{
			Transceivers = new List<TransceiverDto>
			{
				new TransceiverDto
				{
					ID = 0,
					Frequency = frequency,
					LatDeg = latDeg,
					LonDeg = lonDeg,
					HeightAglM = altM,
					HeightMslM = altM
				}
			},
			Interval = TimeSpan.FromSeconds((double)audioData.Length / BYTES_PER_SECOND + 3.0),
			OpusData = opusData
		};
	}

	private static short[] ConvertBytesTo16BitPCM(byte[] input)
	{
		int inputSamples = input.Length / 2;
		short[] output = new short[inputSamples];
		int outputIndex = 0;
		for (int i = 0; i < inputSamples; i++)
		{
			output[outputIndex++] = BitConverter.ToInt16(input, i * 2);
		}
		return output;
	}
}