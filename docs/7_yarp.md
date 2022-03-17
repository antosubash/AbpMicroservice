# Yarp

Yarp is our proxy server which will redirect the request to other services.

## Install nuget

```xml
<PackageReference Include="Yarp.ReverseProxy" Version="1.0.0" />
```

## Update the Program.cs

```cs
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
var app = builder.Build();
app.MapReverseProxy();
app.Run();
```

## Update appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ReverseProxy": {
    "Routes": {
      "main": {
        "ClusterId": "main",
        "Match": {
          "Path": "{**catch-all}"
        }
      },
      "identity": {
        "ClusterId": "identity",
        "Match": {
          "Path": "/api/identity/{*any}"
        }
      },
      "account": {
        "ClusterId": "account",
        "Match": {
          "Path": "/api/account/{*any}"
        }
      },
      "saas": {
        "ClusterId": "saas",
        "Match": {
          "Path": "/api/multi-tenancy/{*any}"
        }
      }
    },
    "Clusters": {
      "main": {
        "Destinations": {
          "main": {
            "Address": "https://localhost:7001"
          }
        }
      },
      "identity": {
        "Destinations": {
          "identity": {
            "Address": "https://localhost:7002"
          }
        }
      },
      "account": {
        "Destinations": {
          "account": {
            "Address": "https://localhost:7002"
          }
        }
      },
      "saas": {
        "Destinations": {
          "saas": {
            "Address": "https://localhost:7003"
          }
        }
      }
    }
  }
}
```
