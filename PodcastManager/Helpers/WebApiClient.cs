using PodcastManager.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PodcastManager.Helpers
{
    internal class WebApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IFileHelper _fileHelper;

        public WebApiClient(HttpClient httpClient, IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
            _httpClient = httpClient;
        }

        public async Task DeleteAsync(string requestUri)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Delete, requestUri))
            {
                AddHeaders(request);

                using (var response = await _httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public void Dispose() =>
            _httpClient.Dispose();

        public async Task GetAsync(string requestUri, string fileToWriteTo)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                AddHeaders(request);
                request.Headers.Range = new RangeHeaderValue(0, null);

                try
                {
                    using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                    {
                        try
                        {
                            using (var stream = await response.Content.ReadAsStreamAsync())
                            {
                                if (!_fileHelper.FileExists(fileToWriteTo))
                                    using (var streamToWriteTo = _fileHelper.FileOpen(fileToWriteTo, FileMode.Create))
                                    {
                                        await stream.CopyToAsync(streamToWriteTo);
                                    }

                                stream.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public async Task<string> GetResponseStringAsync(string requestUri)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                AddHeaders(request);
                request.Headers.Range = new RangeHeaderValue(0, null);

                try
                {
                    using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                    {
                        try
                        {
                            return await response.Content.ReadAsStringAsync();                            
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public Task<HttpResponseMessage> PatchAsync(string requestUri, string json)
        {
            return SendJsonAsync(requestUri, json, new HttpMethod("PATCH"));
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, string json)
        {
            return SendJsonAsync(requestUri, json, HttpMethod.Post);
        }

        public Task<HttpResponseMessage> PutAsync(string requestUri, string json)
        {
            return SendJsonAsync(requestUri, json, HttpMethod.Put);
        }

        private async Task<HttpResponseMessage> SendJsonAsync(string requestUri, string json, HttpMethod method)
        {
            using (var request = new HttpRequestMessage(method, requestUri))
            {
                AddHeaders(request);
                request.Content = new JsonContent(json);

                _httpClient.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _httpClient.SendAsync(request).ConfigureAwait(true);
                return response;
            }
        }

        private void AddHeaders(HttpRequestMessage request)
        {
            request.Headers.Add("Cache-control", "no-cache");
            request.Headers.Add("User-Agent", "Dalvik/1.6.0 (Linux; U; Android 4.4.2; SM-C101 Build/KOT49H)");
            request.Headers.Add("Connection", "Keep-Alive");
            request.Headers.Add("Accept-Encoding", "gzip");
        }
    }
}
