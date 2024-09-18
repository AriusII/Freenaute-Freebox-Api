namespace Freenaute.Freebox.Mapper.Common.Routes;

public readonly partial struct FreeboxRoutes
{
    public static readonly string GetVoicemailEntries = "/call/voicemail";
    public static readonly string GetVoicemailEntryById = "/call/voicemail/{id}";
    public static readonly string DeleteVoicemailById = "/call/voicemail/{id}";
    public static readonly string UpdateVoicemailEntryById = "/call/voicemail/{id}";
    public static readonly string GetVoicemailAudioById = "/call/voicemail/{id}/audio_file";
}
