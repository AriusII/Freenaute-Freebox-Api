using Microsoft.Extensions.DependencyInjection;

namespace Freenaute.Freebox.Client;

public static class FreeboxServiceCollectionExtensions
{
    /// <summary>
    /// Registers a transient facade, an HTTP client factory, and session state shared for one configured Freebox.
    /// The returned standard builder supports HTTP configuration and delegating handlers.
    /// </summary>
    public static IHttpClientBuilder AddFreeboxClient(this IServiceCollection services,
        Action<FreeboxClientOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);
        if (services.Any(descriptor => descriptor.ServiceType == typeof(FreeboxClientState)))
        {
            throw new InvalidOperationException("AddFreeboxClient registers one Freebox per service collection and cannot be called twice.");
        }

        var options = new FreeboxClientOptions();
        configure(options);
        var snapshot = options.CreateValidatedSnapshot();
        services.AddSingleton(_ => new FreeboxClientState(snapshot));
        return services.AddHttpClient("Freenaute.Freebox.Client", client =>
            {
                client.BaseAddress = snapshot.ServerAddress;
                client.Timeout = snapshot.Timeout;
            })
            .ConfigurePrimaryHttpMessageHandler(() => FreeboxTls.CreateHandler())
            .RedactLoggedHeaders(["X-Fbx-App-Auth", "Authorization", "Cookie", "Set-Cookie"])
            .AddTypedClient<IFreeboxClient>((httpClient, provider) =>
                new FreeboxClient(httpClient, provider.GetRequiredService<FreeboxClientState>()));
    }
}
