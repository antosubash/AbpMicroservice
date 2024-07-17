var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Tasky_AuthServer>("tasky-authserver", launchProfileName: "Tasky.AuthServer");

builder.AddProject<Projects.Tasky_Administration_HttpApi_Host>("tasky-administration-httpapi-host", launchProfileName: "Tasky.Administration.Host");

builder.AddProject<Projects.Tasky_IdentityService_HttpApi_Host>("tasky-identityservice-httpapi-host", launchProfileName: "Tasky.IdentityService.Host");

builder.AddProject<Projects.Tasky_Projects_HttpApi_Host>("tasky-projects-httpapi-host", launchProfileName: "Tasky.Projects.Host");

builder.AddProject<Projects.Tasky_SaaS_HttpApi_Host>("tasky-saas-httpapi-host", launchProfileName: "Tasky.SaaS.Host");

builder.AddProject<Projects.Tasky_Gateway>("tasky-gateway", launchProfileName: "Tasky.Gateway");

builder.AddProject<Projects.Tasky_Blazor_Server>("tasky-blazor-server", launchProfileName: "Tasky.Blazor.Server");

builder.AddProject<Projects.Tasky_Blazor>("tasky-blazor", launchProfileName: "Tasky.Blazor");

builder.Build().Run();