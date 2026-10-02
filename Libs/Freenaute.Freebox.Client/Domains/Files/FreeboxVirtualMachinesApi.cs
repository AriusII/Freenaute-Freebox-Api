using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Files;

public sealed class FreeboxVirtualMachinesApi(IFreeboxTransport transport)
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vm-info-</summary>
    public Task<VmSystemInfo> GetSystemInformationAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "vm/info/", FilesJsonSerializerContext.Default.VmSystemInfo, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vm-distros-</summary>
    public Task<VmDistribution[]> GetDistributionsAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "vm/distros/", FilesJsonSerializerContext.Default.VmDistributionArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vm-</summary>
    public Task<VirtualMachine[]> GetAllAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "vm/", FilesJsonSerializerContext.Default.VirtualMachineArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vm-id</summary>
    public Task<VirtualMachine> GetAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"vm/{FilesPath.Id(id)}", FilesJsonSerializerContext.Default.VirtualMachine, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-</summary>
    public Task CreateAsync(CreateVirtualMachineRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "vm/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.CreateVirtualMachineRequest, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-vm-id</summary>
    public Task DeleteAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, $"vm/{FilesPath.Id(id)}", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-id-start</summary>
    public Task StartAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, $"vm/{FilesPath.Id(id)}/start", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-id-powerbutton</summary>
    public Task PressPowerButtonAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, $"vm/{FilesPath.Id(id)}/powerbutton", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-id-stop</summary>
    public Task StopAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, $"vm/{FilesPath.Id(id)}/stop", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-id-restart</summary>
    public Task RestartAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, $"vm/{FilesPath.Id(id)}/restart", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-disk-info</summary>
    public Task<VmDiskInfo> GetDiskInformationAsync(VmDiskInformationRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "vm/disk/info", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.VmDiskInformationRequest, FilesJsonSerializerContext.Default.VmDiskInfo, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vm-disk-task-id</summary>
    public Task<VmDiskTask> GetDiskTaskAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"vm/disk/task/{FilesPath.Id(id)}", FilesJsonSerializerContext.Default.VmDiskTask, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-vm-disk-task-id</summary>
    public Task DeleteDiskTaskAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, $"vm/disk/task/{FilesPath.Id(id)}", cancellationToken);

}
