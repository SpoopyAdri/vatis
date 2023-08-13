using System.Threading;
using System.Threading.Tasks;
using Vatsim.Vatis.Profiles;

namespace Vatsim.Vatis.TextToSpeech;

public interface ITextToSpeechRequest
{
    Task<byte[]> RequestSynthesizedText(string text, Composite composite, CancellationToken token);
}