namespace Freenaute.Freebox.Mapper.Common.Routes;

public readonly partial struct FreeboxRoutes
{
    public const string GetCallLog = "/call/log";
    public const string DeleteAllCalls = "/call/log/delete_all";
    public const string MarkAllCallsAsRead = "/call/log/mark_all_as_read";
    public const string GetCallEntryById = "/call/log/{id}";
    public const string DeleteCallById = "/call/log/{id}";
    public const string UpdateCallEntryById = "/call/log/{id}";
}
