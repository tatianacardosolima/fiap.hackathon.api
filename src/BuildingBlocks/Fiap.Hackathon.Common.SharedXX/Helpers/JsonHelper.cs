using System.Text;
using System.Text.Json;

namespace Fiap.Hackathon.Common.Shared.Helpers
{
    public static class JsonHelper
    {
        public static StringContent GetStringContent(object obj)
            => new StringContent(JsonSerializer.Serialize(obj), Encoding.Default, "application/json");
    }
}
