using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Client.Domains.Files;

internal static class FilesRequestValidator
{
    public static AddDownloadBlacklistRequest Validate(AddDownloadBlacklistRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Host);
        return request;
    }

    public static AddDownloadTrackerRequest Validate(AddDownloadTrackerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Announce);
        return request;
    }

    public static ArchiveFilesRequest Validate(ArchiveFilesRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Files);
        foreach (var path in request.Files) _ = path.Value;
        _ = request.Dst.Value;
        return request;
    }

    public static CheckPartitionRequest Validate(CheckPartitionRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (!Enum.IsDefined(request.Checkmode)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid Checkmode value.");
        return request;
    }

    public static ConcatenateFilesRequest Validate(ConcatenateFilesRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Files);
        foreach (var path in request.Files) _ = path.Value;
        _ = request.Dst.Value;
        RejectNull(request.MultiVolumes, "MultiVolumes");
        RejectNull(request.DeleteFiles, "DeleteFiles");
        RejectNull(request.Overwrite, "Overwrite");
        RejectNull(request.Append, "Append");
        return request;
    }

    public static CreateDirectoryRequest Validate(CreateDirectoryRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        _ = request.Parent.Value;
        ArgumentNullException.ThrowIfNull(request.Dirname);
        return request;
    }

    public static CreateDownloadFeedRequest Validate(CreateDownloadFeedRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Url);
        return request;
    }

    public static CreateRaidRequest Validate(CreateRaidRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (!Enum.IsDefined(request.Level)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid Level value.");
        ArgumentNullException.ThrowIfNull(request.Name);
        ArgumentNullException.ThrowIfNull(request.Members);
        return request;
    }

    public static CreateShareLinkRequest Validate(CreateShareLinkRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Path);
        return request;
    }

    public static CreateVirtualMachineRequest Validate(CreateVirtualMachineRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Name, "Name");
        RejectNull(request.DiskPath, "DiskPath");
        if (request.DiskPath.HasValue) _ = request.DiskPath.Value.Value;
        RejectNull(request.DiskType, "DiskType");
        if (request.DiskType.HasValue && !Enum.IsDefined(request.DiskType.Value)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid DiskType value.");
        RejectNull(request.CdPath, "CdPath");
        if (request.CdPath.HasValue) _ = request.CdPath.Value.Value;
        RejectNull(request.Memory, "Memory");
        RejectNull(request.Vcpus, "Vcpus");
        RejectNull(request.EnableScreen, "EnableScreen");
        RejectNull(request.BindUsbPorts, "BindUsbPorts");
        RejectNull(request.EnableCloudinit, "EnableCloudinit");
        RejectNull(request.CloudinitHostname, "CloudinitHostname");
        RejectNull(request.CloudinitUserdata, "CloudinitUserdata");
        RejectNull(request.Os, "Os");
        if (request.Name.HasValue && request.Name.Value.Length > 31) throw new ArgumentException("Name exceeds its documented length.", nameof(request));
        if (request.CloudinitHostname.HasValue && request.CloudinitHostname.Value.Length > 59) throw new ArgumentException("CloudinitHostname exceeds its documented length.", nameof(request));
        if (request.CloudinitUserdata.HasValue && request.CloudinitUserdata.Value.Length > 32767) throw new ArgumentException("CloudinitUserdata exceeds its documented length.", nameof(request));
        return request;
    }

    public static DownloadFilePriorityRequest Validate(DownloadFilePriorityRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (!Enum.IsDefined(request.Priority)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid Priority value.");
        return request;
    }

    public static ExtractFilesRequest Validate(ExtractFilesRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        _ = request.Src.Value;
        _ = request.Dst.Value;
        RejectNull(request.Password, "Password");
        RejectNull(request.DeleteArchive, "DeleteArchive");
        RejectNull(request.Overwrite, "Overwrite");
        return request;
    }

    public static FetchRrdRequest Validate(FetchRrdRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (!Enum.IsDefined(request.Db)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid Db value.");
        RejectNull(request.DateStart, "DateStart");
        RejectNull(request.DateEnd, "DateEnd");
        RejectNull(request.Precision, "Precision");
        RejectNull(request.Fields, "Fields");
        return request;
    }

    public static FileTransferRequest Validate(FileTransferRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Files);
        foreach (var path in request.Files) _ = path.Value;
        _ = request.Dst.Value;
        if (!Enum.IsDefined(request.Mode)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid Mode value.");
        return request;
    }

    public static FormatStorageDiskRequest Validate(FormatStorageDiskRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (!Enum.IsDefined(request.TableType)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid TableType value.");
        if (!Enum.IsDefined(request.FsType)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid FsType value.");
        ArgumentNullException.ThrowIfNull(request.Label);
        return request;
    }

    public static HashFileRequest Validate(HashFileRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        _ = request.Src.Value;
        ArgumentNullException.ThrowIfNull(request.HashType);
        return request;
    }

    public static MarkDownloadFeedItemRequest Validate(MarkDownloadFeedItemRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return request;
    }

    public static RemoveFilesRequest Validate(RemoveFilesRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Files);
        foreach (var path in request.Files) _ = path.Value;
        return request;
    }

    public static RenameFileRequest Validate(RenameFileRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        _ = request.Src.Value;
        ArgumentNullException.ThrowIfNull(request.Dst);
        return request;
    }

    public static RepairFilesRequest Validate(RepairFilesRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        _ = request.Src.Value;
        RejectNull(request.DeleteArchive, "DeleteArchive");
        return request;
    }

    public static SetDownloadThrottlingRequest Validate(SetDownloadThrottlingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (!Enum.IsDefined(request.Throttling)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid Throttling value.");
        return request;
    }

    public static SetFileTaskStateRequest Validate(SetFileTaskStateRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (!Enum.IsDefined(request.State)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid State value.");
        return request;
    }

    public static SetPartitionStateRequest Validate(SetPartitionStateRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (!Enum.IsDefined(request.State)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid State value.");
        return request;
    }

    public static SetRaidStateRequest Validate(SetRaidStateRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (!Enum.IsDefined(request.State)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid State value.");
        return request;
    }

    public static SetStorageDiskStateRequest Validate(SetStorageDiskStateRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (!Enum.IsDefined(request.State)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid State value.");
        if (request.State is not StorageDiskState.Enabled and not StorageDiskState.Disabled) throw new ArgumentException("Only enabling or disabling a disk is documented.", nameof(request));
        return request;
    }

    public static UpdateDlBtConfig Validate(UpdateDlBtConfig request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.MaxPeers, "MaxPeers");
        RejectNull(request.StopRatio, "StopRatio");
        RejectNull(request.CryptoSupport, "CryptoSupport");
        if (request.CryptoSupport.HasValue && !Enum.IsDefined(request.CryptoSupport.Value)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid CryptoSupport value.");
        RejectNull(request.EnableDht, "EnableDht");
        RejectNull(request.EnablePex, "EnablePex");
        RejectNull(request.AnnounceTimeout, "AnnounceTimeout");
        RejectNull(request.MainPort, "MainPort");
        RejectNull(request.DhtPort, "DhtPort");
        return request;
    }

    public static UpdateDlFeedConfig Validate(UpdateDlFeedConfig request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.FetchInterval, "FetchInterval");
        RejectNull(request.MaxItems, "MaxItems");
        return request;
    }

    public static UpdateDlNewsConfig Validate(UpdateDlNewsConfig request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Server, "Server");
        RejectNull(request.Port, "Port");
        RejectNull(request.Ssl, "Ssl");
        RejectNull(request.User, "User");
        RejectNull(request.Password, "Password");
        RejectNull(request.Nthreads, "Nthreads");
        RejectNull(request.AutoRepair, "AutoRepair");
        RejectNull(request.LazyPar2, "LazyPar2");
        RejectNull(request.AutoExtract, "AutoExtract");
        RejectNull(request.EraseTmp, "EraseTmp");
        return request;
    }

    public static UpdateDlRate Validate(UpdateDlRate request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.TxRate, "TxRate");
        RejectNull(request.RxRate, "RxRate");
        return request;
    }

    public static UpdateDlThrottlingConfig Validate(UpdateDlThrottlingConfig request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Normal, "Normal");
        if (request.Normal.HasValue) Validate(request.Normal.Value);
        RejectNull(request.Slow, "Slow");
        if (request.Slow.HasValue) Validate(request.Slow.Value);
        RejectNull(request.Schedule, "Schedule");
        if (request.Schedule.HasValue && (request.Schedule.Value.Length != 168 || request.Schedule.Value.Any(value => !Enum.IsDefined(value)))) throw new ArgumentException("Schedule must contain 168 documented mode values.", nameof(request));
        RejectNull(request.Mode, "Mode");
        if (request.Mode.HasValue && !Enum.IsDefined(request.Mode.Value)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid Mode value.");
        return request;
    }

    public static UpdateDownloadConfiguration Validate(UpdateDownloadConfiguration request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.MaxDownloadingTasks, "MaxDownloadingTasks");
        RejectNull(request.DownloadDir, "DownloadDir");
        if (request.DownloadDir.HasValue) _ = request.DownloadDir.Value.Value;
        RejectNull(request.WatchDir, "WatchDir");
        if (request.WatchDir.HasValue) _ = request.WatchDir.Value.Value;
        RejectNull(request.UseWatchDir, "UseWatchDir");
        RejectNull(request.Throttling, "Throttling");
        if (request.Throttling.HasValue) Validate(request.Throttling.Value);
        RejectNull(request.News, "News");
        if (request.News.HasValue) Validate(request.News.Value);
        RejectNull(request.Bt, "Bt");
        if (request.Bt.HasValue) Validate(request.Bt.Value);
        RejectNull(request.Feed, "Feed");
        if (request.Feed.HasValue) Validate(request.Feed.Value);
        RejectNull(request.Dns1, "Dns1");
        RejectNull(request.Dns2, "Dns2");
        return request;
    }

    public static UpdateDownloadFeedRequest Validate(UpdateDownloadFeedRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return request;
    }

    public static UpdateDownloadRequest Validate(UpdateDownloadRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Status, "Status");
        if (request.Status.HasValue && !Enum.IsDefined(request.Status.Value)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid Status value.");
        RejectNull(request.QueuePos, "QueuePos");
        RejectNull(request.IoPriority, "IoPriority");
        if (request.IoPriority.HasValue && !Enum.IsDefined(request.IoPriority.Value)) throw new ArgumentOutOfRangeException(nameof(request), "Invalid IoPriority value.");
        RejectNull(request.ArchivePassword, "ArchivePassword");
        return request;
    }

    public static UpdateDownloadTrackerRequest Validate(UpdateDownloadTrackerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Announce, "Announce");
        RejectNull(request.IsEnabled, "IsEnabled");
        return request;
    }

    public static UpdateRaidMembersRequest Validate(UpdateRaidMembersRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Members);
        return request;
    }

    public static UpdateStorageConfiguration Validate(UpdateStorageConfiguration request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.ExternalPmEnabled, "ExternalPmEnabled");
        RejectNull(request.ExternalPmIdleBeforeSpindown, "ExternalPmIdleBeforeSpindown");
        return request;
    }

    public static VmDiskInformationRequest Validate(VmDiskInformationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        _ = request.DiskPath.Value;
        return request;
    }

    public static EncodedFreeboxPath[] Validate(EncodedFreeboxPath[] paths)
    {
        ArgumentNullException.ThrowIfNull(paths);
        foreach (var path in paths) _ = path.Value;
        return paths;
    }

    private static void RejectNull<T>(Optional<T> value, string name)
    {
        if (value.IsNull) throw new ArgumentException("Null clearing is not documented; omit the field instead.", name);
    }
}
