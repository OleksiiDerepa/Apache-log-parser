using System.Linq;

using LogParser.DAL.Entities;

namespace LogParser.DAL.Context
{
    public static class SeedData
    {
        public static ApacheLogDbContext Initialize(this ApacheLogDbContext context)
        {
            if (!context.ProtocolVersions.Any())
            {
                context.ProtocolVersions.AddRange(
                    new[]
                    {
                        new ProtocolVersion {Version = 0.9d},
                        new ProtocolVersion {Version = 1.0d},
                        new ProtocolVersion {Version = 1.1d},
                        new ProtocolVersion {Version = 2.0d},
                        new ProtocolVersion {Version = 3.0d}
                    }
                );
            }
            if (!context.Protocols.Any())
            {
                context.Protocols.AddRange(new[]
                {
                    new Protocol{Name = "HTTP"},
                    new Protocol{Name = "HTTPS"},
                });
            }
            if (!context.RestMethods.Any())
            {
                context.RestMethods.AddRange(
                    new[]
                    {
                        new RestMethod {Name = "GET"},
                        new RestMethod {Name = "HEAD"},
                        new RestMethod {Name = "POST"},
                        new RestMethod {Name = "PUT"},
                        new RestMethod {Name = "DELETE"},
                        new RestMethod {Name = "TRACE"},
                        new RestMethod {Name = "OPTIONS"},
                        new RestMethod {Name = "CONNECT"},
                        new RestMethod {Name = "PATCH"},
                    });
            }
            if (!context.RestStatusCodes.Any())
            {
                context.RestStatusCodes.AddRange(
                    new[]
                    {
                        new RestStatusCode{ Number =100, Description = "Continue"},
                        new RestStatusCode{ Number =101, Description = "Switching Protocols"},
                        new RestStatusCode{ Number =102, Description = "Processing"},
                        new RestStatusCode{ Number =200, Description = "OK"},
                        new RestStatusCode{ Number =201, Description = "Created"},
                        new RestStatusCode{ Number =202, Description = "Accepted"},
                        new RestStatusCode{ Number =203, Description = "Non-authoritative Information"},
                        new RestStatusCode{ Number =204, Description = "No Content"},
                        new RestStatusCode{ Number =205, Description = "Reset Content"},
                        new RestStatusCode{ Number =206, Description = "Partial Content"},
                        new RestStatusCode{ Number =207, Description = "Multi-Status"},
                        new RestStatusCode{ Number =208, Description = "Already Reported"},
                        new RestStatusCode{ Number =226, Description = "IM Used"},
                        new RestStatusCode{ Number =300, Description = "Multiple Choices"},
                        new RestStatusCode{ Number =301, Description = "Moved Permanently"},
                        new RestStatusCode{ Number =302, Description = "Found"},
                        new RestStatusCode{ Number =303, Description = "See Other"},
                        new RestStatusCode{ Number =304, Description = "Not Modified"},
                        new RestStatusCode{ Number =305, Description = "Use Proxy"},
                        new RestStatusCode{ Number =307, Description = "Temporary Redirect"},
                        new RestStatusCode{ Number =308, Description = "Permanent Redirect"},
                        new RestStatusCode{ Number =400, Description = "Bad Request"},
                        new RestStatusCode{ Number =401, Description = "Unauthorized"},
                        new RestStatusCode{ Number =402, Description = "Payment Required"},
                        new RestStatusCode{ Number =403, Description = "Forbidden"},
                        new RestStatusCode{ Number =404, Description = "Not Found"},
                        new RestStatusCode{ Number =405, Description = "Method Not Allowed"},
                        new RestStatusCode{ Number =406, Description = "Not Acceptable"},
                        new RestStatusCode{ Number =407, Description = "Proxy Authentication Required"},
                        new RestStatusCode{ Number =408, Description = "Request Timeout"},
                        new RestStatusCode{ Number =409, Description = "Conflict"},
                        new RestStatusCode{ Number =410, Description = "Gone"},
                        new RestStatusCode{ Number =411, Description = "Length Required"},
                        new RestStatusCode{ Number =412, Description = "Precondition Failed"},
                        new RestStatusCode{ Number =413, Description = "Payload Too Large"},
                        new RestStatusCode{ Number =414, Description = "Request-URI Too Long"},
                        new RestStatusCode{ Number =415, Description = "Unsupported Media Type"},
                        new RestStatusCode{ Number =416, Description = "Requested Range Not Satisfiable"},
                        new RestStatusCode{ Number =417, Description = "Expectation Failed"},
                        new RestStatusCode{ Number =418, Description = "I’m a teapot"},
                        new RestStatusCode{ Number =421, Description = "Misdirected Request"},
                        new RestStatusCode{ Number =422, Description = "Unprocessable Entity"},
                        new RestStatusCode{ Number =423, Description = "Locked"},
                        new RestStatusCode{ Number =424, Description = "Failed Dependency"},
                        new RestStatusCode{ Number =426, Description = "Upgrade Required"},
                        new RestStatusCode{ Number =428, Description = "Precondition Required"},
                        new RestStatusCode{ Number =429, Description = "Too Many Requests"},
                        new RestStatusCode{ Number =431, Description = "Request Header Fields Too Large"},
                        new RestStatusCode{ Number =444, Description = "Connection Closed Without Response"},
                        new RestStatusCode{ Number =451, Description = "Unavailable For Legal Reasons"},
                        new RestStatusCode{ Number =499, Description = "Client Closed Request"},
                        new RestStatusCode{ Number =500, Description = "Internal Server Error"},
                        new RestStatusCode{ Number =501, Description = "Not Implemented"},
                        new RestStatusCode{ Number =502, Description = "Bad Gateway"},
                        new RestStatusCode{ Number =503, Description = "Service Unavailable"},
                        new RestStatusCode{ Number =504, Description = "Gateway Timeout"},
                        new RestStatusCode{ Number =505, Description = "HTTP Version Not Supported"},
                        new RestStatusCode{ Number =506, Description = "Variant Also Negotiates"},
                        new RestStatusCode{ Number =507, Description = "Insufficient Storage"},
                        new RestStatusCode{ Number =508, Description = "Loop Detected"},
                        new RestStatusCode{ Number =510, Description = "Not Extended"},
                        new RestStatusCode{ Number =511, Description = "Network Authentication Required"},
                        new RestStatusCode{ Number =599, Description = "Network Connect Timeout Error"}
                    });
            }

            context.SaveChanges();

            return context;
        }

    }
}