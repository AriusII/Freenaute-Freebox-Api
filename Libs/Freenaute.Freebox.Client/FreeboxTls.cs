using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Freenaute.Freebox.Client;

/// <summary>Creates a Freebox HTTP handler with the certificate roots published by the Freebox Server SDK.</summary>
public static class FreeboxTls
{
    /// <summary>
    /// Creates an owned HTTP handler that validates certificate chains, server authentication and hostnames.
    /// Certificate revocation follows the default .NET HTTP policy unless explicitly configured.
    /// </summary>
    public static HttpMessageHandler CreateHandler(X509RevocationMode revocationMode = X509RevocationMode.NoCheck)
    {
        if (revocationMode is not X509RevocationMode.NoCheck and not X509RevocationMode.Online
            and not X509RevocationMode.Offline)
        {
            throw new ArgumentOutOfRangeException(nameof(revocationMode));
        }

        List<X509Certificate2> certificates = [];
        SocketsHttpHandler? handler = null;
        try
        {
            var policy = new X509ChainPolicy
            {
                TrustMode = X509ChainTrustMode.CustomRootTrust,
                VerificationFlags = X509VerificationFlags.NoFlag,
                RevocationMode = revocationMode
            };
            policy.ApplicationPolicy.Add(new Oid("1.3.6.1.5.5.7.3.1"));

            foreach (var root in FreeboxTlsCertificates.FrenchRoots)
            {
                var certificate = X509Certificate2.CreateFromPem(root.Pem);
                certificates.Add(certificate);
                var fingerprint = Convert.ToHexStringLower(SHA256.HashData(certificate.RawData));
                if (!StringComparer.Ordinal.Equals(fingerprint, root.Sha256))
                {
                    throw new CryptographicException("A Freebox certificate root does not match its verified SDK fingerprint.");
                }

                policy.CustomTrustStore.Add(certificate);
            }

            handler = new SocketsHttpHandler
            {
                AllowAutoRedirect = false,
                UseCookies = false,
                SslOptions = new SslClientAuthenticationOptions
                {
                    CertificateChainPolicy = policy
                }
            };
            return new CertificateOwnerHandler(handler, certificates.ToArray());
        }
        catch
        {
            handler?.Dispose();
            foreach (var certificate in certificates)
            {
                certificate.Dispose();
            }

            throw;
        }
    }

    private sealed class CertificateOwnerHandler(HttpMessageHandler innerHandler, X509Certificate2[] certificates)
        : DelegatingHandler(innerHandler)
    {
        private int disposed;

        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
            }
            finally
            {
                if (disposing && Interlocked.Exchange(ref disposed, 1) == 0)
                {
                    foreach (var certificate in certificates)
                    {
                        certificate.Dispose();
                    }
                }
            }
        }
    }
}
