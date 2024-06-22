using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Policy;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cyrus.Requests
{
    public static class Request
    {
        /// <summary>
        /// Sends a GET request to the specified URL with optional query parameters and headers.
        /// </summary>
        /// <param name="url">The URL to send the GET request to.</param>
        /// <param name="parameters">Optional query parameters to include in the request.</param>
        /// <param name="headers">Optional headers to include in the request.</param>
        /// <returns>A tuple containing the status code and response body as a string.</returns>
        public async static Task<(int statusCode, string response)> Get(string url, string[] parameters = null, Dictionary<string, string> headers = null)
        {
            if (parameters != null && parameters.Length > 0)
            {
                url += "?" + string.Join("&", parameters);
            }
            var requestBuilder = new RequestBuilder(HttpMethod.Get, url)
                .AddHeaders(headers);
            var request = requestBuilder.Build();
            HttpResponseMessage response = await Send(request);
            string responseBody = await response.Content.ReadAsStringAsync();
            return ((int)response.StatusCode, responseBody);
        }
        
        /// <summary>
        /// Sends a POST request to the specified URL with optional query parameters, form data, headers, and content type.
        /// </summary>
        /// <param name="url">The URL to send the POST request to.</param>
        /// <param name="parameters">Optional query parameters to include in the request.</param>
        /// <param name="data">Optional form data to include in the request body.</param>
        /// <param name="headers">Optional headers to include in the request.</param>
        /// <param name="contentType">The content type of the request body (default is "application/x-www-form-urlencoded").</param>
        /// <returns>A tuple containing the status code and response body as a string.</returns>
        public async static Task<(int statusCode, string response)> Post(string url, string[] parameters = null, Dictionary<string, string> data = null, Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded")
        {
            if (parameters != null && parameters.Length > 0)
            {
                url += "?" + string.Join("&", parameters);
            }
            var requestBuilder = new RequestBuilder(HttpMethod.Post, url)
                .AddHeaders(headers)
                .AddData(data)
                .SetContentType(contentType);

            var request = requestBuilder.Build();
            HttpResponseMessage response = await Send(request);
            string responseBody = await response.Content.ReadAsStringAsync();
            return ((int)response.StatusCode, responseBody);
        }

        /// <summary>
        /// Sends a PUT request to the specified URL with optional query parameters, form data, headers, and content type.
        /// </summary>
        /// <param name="url">The URL to send the PUT request to.</param>
        /// <param name="parameters">Optional query parameters to include in the request.</param>
        /// <param name="data">Optional form data to include in the request body.</param>
        /// <param name="headers">Optional headers to include in the request.</param>
        /// <param name="contentType">The content type of the request body (default is "application/x-www-form-urlencoded").</param>
        /// <returns>A tuple containing the status code and response body as a string.</returns>
        public async static Task<(int statusCode, string response)> Put(string url, string[] parameters = null, Dictionary<string, string> data = null, Dictionary<string, string> headers = null, string contentType = "application/x-www-form-urlencoded")
        {
            if (parameters != null && parameters.Length > 0)
            {
                url += "?" + string.Join("&", parameters);
            }
            var requestBuilder = new RequestBuilder(HttpMethod.Put, url)
                .AddHeaders(headers)
                .AddData(data)
                .SetContentType(contentType);

            var request = requestBuilder.Build();
            HttpResponseMessage response = await Send(request);
            string responseBody = await response.Content.ReadAsStringAsync();
            return ((int)response.StatusCode, responseBody);
        }

        /// <summary>
        /// Sends a DELETE request to the specified URL with optional query parameters and headers.
        /// </summary>
        /// <param name="url">The URL to send the DELETE request to.</param>
        /// <param name="parameters">Optional query parameters to include in the request.</param>
        /// <param name="headers">Optional headers to include in the request.</param>
        /// <returns>A tuple containing the status code and response body as a string.</returns>
        public async static Task<(int statusCode, string response)> Delete(string url, string[] parameters = null, Dictionary<string, string> headers = null)
        {
            if (parameters != null && parameters.Length > 0)
            {
                url += "?" + string.Join("&", parameters);
            }
            var requestBuilder = new RequestBuilder(HttpMethod.Delete, url).AddHeaders(headers);

            var request = requestBuilder.Build();
            HttpResponseMessage response = await Send(request);
            string responseBody = await response.Content.ReadAsStringAsync();
            return ((int)response.StatusCode, responseBody);
        }

        /// <summary>
        /// Sends an OPTIONS request to the specified URL with optional query parameters and headers.
        /// </summary>
        /// <param name="url">The URL to send the OPTIONS request to.</param>
        /// <param name="parameters">Optional query parameters to include in the request.</param>
        /// <param name="headers">Optional headers to include in the request.</param>
        /// <returns>A tuple containing the status code and response body as a string.</returns>
        public async static Task<(int statusCode, string response)> Options(string url, string[] parameters = null, Dictionary<string, string> headers = null)
        {
            if (parameters != null && parameters.Length > 0)
            {
                url += "?" + string.Join("&", parameters);
            }
            var requestBuilder = new RequestBuilder(HttpMethod.Options, url)
                .AddHeaders(headers);

            var request = requestBuilder.Build();
            HttpResponseMessage response = await Send(request);
            string responseBody = await response.Content.ReadAsStringAsync();
            return ((int)response.StatusCode, responseBody);
        }

        /// <summary>
        /// Sends a TRACE request to the specified URL with optional query parameters and headers.
        /// </summary>
        /// <param name="url">The URL to send the TRACE request to.</param>
        /// <param name="parameters">Optional query parameters to include in the request.</param>
        /// <param name="headers">Optional headers to include in the request.</param>
        /// <returns>A tuple containing the status code and response body as a string.</returns>
        public async static Task<(int statusCode, string response)> Trace(string url, string[] parameters = null, Dictionary<string, string> headers = null)
        {
            if (parameters != null && parameters.Length > 0)
            {
                url += "?" + string.Join("&", parameters);
            }
            var requestBuilder = new RequestBuilder(HttpMethod.Trace, url)
                .AddHeaders(headers);

            var request = requestBuilder.Build();
            HttpResponseMessage response = await Send(request);
            string responseBody = await response.Content.ReadAsStringAsync();
            return ((int)response.StatusCode, responseBody);
        }

        /// <summary>
        /// Sends an HTTP request with the specified method, data, headers, and content type.
        /// </summary>
        /// <param name="request">The HttpRequestMessage containing the request details.</param>
        /// <param name="data">Optional form data to include in the request body.</param>
        /// <param name="headers">Optional headers to include in the request.</param>
        /// <param name="contentType">The content type of the request body (default is "application/x-www-form-urlencoded").</param>
        /// <returns>The HttpResponseMessage resulting from the request.</returns>
        public async static Task<HttpResponseMessage> Send(HttpRequestMessage request)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.SendAsync(request);
                    return response;
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle specific HttpRequestException
                throw new ApplicationException($"[X] HTTP request error occurred while sending the {request.Method} request: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions as needed
                throw new ApplicationException($"[X] An error occurred while sending the {request.Method} request: {ex.Message}");
            }

        }
    }
}
