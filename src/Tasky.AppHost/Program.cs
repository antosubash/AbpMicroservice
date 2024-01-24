var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Tasky_Administration_HttpApi_Host>("tasky.administration.httpapi.host");

builder.AddProject<Projects.Tasky_IdentityService_HttpApi_Host>("tasky.identityservice.httpapi.host");

builder.AddProject<Projects.Tasky_Projects_HttpApi_Host>("tasky.projects.httpapi.host");

builder.AddProject<Projects.Tasky_SaaS_HttpApi_Host>("tasky.saas.httpapi.host");

builder.AddProject<Projects.Tasky_Gateway>("tasky.gateway");

//builder.AddProject<Projects.Tasky_Blazor_Server>("tasky.blazor.server");

builder.AddProject<Projects.Tasky_Blazor>("tasky.blazor");

builder.AddProject<Projects.Tasky_AuthServer>("tasky.authserver");

builder.Build().Run();
