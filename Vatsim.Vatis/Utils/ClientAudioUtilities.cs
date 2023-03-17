using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace Vatsim.Vatis.Utils;

public static class ClientAudioUtilities
{
    public static bool IsInputDevicePresent => WaveIn.DeviceCount > 0;

    public static IEnumerable<string> GetInputDevices()
    {
        return GetWasapiInputDevices();
    }

    public static IEnumerable<string> GetOutputDevices()
    {
        return GetWasapiOutputDevices();
    }

    public static int MapInputDevice(string inputDevice)
    {
        for (int i = 0; i < WaveIn.DeviceCount; i++)
        {
            if (inputDevice.StartsWith(WaveIn.GetCapabilities(i).ProductName))
            {
                return i;
            }
        }
        return 0; // return default
    }

    public static int MapOutputDevice(string outputDevice)
    {
        for (int i = 0; i < WaveOut.DeviceCount; i++)
        {
            if (outputDevice.StartsWith(WaveOut.GetCapabilities(i).ProductName))
                return i;
        }
        return 0; // return default
    }

    public static MMDevice MapWasapiInputDevice(string inputDevice)
    {
        MMDeviceEnumerator em = new MMDeviceEnumerator();
        var deviceCollection = em.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
        return deviceCollection.FirstOrDefault(x => x.FriendlyName == inputDevice) ?? deviceCollection[0];
    }

    public static MMDevice MapWasapiOutputDevice(string outputDevice)
    {
        MMDeviceEnumerator em = new MMDeviceEnumerator();
        var deviceCollection = em.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
        if (deviceCollection.Any(x => x.FriendlyName == outputDevice))
            return deviceCollection.First(x => x.FriendlyName == outputDevice);
        else
            return deviceCollection[0];
    }

    private static IEnumerable<string> GetWasapiInputDevices()
    {
        MMDeviceEnumerator em = new MMDeviceEnumerator();
        var something = em.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
        return something.Select(x => x.FriendlyName);
    }

    private static IEnumerable<string> GetWasapiOutputDevices()
    {
        MMDeviceEnumerator em = new MMDeviceEnumerator();
        var something = em.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
        return something.Select(x => x.FriendlyName);
    }

    public static async Task AwaitTimeout(this Task task, int timeout)
    {
        if (task == await Task.WhenAny(task, Task.Delay(timeout)))
        {
            await task;
            return;
        }
        throw new TimeoutException();
    }
}