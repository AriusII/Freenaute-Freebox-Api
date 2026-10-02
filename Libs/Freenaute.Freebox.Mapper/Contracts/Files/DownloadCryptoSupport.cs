using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.crypto_support</summary>
[JsonConverter(typeof(JsonStringEnumConverter<DownloadCryptoSupport>))]
public enum DownloadCryptoSupport
{
    [JsonStringEnumMemberName("unsupported")] Unsupported,
    [JsonStringEnumMemberName("allowed")] Allowed,
    [JsonStringEnumMemberName("preferred")] Preferred,
    [JsonStringEnumMemberName("required")] Required,
}
