namespace Freenaute.Freebox.Mapper.Common.Routes;

public readonly partial struct FreeboxRoutes
{
    public static readonly string Authorization = "/login/authorize";
    public static readonly string AuthorizeTrack = "/login/authorize/{0}";
    public static readonly string Login = "/login";
    public static readonly string Logout = "/login/logout";
    public static readonly string Session = "/session";
}
