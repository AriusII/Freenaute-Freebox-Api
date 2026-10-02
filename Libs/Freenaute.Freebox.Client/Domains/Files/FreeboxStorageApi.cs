using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Files;

public sealed class FreeboxStorageApi(IFreeboxTransport transport)
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-disk-</summary>
    public Task<StorageDisk[]> GetDisksAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "storage/disk/", FilesJsonSerializerContext.Default.StorageDiskArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-disk-id</summary>
    public Task<StorageDisk> GetDiskAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"storage/disk/{FilesPath.Id(id)}", FilesJsonSerializerContext.Default.StorageDisk, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-disk-id</summary>
    public Task<StorageDisk> SetDiskStateAsync(long id, SetStorageDiskStateRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, $"storage/disk/{FilesPath.Id(id)}", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.SetStorageDiskStateRequest, FilesJsonSerializerContext.Default.StorageDisk, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-disk-id-format-</summary>
    public Task FormatDiskAsync(long id, FormatStorageDiskRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, $"storage/disk/{FilesPath.Id(id)}/format/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.FormatStorageDiskRequest, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-partition-</summary>
    public Task<DiskPartition[]> GetPartitionsAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "storage/partition/", FilesJsonSerializerContext.Default.DiskPartitionArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-partition-id</summary>
    public Task<DiskPartition> GetPartitionAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"storage/partition/{FilesPath.Id(id)}", FilesJsonSerializerContext.Default.DiskPartition, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-partition-id</summary>
    public Task<DiskPartition> SetPartitionStateAsync(long id, SetPartitionStateRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, $"storage/partition/{FilesPath.Id(id)}", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.SetPartitionStateRequest, FilesJsonSerializerContext.Default.DiskPartition, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-partition-id-check-</summary>
    public Task CheckPartitionAsync(long id, CheckPartitionRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, $"storage/partition/{FilesPath.Id(id)}/check/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.CheckPartitionRequest, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-config-</summary>
    public Task<StorageConfig> GetConfigurationAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "storage/config/", FilesJsonSerializerContext.Default.StorageConfig, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-config-</summary>
    public Task<StorageConfig> UpdateConfigurationAsync(UpdateStorageConfiguration request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, "storage/config/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.UpdateStorageConfiguration, FilesJsonSerializerContext.Default.StorageConfig, cancellationToken);

    public Task<FileSystemAdvice> GetFileSystemAdviceAsync(long diskId, long? partitionId = null, bool? dedicatedDisk = null,
        CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, FilesPath.Advice(diskId, partitionId, dedicatedDisk),
            FilesJsonSerializerContext.Default.FileSystemAdvice, cancellationToken);
}
