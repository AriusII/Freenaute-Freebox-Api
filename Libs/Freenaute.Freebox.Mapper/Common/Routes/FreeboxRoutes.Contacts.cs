namespace Freenaute.Freebox.Mapper.Common.Routes;

public readonly partial struct FreeboxRoutes
{
    public static readonly string GetContacts = "/contact";
    public static readonly string GetContactById = "/contact/{id}";
    public static readonly string CreateContact = "/contact";
    public static readonly string DeleteContactById = "/contact/{id}";
    public static readonly string UpdateContactById = "/contact/{id}";
}
