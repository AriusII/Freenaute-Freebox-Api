namespace Freenaute.Freebox.Mapper.Common.Routes;

public readonly partial struct FreeboxRoutes
{
    public static readonly string GetContactNumbers = "/contact/{contact_id}/numbers";
    public static readonly string GetContactNumberById = "/number/{id}";
    public static readonly string CreateContactNumber = "/number";
    public static readonly string DeleteContactNumberById = "/number/{id}";
    public static readonly string UpdateContactNumberById = "/number/{id}";
}
