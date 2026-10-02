using System.Text.Json;
using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Exact wire values documented for CallEntry.type; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#CallEntry.type" />
[JsonConverter(typeof(CallTypeJsonConverter))]
public readonly record struct CallType(string Value)
{
    public static CallType Missed => new("missed");
    public static CallType Accepted => new("accepted");
    public static CallType Outgoing => new("outgoing");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class CallTypeJsonConverter : JsonConverter<CallType>
{
    public override CallType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for CallType.");

    public override void Write(Utf8JsonWriter writer, CallType value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for ContactNumber.type; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#ContactNumber.type" />
[JsonConverter(typeof(ContactNumberTypeJsonConverter))]
public readonly record struct ContactNumberType(string Value)
{
    public static ContactNumberType Fixed => new("fixed");
    public static ContactNumberType Mobile => new("mobile");
    public static ContactNumberType Work => new("work");
    public static ContactNumberType Fax => new("fax");
    public static ContactNumberType Other => new("other");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class ContactNumberTypeJsonConverter : JsonConverter<ContactNumberType>
{
    public override ContactNumberType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for ContactNumberType.");

    public override void Write(Utf8JsonWriter writer, ContactNumberType value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for ContactAddress.type; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#ContactAddress.type" />
[JsonConverter(typeof(ContactAddressTypeJsonConverter))]
public readonly record struct ContactAddressType(string Value)
{
    public static ContactAddressType Home => new("home");
    public static ContactAddressType Work => new("work");
    public static ContactAddressType Other => new("other");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class ContactAddressTypeJsonConverter : JsonConverter<ContactAddressType>
{
    public override ContactAddressType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for ContactAddressType.");

    public override void Write(Utf8JsonWriter writer, ContactAddressType value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for ContactUrl.type; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#ContactUrl.type" />
[JsonConverter(typeof(ContactUrlTypeJsonConverter))]
public readonly record struct ContactUrlType(string Value)
{
    public static ContactUrlType Profile => new("profile");
    public static ContactUrlType Blog => new("blog");
    public static ContactUrlType Site => new("site");
    public static ContactUrlType Other => new("other");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class ContactUrlTypeJsonConverter : JsonConverter<ContactUrlType>
{
    public override ContactUrlType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for ContactUrlType.");

    public override void Write(Utf8JsonWriter writer, ContactUrlType value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for ContactEmail.type; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#ContactEmail.type" />
[JsonConverter(typeof(ContactEmailTypeJsonConverter))]
public readonly record struct ContactEmailType(string Value)
{
    public static ContactEmailType Home => new("home");
    public static ContactEmailType Work => new("work");
    public static ContactEmailType Other => new("other");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class ContactEmailTypeJsonConverter : JsonConverter<ContactEmailType>
{
    public override ContactEmailType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for ContactEmailType.");

    public override void Write(Utf8JsonWriter writer, ContactEmailType value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for AfpConfig.server_type; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#AfpConfig.server_type" />
[JsonConverter(typeof(AfpServerTypeJsonConverter))]
public readonly record struct AfpServerType(string Value)
{
    public static AfpServerType Powerbook => new("powerbook");
    public static AfpServerType Powermac => new("powermac");
    public static AfpServerType Macmini => new("macmini");
    public static AfpServerType Imac => new("imac");
    public static AfpServerType Macbook => new("macbook");
    public static AfpServerType Macbookpro => new("macbookpro");
    public static AfpServerType Macbookair => new("macbookair");
    public static AfpServerType Macpro => new("macpro");
    public static AfpServerType Appletv => new("appletv");
    public static AfpServerType Airport => new("airport");
    public static AfpServerType Xserve => new("xserve");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class AfpServerTypeJsonConverter : JsonConverter<AfpServerType>
{
    public override AfpServerType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for AfpServerType.");

    public override void Write(Utf8JsonWriter writer, AfpServerType value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for VPNServer.type; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNServer.type" />
[JsonConverter(typeof(VpnServerTypeJsonConverter))]
public readonly record struct VpnServerType(string Value)
{
    public static VpnServerType Ipsec => new("ipsec");
    public static VpnServerType Pptp => new("pptp");
    public static VpnServerType OpenVpn => new("openvpn");
    public static VpnServerType WireGuard => new("wireguard");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class VpnServerTypeJsonConverter : JsonConverter<VpnServerType>
{
    public override VpnServerType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for VpnServerType.");

    public override void Write(Utf8JsonWriter writer, VpnServerType value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for VPNServer.state; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNServer.state" />
[JsonConverter(typeof(VpnServerStateJsonConverter))]
public readonly record struct VpnServerState(string Value)
{
    public static VpnServerState Stopped => new("stopped");
    public static VpnServerState Starting => new("starting");
    public static VpnServerState Started => new("started");
    public static VpnServerState Stopping => new("stopping");
    public static VpnServerState Error => new("error");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class VpnServerStateJsonConverter : JsonConverter<VpnServerState>
{
    public override VpnServerState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for VpnServerState.");

    public override void Write(Utf8JsonWriter writer, VpnServerState value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for VPNPPTPConfig.mppe; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNPPTPConfig.mppe" />
[JsonConverter(typeof(VpnMppeModeJsonConverter))]
public readonly record struct VpnMppeMode(string Value)
{
    public static VpnMppeMode Disable => new("disable");
    public static VpnMppeMode Require => new("require");
    public static VpnMppeMode Require128 => new("require_128");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class VpnMppeModeJsonConverter : JsonConverter<VpnMppeMode>
{
    public override VpnMppeMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for VpnMppeMode.");

    public override void Write(Utf8JsonWriter writer, VpnMppeMode value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for VPNOpenVpnConfig.cipher; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNOpenVpnConfig.cipher" />
[JsonConverter(typeof(VpnCipherJsonConverter))]
public readonly record struct VpnCipher(string Value)
{
    public static VpnCipher Blowfish => new("blowfish");
    public static VpnCipher Aes128 => new("aes128");
    public static VpnCipher Aes256 => new("aes256");
    public static VpnCipher Chacha20poly1305 => new("chacha20poly1305");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class VpnCipherJsonConverter : JsonConverter<VpnCipher>
{
    public override VpnCipher Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for VpnCipher.");

    public override void Write(Utf8JsonWriter writer, VpnCipher value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for VPNIPSecAuthMode.id_source; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNIPSecAuthMode.id_source" />
[JsonConverter(typeof(VpnIpSecIdSourceJsonConverter))]
public readonly record struct VpnIpSecIdSource(string Value)
{
    public static VpnIpSecIdSource Custom => new("custom");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class VpnIpSecIdSourceJsonConverter : JsonConverter<VpnIpSecIdSource>
{
    public override VpnIpSecIdSource Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for VpnIpSecIdSource.");

    public override void Write(Utf8JsonWriter writer, VpnIpSecIdSource value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for VPNUser.type; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNUser.type" />
[JsonConverter(typeof(VpnUserTypeJsonConverter))]
public readonly record struct VpnUserType(string Value)
{
    public static VpnUserType Standard => new("standard");
    public static VpnUserType WireGuard => new("wireguard");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class VpnUserTypeJsonConverter : JsonConverter<VpnUserType>
{
    public override VpnUserType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for VpnUserType.");

    public override void Write(Utf8JsonWriter writer, VpnUserType value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for VPNClientConfig.type; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNClientConfig.type" />
[JsonConverter(typeof(VpnClientTypeJsonConverter))]
public readonly record struct VpnClientType(string Value)
{
    public static VpnClientType Pptp => new("pptp");
    public static VpnClientType OpenVpn => new("openvpn");
    public static VpnClientType WireGuard => new("wireguard");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class VpnClientTypeJsonConverter : JsonConverter<VpnClientType>
{
    public override VpnClientType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for VpnClientType.");

    public override void Write(Utf8JsonWriter writer, VpnClientType value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for VPNClientStatus.state; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.state" />
[JsonConverter(typeof(VpnClientStateJsonConverter))]
public readonly record struct VpnClientState(string Value)
{
    public static VpnClientState WaitingWan => new("waiting_wan");
    public static VpnClientState GoingUp => new("going_up");
    public static VpnClientState Up => new("up");
    public static VpnClientState GoingDown => new("going_down");
    public static VpnClientState Down => new("down");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class VpnClientStateJsonConverter : JsonConverter<VpnClientState>
{
    public override VpnClientState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for VpnClientState.");

    public override void Write(Utf8JsonWriter writer, VpnClientState value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for VPNClientStatus.last_error; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.last_error" />
[JsonConverter(typeof(VpnClientErrorJsonConverter))]
public readonly record struct VpnClientError(string Value)
{
    public static VpnClientError None => new("none");
    public static VpnClientError Internal => new("internal");
    public static VpnClientError AuthenticationFailed => new("authentication_failed");
    public static VpnClientError AuthFailed => new("auth_failed");
    public static VpnClientError ResolvFailed => new("resolv_failed");
    public static VpnClientError ConnectTimeout => new("connect_timeout");
    public static VpnClientError ConnectFailed => new("connect_failed");
    public static VpnClientError SetupControlFailed => new("setup_control_failed");
    public static VpnClientError SetupCallFailed => new("setup_call_failed");
    public static VpnClientError Protocol => new("protocol");
    public static VpnClientError RemoteTerminated => new("remote_terminated");
    public static VpnClientError RemoteDisconnect => new("remote_disconnect");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class VpnClientErrorJsonConverter : JsonConverter<VpnClientError>
{
    public override VpnClientError Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for VpnClientError.");

    public override void Write(Utf8JsonWriter writer, VpnClientError value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for VpnClientIpInfo.provider; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VpnClientIpInfo.provider" />
[JsonConverter(typeof(VpnIpProviderJsonConverter))]
public readonly record struct VpnIpProvider(string Value)
{
    public static VpnIpProvider None => new("none");
    public static VpnIpProvider Static => new("static");
    public static VpnIpProvider Ppp => new("ppp");
    public static VpnIpProvider Dhcp => new("dhcp");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class VpnIpProviderJsonConverter : JsonConverter<VpnIpProvider>
{
    public override VpnIpProvider Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for VpnIpProvider.");

    public override void Write(Utf8JsonWriter writer, VpnIpProvider value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for Precord.state; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#Precord.state" />
[JsonConverter(typeof(RecordingStateJsonConverter))]
public readonly record struct RecordingState(string Value)
{
    public static RecordingState Disabled => new("disabled");
    public static RecordingState StartError => new("start_error");
    public static RecordingState WaitingStartTime => new("waiting_start_time");
    public static RecordingState Starting => new("starting");
    public static RecordingState Running => new("running");
    public static RecordingState RunningError => new("running_error");
    public static RecordingState Failed => new("failed");
    public static RecordingState Finished => new("finished");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class RecordingStateJsonConverter : JsonConverter<RecordingState>
{
    public override RecordingState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for RecordingState.");

    public override void Write(Utf8JsonWriter writer, RecordingState value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for Precord.error; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#Precord.error" />
[JsonConverter(typeof(RecordingErrorJsonConverter))]
public readonly record struct RecordingError(string Value)
{
    public static RecordingError None => new("none");
    public static RecordingError FileAccessError => new("file_access_error");
    public static RecordingError DiskFull => new("disk_full");
    public static RecordingError PrivateButNoPrivateDir => new("private_but_no_private_dir");
    public static RecordingError NetworkProblem => new("network_problem");
    public static RecordingError ResourceProblem => new("resource_problem");
    public static RecordingError NoStreamAvailable => new("no_stream_available");
    public static RecordingError NoDataReceived => new("no_data_received");
    public static RecordingError Missed => new("missed");
    public static RecordingError Stopped => new("stopped");
    public static RecordingError InternalError => new("internal_error");
    public static RecordingError UnknownError => new("unknown_error");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class RecordingErrorJsonConverter : JsonConverter<RecordingError>
{
    public override RecordingError Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for RecordingError.");

    public override void Write(Utf8JsonWriter writer, RecordingError value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for Precord.channel_quality; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#Precord.channel_quality" />
[JsonConverter(typeof(RecordingQualityJsonConverter))]
public readonly record struct RecordingQuality(string Value)
{
    public static RecordingQuality Auto => new("auto");
    public static RecordingQuality Hd => new("hd");
    public static RecordingQuality Sd => new("sd");
    public static RecordingQuality Ld => new("ld");
    public static RecordingQuality ThreeD => new("3d");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class RecordingQualityJsonConverter : JsonConverter<RecordingQuality>
{
    public override RecordingQuality Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for RecordingQuality.");

    public override void Write(Utf8JsonWriter writer, RecordingQuality value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for Precord.channel_type; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#Precord.channel_type" />
[JsonConverter(typeof(RecordingChannelTypeJsonConverter))]
public readonly record struct RecordingChannelType(string Value)
{
    public static RecordingChannelType Auto => new("");
    public static RecordingChannelType Iptv => new("iptv");
    public static RecordingChannelType Dvb => new("dvb");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class RecordingChannelTypeJsonConverter : JsonConverter<RecordingChannelType>
{
    public override RecordingChannelType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for RecordingChannelType.");

    public override void Write(Utf8JsonWriter writer, RecordingChannelType value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for Precord.broadcast_type; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#Precord.broadcast_type" />
[JsonConverter(typeof(RecordingBroadcastTypeJsonConverter))]
public readonly record struct RecordingBroadcastType(string Value)
{
    public static RecordingBroadcastType Tv => new("tv");
    public static RecordingBroadcastType Radio => new("radio");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class RecordingBroadcastTypeJsonConverter : JsonConverter<RecordingBroadcastType>
{
    public override RecordingBroadcastType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for RecordingBroadcastType.");

    public override void Write(Utf8JsonWriter writer, RecordingBroadcastType value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Exact wire values documented for PlayerMediaCommand.cmd; unknown incoming strings are preserved.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#post--api-v8-player-id_player-api-v6-control-mediactrl-" />
[JsonConverter(typeof(PlayerMediaCommandNameJsonConverter))]
public readonly record struct PlayerMediaCommandName(string Value)
{
    public static PlayerMediaCommandName PlayPause => new("play_pause");
    public static PlayerMediaCommandName Stop => new("stop");
    public static PlayerMediaCommandName Prev => new("prev");
    public static PlayerMediaCommandName Next => new("next");
    public static PlayerMediaCommandName SelectStream => new("select_stream");
    public static PlayerMediaCommandName SelectAudioTrack => new("select_audio_track");
    public static PlayerMediaCommandName SelectSrtTrack => new("select_srt_track");

    public override string ToString() => Value ?? string.Empty;
}

public sealed class PlayerMediaCommandNameJsonConverter : JsonConverter<PlayerMediaCommandName>
{
    public override PlayerMediaCommandName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType == JsonTokenType.String ? new(reader.GetString()!)
            : throw new JsonException("Expected a string for PlayerMediaCommandName.");

    public override void Write(Utf8JsonWriter writer, PlayerMediaCommandName value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An uninitialized wire value cannot be serialized.");
        writer.WriteStringValue(value.Value);
    }
}
