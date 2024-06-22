using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cyrus.Requests
{
    /// <summary>
    /// Builder class for constructing HttpRequestMessage objects with headers, data, and content type.
    /// </summary>
    public class RequestBuilder
    {
        private HttpRequestMessage _request;
        private Dictionary<string, string> _headers;
        private object _data; // Changed to string for custom data format
        private string _contentType = "application/x-www-form-urlencoded";

        /// <summary>
        /// Constructs a new RequestBuilder instance for a specified HTTP method and URL.
        /// </summary>
        /// <param name="method">The HTTP method to be used in the request (e.g., GET, POST).</param>
        /// <param name="url">The URL of the request.</param>
        public RequestBuilder(HttpMethod method, string url)
        {
            this._request = new HttpRequestMessage(method, url);
        }

        /// <summary>
        /// Adds headers to the HTTP request.
        /// </summary>
        /// <param name="headers">A dictionary containing headers to be added to the request.</param>
        /// <returns>The current RequestBuilder instance for method chaining.</returns>
        public RequestBuilder AddHeaders(Dictionary<string, string> headers)
        {
            this._headers = headers;
            return this;
        }

        /// <summary>
        /// Sets the data to be included in the request body.
        /// </summary>
        /// <param name="data">The data to be sent in the request body. Can be a Dictionary&lt;string, string&gt; for form-urlencoded or JSON string.</param>
        /// <returns>The current RequestBuilder instance for method chaining.</returns>
        public RequestBuilder AddData(Dictionary<string, string> data)
        {
            this._data = data;
            return this;
        }

        /// <summary>
        /// Sets the content type of the request body.
        /// </summary>
        /// <param name="contentType">The content type of the request body (e.g., "application/json", "application/x-www-form-urlencoded").</param>
        /// <returns>The current RequestBuilder instance for method chaining.</returns>
        public RequestBuilder SetContentType(string contentType)
        {
            this._contentType = contentType;
            return this;
        }

        /// <summary>
        /// Constructs and returns the final HttpRequestMessage with headers and content based on the configured settings.
        /// </summary>
        /// <returns>The HttpRequestMessage object configured with headers and content based on the builder settings.</returns>
        /// <exception cref="InvalidOperationException">Thrown when attempting to build the request without setting the HTTP method.</exception>
        /// <exception cref="Exception">Thrown when there is an error setting headers or content for the request.</exception>
        public HttpRequestMessage Build()
        {

            Validate(); // Perform validation before building

            try
            {
                if (this._headers != null)
                {
                    foreach (var header in this._headers)
                    {
                        this._request.Headers.Add(header.Key, header.Value);
                    }
                }

                // Apply content if data is provided for methods that support it
                if (this._data != null && SupportsRequestBody(this._request.Method))
                {
                    SetRequestContent();
                }

                return this._request;
            }
            catch (Exception e)
            {
                throw new Exception("[X] " + e.Message);
            }
        }
        private void Validate()
        {
            // Check if HTTP method is set
            if (this._request.Method == null)
            {
                throw new InvalidOperationException("HTTP method must be set.");
            }

            // Example: Ensure Content-Type header is set for requests with a body
            if (SupportsRequestBody(this._request.Method) && this._data != null)
            {
                if (!this._request.Headers.Contains("Content-Type"))
                {
                    this._request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(this._contentType);
                }
            }
        }
        private void SetRequestContent()
        {
            try
            {
                // Determine how to serialize the data based on content type
                if (this._contentType == "application/json")
                {
                    var json = JsonConvert.SerializeObject(this._data);
                    this._request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }
                else if (this._contentType == "application/x-www-form-urlencoded")
                {
                    // Assuming this._data is of type Dictionary<string, string> for form-urlencoded
                    var formData = this._data as Dictionary<string, string>;
                    this._request.Content = new FormUrlEncodedContent(formData);
                }
                else if (this._data.GetType() == typeof(string))
                {
                    this._request.Content = new StringContent(this._data.ToString(), Encoding.UTF8, this._contentType);
                }
                else
                {
                    throw new InvalidOperationException($"[X] Unsupported data type for content type {_contentType}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("[X] " + ex.Message);
            }
        }
        private bool SupportsRequestBody(HttpMethod method)
        {
            return method == HttpMethod.Post || method == HttpMethod.Put;
        }
    }
}
