namespace Freenaute.Freebox.Client.Domains.Files;

/// <summary>Files, downloads, storage and VM HTTP resources using one shared authenticated transport.</summary>
public interface IFreeboxFilesApi
{
    FreeboxDownloadsApi Downloads { get; }
    FreeboxFileSystemApi FileSystem { get; }
    FreeboxSharesApi Shares { get; }
    FreeboxUploadsApi Uploads { get; }
    FreeboxStorageApi Storage { get; }
    FreeboxRaidApi Raid { get; }
    FreeboxVirtualMachinesApi VirtualMachines { get; }
    FreeboxStatisticsApi Statistics { get; }
}

public sealed class FreeboxFilesApi : IFreeboxFilesApi
{
    public FreeboxFilesApi(IFreeboxTransport transport, IFreeboxBinaryTransport binaryTransport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        ArgumentNullException.ThrowIfNull(binaryTransport);
        Downloads = new(transport);
        FileSystem = new(transport, binaryTransport);
        Shares = new(transport);
        Uploads = new(transport);
        Storage = new(transport);
        Raid = new(transport);
        VirtualMachines = new(transport);
        Statistics = new(transport);
    }

    public FreeboxDownloadsApi Downloads { get; }
    public FreeboxFileSystemApi FileSystem { get; }
    public FreeboxSharesApi Shares { get; }
    public FreeboxUploadsApi Uploads { get; }
    public FreeboxStorageApi Storage { get; }
    public FreeboxRaidApi Raid { get; }
    public FreeboxVirtualMachinesApi VirtualMachines { get; }
    public FreeboxStatisticsApi Statistics { get; }
}
