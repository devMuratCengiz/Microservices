using Microservices.Web.Models;
using Microservices.Web.Services.Interfaces;
using IdentityModel.AspNetCore;
using IdentityModel.AspNetCore.AccessTokenManagement;
using Microsoft.Extensions.Options;
using IdentityModel.Client;

namespace Microservices.Web.Services
{
    public class ClientCredentialsTokenService : IClientCredentialsTokenService
    {
        private readonly ServiceApiSettings _settings;
        private readonly ClientSettings _clientSettings;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;
        private readonly HttpClient _httpClient;

        public ClientCredentialsTokenService(IOptions<ServiceApiSettings> settings, IOptions<ClientSettings> clientSettings, IClientAccessTokenCache clientAccessTokenCache, HttpClient httpClient)
        {
            _settings = settings.Value;
            _clientSettings = clientSettings.Value;
            _clientAccessTokenCache = clientAccessTokenCache;
            _httpClient = httpClient;
        }

        public async Task<string> GetToken()
        {

            var currentToken = await _clientAccessTokenCache.GetAsync("WebClientToken",default);

            if (currentToken != null)
            {
                return currentToken.AccessToken;
            }

            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _settings.IdentityBaseUrl
            });

            if (disco.IsError)
            {
                throw disco.Exception;
            }

            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.WebClient.ClientId,
                ClientSecret = _clientSettings.WebClient.ClientSecret,
                Address = disco.TokenEndpoint
            };
            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);
            if (newToken.IsError)
            {
                throw newToken.Exception;
            }

            await _clientAccessTokenCache.SetAsync("WebClientToken",newToken.AccessToken,newToken.ExpiresIn,default);

            return newToken.AccessToken;
        }
    }
}
