﻿using System.IO;

namespace AzureFunctions.Extensions.Swashbuckle.SwashBuckle
{
    internal class SwashBuckleClient : ISwashBuckleClient
    {
        private readonly SwashbuckleConfig _config;

        public SwashBuckleClient(SwashbuckleConfig config)
        {
            _config = config;
        }

        public Stream GetSwaggerDocument(string host, string documentName = "v1")
        {
            return _config.GetSwaggerDocument(host, documentName);
        }

        public Stream GetSwaggerOAuth2Redirect()
        {
            var content = _config.GetSwaggerOAuth2RedirectContent();
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            writer.Write(content);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        public Stream GetSwaggerUi(string swaggerUrl)
        {
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);

            writer.Write(_config.GetSwaggerUIContent(swaggerUrl));
            writer.Flush();

            memoryStream.Position = 0;
            return memoryStream;
        }

        public string RoutePrefix => _config.RoutePrefix;
    }
}