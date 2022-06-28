﻿using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZeroPass.Api.Tests
{
    public class RequestBuilder
    {
        const string GET = "GET";
        const string POST = "POST";
        const string PUT = "PUT";
        const string DELETE = "DELETE";

        string Path { get; }
        string Method { get; }
        string Resource { get; } = "/{proxy+}";
        string Body { get; set; }
        IDictionary<string, string> Headers = new Dictionary<string, string>();
        IDictionary<string, string> QueryStringParameters { get; set; } = new Dictionary<string, string>();

        public static RequestBuilder GetRequest(string path)
            => new RequestBuilder(path, GET);

        public static RequestBuilder PostRequest(string path)
            => new RequestBuilder(path, "POST");

        public RequestBuilder(string path, string method)
        {
            Path = path;
            Method = method;
        }

        public RequestBuilder WithBody(object body)
        {
            Body = JsonConvert.SerializeObject(body);
            return AddHeader("Content-Type", "application/json");
        }

        public RequestBuilder AddQueryParameter(string name, string value)
        {
            QueryStringParameters[name] = value;
            return this;
        }

        public APIGatewayProxyRequest Build()
            => new APIGatewayProxyRequest
            {
                Path = Path,
                HttpMethod = Method,
                Resource = Resource,
                Headers = Headers,
                Body = Body,
                QueryStringParameters = QueryStringParameters,
            };

        RequestBuilder AddHeader(string name, string value)
        {
            Headers.Add(name, value);
            return this;
        }
    }
}