using System.Text.Json.Serialization.Metadata;
using Freenaute.Freebox.Mapper.Contracts.Services;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Services;

public sealed class FreeboxContactsApi(IFreeboxTransport transport)
{
    public Task<ContactEntry[]> GetAllAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "contact/", ServicesJsonSerializerContext.Default.ContactEntryArray, cancellationToken);
    public FreeboxContact Contact(long id) => new(transport, id);
    public Task<ContactEntry> CreateAsync(ContactWriteRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();
        return transport.SendAsync(HttpMethod.Post, "contact/", request, ServicesJsonSerializerContext.Default.ContactWriteRequest,
            ServicesJsonSerializerContext.Default.ContactEntry, cancellationToken);
    }
    public FreeboxServiceCommand<ContactWriteRequest, ContactEntry> Create() => new(new(), CreateAsync);
}

public sealed class FreeboxContact
{
    private readonly FreeboxServiceResource<ContactEntry, ContactWriteRequest> _resource;
    internal FreeboxContact(IFreeboxTransport transport, long id)
    {
        var segment = ServicesPath.Id(id);
        _resource = new(transport, $"contact/{segment}", ServicesJsonSerializerContext.Default.ContactEntry,
            ServicesJsonSerializerContext.Default.ContactWriteRequest, request => request.Validate());
        Numbers = new(transport, $"contact/{segment}/numbers/", "number/", ServicesJsonSerializerContext.Default.ContactNumberArray,
            ServicesJsonSerializerContext.Default.ContactNumber, ServicesJsonSerializerContext.Default.ContactNumberWriteRequest,
            request => request.Validate(), request => request with { ContactId = id });
        Addresses = new(transport, $"contact/{segment}/addresses/", "address/", ServicesJsonSerializerContext.Default.ContactAddressArray,
            ServicesJsonSerializerContext.Default.ContactAddress, ServicesJsonSerializerContext.Default.ContactAddressWriteRequest,
            request => request.Validate(), request => request with { ContactId = id });
        Urls = new(transport, $"contact/{segment}/urls/", "url/", ServicesJsonSerializerContext.Default.ContactUrlArray,
            ServicesJsonSerializerContext.Default.ContactUrl, ServicesJsonSerializerContext.Default.ContactUrlWriteRequest,
            request => request.Validate(), request => request with { ContactId = id });
        Emails = new(transport, $"contact/{segment}/emails/", "email/", ServicesJsonSerializerContext.Default.ContactEmailArray,
            ServicesJsonSerializerContext.Default.ContactEmail, ServicesJsonSerializerContext.Default.ContactEmailWriteRequest,
            request => request.Validate(), request => request with { ContactId = id });
    }
    public FreeboxContactItems<ContactNumber, ContactNumberWriteRequest> Numbers { get; }
    public FreeboxContactItems<ContactAddress, ContactAddressWriteRequest> Addresses { get; }
    public FreeboxContactItems<ContactUrl, ContactUrlWriteRequest> Urls { get; }
    public FreeboxContactItems<ContactEmail, ContactEmailWriteRequest> Emails { get; }
    public Task<ContactEntry> GetAsync(CancellationToken cancellationToken = default) => _resource.GetAsync(cancellationToken);
    public Task<ContactEntry> UpdateAsync(ContactWriteRequest request, CancellationToken cancellationToken = default) => _resource.UpdateAsync(request, cancellationToken);
    public Task DeleteAsync(CancellationToken cancellationToken = default) => _resource.DeleteAsync(cancellationToken);
    public FreeboxServiceCommand<ContactWriteRequest, ContactEntry> Configure() => _resource.Configure();
}

/// <summary>A contact's item collection. Individual item routes are the documented top-level number/address/url/email routes.</summary>
public sealed class FreeboxContactItems<TRead, TWrite> where TWrite : class, new()
{
    private readonly IFreeboxTransport _transport;
    private readonly string _listPath;
    private readonly string _itemRoot;
    private readonly JsonTypeInfo<TRead[]> _listType;
    private readonly JsonTypeInfo<TRead> _readType;
    private readonly JsonTypeInfo<TWrite> _writeType;
    private readonly Action<TWrite> _validate;
    private readonly Func<TWrite, TWrite> _bindContact;
    internal FreeboxContactItems(IFreeboxTransport transport, string listPath, string itemRoot,
        JsonTypeInfo<TRead[]> listType, JsonTypeInfo<TRead> readType, JsonTypeInfo<TWrite> writeType,
        Action<TWrite> validate, Func<TWrite, TWrite> bindContact)
    {
        _transport = transport; _listPath = listPath; _itemRoot = itemRoot; _listType = listType; _readType = readType;
        _writeType = writeType; _validate = validate; _bindContact = bindContact;
    }
    public Task<TRead[]> GetAllAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Get, _listPath, _listType, cancellationToken);
    public FreeboxServiceResource<TRead, TWrite> Item(long id) =>
        new(_transport, _itemRoot + ServicesPath.Id(id), _readType, _writeType, _validate);
    public Task<TRead> CreateAsync(TWrite request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        var bound = _bindContact(request);
        _validate(bound);
        return _transport.SendAsync(HttpMethod.Post, _itemRoot, bound, _writeType, _readType, cancellationToken);
    }
    public FreeboxServiceCommand<TWrite, TRead> Create() => new(new(), CreateAsync);
}
