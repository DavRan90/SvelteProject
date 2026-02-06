using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

namespace MinimalApi.Config
{
    public static class OpenApiConfiguration
    {
        public static IServiceCollection AddCustomOpenApi(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {

                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    document.Info = new OpenApiInfo
                    {
                        Title = "Grantidote API",
                        Description = """
                        A comprehensive grant matching platform API that connects organizations with relevant funding opportunities.

                        **Key Features:**
                        - Profile management for different organization types (companies, farmers, research institutes, etc.)
                        - Advanced grant discovery and matching using AI-powered vector search
                        - Complete application workflow management with collaboration tools
                        - Real-time notifications and activity tracking
                        - Comprehensive favorites and feedback systems
                        """,
                        Version = "v1.0",
                        Contact = new OpenApiContact
                        {
                            Name = "Grantidote Support",
                            Email = "support@grantidote.com"
                        }
                    };

                    document.Servers = new List<OpenApiServer>
                {
                    new() { Url = "https://api.grantidote.com", Description = "Production" },
                    new() { Url = "https://staging-api.grantidote.com", Description = "Staging" },
                    new() { Url = "http://localhost:5285", Description = "Development" }
                };

                    // Add security definitions
                    document.Components ??= new OpenApiComponents();
                    document.Components.SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
                    {
                        ["Bearer"] = new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.Http,
                            Scheme = "bearer",
                            BearerFormat = "JWT",
                            Description = "Enter your JWT token"
                        },
                        ["Cookie"] = new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Cookie,
                            Name = "Grantidote.Auth",
                            Description = "Authentication cookie"
                        }
                    };

                    return Task.CompletedTask;
                });
            });

            return services;
        }

        public static WebApplication UseCustomScalar(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference(options =>
                {
                    options
                        .WithTitle("Grantidote API Documentation")
                        .WithTheme(ScalarTheme.Purple)
                        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
                        .WithSidebar(true)
                        .WithModels(true)
                        .WithDownloadButton(true)
                        .WithSearchHotKey("k")
                        .WithDefaultOpenAllTags(false);
                })
                .AllowAnonymous();
            }

            return app;
        }

        public static class Tags
        {
            // Resource-based tags for primary grouping
            public const string Companies = "Companies";
            public const string Farmers = "Farmers";
            public const string Researchers = "Researchers";
            public const string Startups = "Startups";
            public const string Associations = "Associations";
            public const string Profiles = "Profiles";
            public const string Grants = "Grants";
            public const string Applications = "Applications";
            public const string Favorites = "Favorites";
            public const string Notifications = "Notifications";
            public const string Activity = "Activity";
            public const string Matches = "Matches";
            public const string Comments = "Comments";
            public const string Files = "Files";
            public const string MatchFeedback = "Match Feedback";
            public const string Health = "Health";
            public const string Debug = "Debug";
            public const string Feedback = "Feedback";
            public const string Info = "Info";
            public const string Identity = "Identity";
            public const string Billing = "Billing";

            public static readonly Dictionary<string, string> TagDescriptions = new()
            {
                // Profile Resources
                [Companies] = "Company profile management with business data, financial information, and organizational details",
                [Farmers] = "Farmer profile management with agricultural-specific data and farm information",
                [Researchers] = "Research institute profile operations and academic organization management",
                [Startups] = "Startup company profile management and entrepreneurial organization data",
                [Associations] = "Association profile operations and organizational group management",
                [Profiles] = "General profile operations that work across all organization types",

                // Core Resources
                [Grants] = "Grant discovery, search, and management operations",
                [Applications] = "Grant application lifecycle management and workflow",

                // User Features
                [Favorites] = "Personal favorites management for grants and other resources",
                [Notifications] = "Real-time notification system for important updates",
                [Activity] = "Activity feed and tracking across the platform",
                [Matches] = "AI-powered grant matching and recommendation system",

                // Collaboration
                [Comments] = "Collaborative commenting system for applications and grants",
                [Files] = "File upload, download, and management for applications",
                [MatchFeedback] = "User feedback on grant matching quality and relevance",

                // System
                [Health] = "System health monitoring and status endpoints",
                [Debug] = "Development and debugging utilities (admin only)",
                [Feedback] = "General platform feedback and support system",
                [Info] = "System information and configuration details",
                [Identity] = "User authentication, registration, and session management",
                [Billing] = "Credit purchases, balance management, and Stripe payment integration"
            };
        }
    }
}
