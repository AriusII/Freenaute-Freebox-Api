namespace Freenaute.Freebox.Mapper.Common.Routes;

public readonly partial struct FreeboxRoutes
{
    public static readonly string GetAirMediaConfig = "/airmedia/config";
    public static readonly string UpdateAirMediaConfig = "/airmedia/config";
    public static readonly string GetAirMediaReceivers = "/airmedia/receivers";
    public static readonly string InteractWithAirMediaReceiver = "/airmedia/receivers/{receiver_name}";
}
