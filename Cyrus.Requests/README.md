# Cyrus.Requests

Cyrus.Requests is a utility library for making HTTP requests in a simple and consistent manner.

## Installation

```sh
dotnet add package Cyrus.Requests
```
## Usage
```C#
using Cyrus.Requests;
using System.Net.Http;
using System.Threading.Tasks;

public class Example
{
    public async Task ExampleGetRequest()
    {
        var response = await Request.Get("https://api.example.com/data");
        Console.WriteLine($"Status: {response.statusCode}, Response: {response.response}");
    }
}
```
### Functions
1. `Request.Get`: 
Performs an HTTP GET request.
- Arguments:
    - url (string): The URL to send the GET request to.
    - parameters (string array): Query parameters to include in the URL.
    - headers (Dictionary<string, string>): Headers to include in the request.
- returns:
    A tuple containing the status code and the response string
```C#
    var response = await Request.Get("https://api.example.com/data");
    Console.WriteLine($"Status: {response.statusCode}, Response: {response.response}");
```
2. `Request.Post`:
Performs an HTTP POST request.
- Arguments:
    - url (string): The URL to send the POST request to.
    - parameters (string array): Query parameters to include in the URL.
    - data (Dictionary<string, string>): Data to include in the POST request body.
    - headers (Dictionary<string, string>): Headers to include in the request.
- Returns: A tuple containing the status code and the response string.
```C#
var data = new Dictionary<string, string>
{
    { "key1", "value1" },
    { "key2", "value2" }
};

var response = await Request.Post("https://api.example.com/data", data);
Console.WriteLine($"Status: {response.statusCode}, Response: {response.response}");

```
3. `Request.Put`:
Performs an HTTP PUT request.
- Arguments:
    - url (string): The URL to send the PUT request to.
    - parameters (string array): Query parameters to include in the URL.
    - data (Dictionary<string, string>): Data to include in the PUT request body.
    - headers (Dictionary<string, string>): Headers to include in the request.
- Returns: A tuple containing the status code and the response string.
```C#
var data = new Dictionary<string, string>
{
    { "key1", "value1" },
    { "key2", "value2" }
};

var response = await Request.Put("https://api.example.com/data", data);
Console.WriteLine($"Status: {response.statusCode}, Response: {response.response}");
```
4. `Request.Delete`:
Performs an HTTP DELETE request.

- Arguments:
    - url (string): The URL to send the DELETE request to.
    - headers (Dictionary<string, string>): Headers to include in the request.
- Returns: A tuple containing the status code and the response string.

```C#
var response = await Request.Delete("https://api.example.com/data/1");
Console.WriteLine($"Status: {response.statusCode}, Response: {response.response}");
```
5. `Request.Options`: 
Performs an HTTP OPTIONS request.
- Arguments:
    - url (string): The URL to send the OPTIONS request to.
    - parameters (string array): Query parameters to include in the URL.
    - headers (Dictionary<string, string>): Headers to include in the request.
- returns:
    A tuple containing the status code and the response string
```C#
var response = await Request.Options("https://api.example.com/data");
Console.WriteLine($"Status: {response.statusCode}, Response: {response.response}");
```
6. `Request.Trace`: 
Performs an HTTP TRACE request.
- Arguments:
    - url (string): The URL to send the TRACE request to.
    - parameters (string array): Query parameters to include in the URL.
    - headers (Dictionary<string, string>): Headers to include in the request.
- returns:
    A tuple containing the status code and the response string
```C#
var response = await Request.Trace("https://api.example.com/data");
Console.WriteLine($"Status: {response.statusCode}, Response: {response.response}");
```

### RequestBuilder
A class to build custom HTTP requests.

#### 1. Constructor
Initializes a new instance of the RequestBuilder class.

- Arguments:
    - method (HttpMethod): The HTTP method to use for the request.
    - url (string): The URL for the request.
```C#
var builder = new RequestBuilder(HttpMethod.Post, "https://api.example.com/data");
```
#### 2. AddHeaders
Adds headers to the request.

- Arguments:
    - headers (Dictionary<string, string>): Headers to include in the request.
- Returns: The RequestBuilder instance.
```C#
var headers = new Dictionary<string, string>
{
    { "Authorization", "Bearer token" }
};

builder.AddHeaders(headers);
```
#### 3. AddData
Adds data to the request body.

- Arguments:
    - data (Dictionary<string, string>): Data to include in the request body.
- Returns: The RequestBuilder instance.
```C#
var data = new Dictionary<string, string>
{
    { "key1", "value1" },
    { "key2", "value2" }
};

builder.AddData(data);
```
#### 5. SetContentType
Sets the content type of the request.

- Arguments:
    - contentType (string): The content type to set.
- Returns: The RequestBuilder instance.
```C#
builder.SetContentType("application/json");
```
#### 6. Build
Builds the HttpRequestMessage.

- Returns: The HttpRequestMessage instance.
```C#
var request = builder.Build();
```
**Example Usage**
Combining all the components to make a POST request with custom headers and data:
```C#
var builder = new RequestBuilder(HttpMethod.Post, "https://api.example.com/data");

var headers = new Dictionary<string, string>
{
    { "Authorization", "Bearer token" }
};

var data = new Dictionary<string, string>
{
    { "key1", "value1" },
    { "key2", "value2" }
};

var request = builder.AddHeaders(headers)
                      .AddData(data)
                      .SetContentType("application/json")
                      .Build();

var response = await Request.SendAsync(request);
Console.WriteLine($"Status: {response.statusCode}, Response: {response.response}");
```
## License
This project is licensed under the MIT License.