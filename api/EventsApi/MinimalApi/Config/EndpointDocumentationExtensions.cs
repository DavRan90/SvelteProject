using Microsoft.AspNetCore.Mvc;

namespace MinimalApi.Config
{
    public static class EndpointDocumentationExtensions
    {
        /// <summary>
        /// Adds comprehensive documentation to an endpoint with operation ID, tags, and responses
        /// </summary>
        public static RouteHandlerBuilder WithApiDocumentation(
            this RouteHandlerBuilder builder,
            string operationId,
            string summary,
            string? description = null,
            params string[] tags)
        {
            builder
                .WithName(operationId)
                .WithSummary(summary)
                .WithTags(tags);

            if (!string.IsNullOrEmpty(description))
            {
                builder.WithDescription(description);
            }

            return builder;
        }

        /// <summary>
        /// Adds standard CRUD operation documentation
        /// </summary>
        public static RouteHandlerBuilder WithCrudDocumentation<TResponse>(
            this RouteHandlerBuilder builder,
            string operationId,
            string resource,
            CrudOperation operation,
            string tag)
        {
            var (summary, description) = operation switch
            {
                CrudOperation.Create => ($"Create {resource}", $"Creates a new {resource} in the system"),
                CrudOperation.Read => ($"Get {resource}", $"Retrieves a specific {resource} by ID"),
                CrudOperation.Update => ($"Update {resource}", $"Updates an existing {resource}"),
                CrudOperation.Delete => ($"Delete {resource}", $"Deletes a {resource} from the system"),
                CrudOperation.List => ($"List {resource}s", $"Retrieves a paginated list of {resource}s"),
                _ => ($"Manage {resource}", $"Performs operations on {resource}")
            };

            builder.WithApiDocumentation(operationId, summary, description, tag);

            // Add standard responses based on operation type
            return operation switch
            {
                CrudOperation.Create => builder
                    .Produces<TResponse>(StatusCodes.Status201Created)
                    .Produces<string>(StatusCodes.Status400BadRequest)
                    .Produces<string>(StatusCodes.Status409Conflict),

                CrudOperation.Read => builder
                    .Produces<TResponse>(StatusCodes.Status200OK)
                    .Produces<string>(StatusCodes.Status404NotFound),

                CrudOperation.Update => builder
                    .Produces<TResponse>(StatusCodes.Status200OK)
                    .Produces<string>(StatusCodes.Status400BadRequest)
                    .Produces<string>(StatusCodes.Status404NotFound),

                CrudOperation.Delete => builder
                    .Produces(StatusCodes.Status204NoContent)
                    .Produces<string>(StatusCodes.Status404NotFound),

                CrudOperation.List => builder
                    .Produces<TResponse>(StatusCodes.Status200OK)
                    .Produces<string>(StatusCodes.Status400BadRequest),

                _ => builder.Produces<TResponse>(StatusCodes.Status200OK)
            };
        }

        /// <summary>
        /// Adds authentication requirements to an endpoint
        /// </summary>
        public static RouteHandlerBuilder RequireAuthentication(this RouteHandlerBuilder builder)
        {
            return builder
                .RequireAuthorization()
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status403Forbidden);
        }

        /// <summary>
        /// Adds admin-only authentication requirements
        /// </summary>
        public static RouteHandlerBuilder RequireAdminAuthentication(this RouteHandlerBuilder builder)
        {
            return builder
                .RequireAuthorization("AdminOnly")
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status403Forbidden);
        }

        /// <summary>
        /// Adds validation error responses
        /// </summary>
        public static RouteHandlerBuilder WithValidationResponses(this RouteHandlerBuilder builder)
        {
            return builder
                .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<string>(StatusCodes.Status422UnprocessableEntity);
        }

        /// <summary>
        /// Adds rate limiting responses
        /// </summary>
        public static RouteHandlerBuilder WithRateLimitingResponses(this RouteHandlerBuilder builder)
        {
            return builder.Produces<string>(StatusCodes.Status429TooManyRequests);
        }

        /// <summary>
        /// Adds common error responses (500, 503)
        /// </summary>
        public static RouteHandlerBuilder WithCommonErrorResponses(this RouteHandlerBuilder builder)
        {
            return builder
                .Produces<string>(StatusCodes.Status500InternalServerError)
                .Produces<string>(StatusCodes.Status503ServiceUnavailable);
        }

        /// <summary>
        /// Adds comprehensive documentation for a profile creation endpoint
        /// </summary>
        public static RouteHandlerBuilder WithProfileCreationDocumentation<TResponse>(
            this RouteHandlerBuilder builder,
            string profileType,
            string operationId)
        {
            // Use the specific profile type as the tag (e.g., "Companies", "Farmers")
            var tag = profileType switch
            {
                "Company" => "Companies",
                "Farmer" => "Farmers",
                "Researcher" => "Researchers",
                "Startup" => "Startups",
                "Association" => "Associations",
                _ => "Profiles"
            };

            return builder
                .WithApiDocumentation(
                    operationId,
                    $"Create {profileType} Profile",
                    $"Creates a new {profileType.ToLower()} profile and associates it with the authenticated user. " +
                    $"Users can only have one profile, so this will fail if the user already has an existing profile.",
                    tag)
                .RequireAuthentication()
                .WithValidationResponses()
                .Produces<TResponse>(StatusCodes.Status201Created)
                .Produces<string>(StatusCodes.Status409Conflict); // User already has profile
        }

        /// <summary>
        /// Adds comprehensive documentation for file upload endpoints
        /// </summary>
        public static RouteHandlerBuilder WithFileUploadDocumentation<TResponse>(
            this RouteHandlerBuilder builder,
            string operationId,
            string summary,
            long maxFileSizeBytes)
        {
            var maxSizeMB = maxFileSizeBytes / (1024 * 1024);

            return builder
                .WithApiDocumentation(
                    operationId,
                    summary,
                    $"Uploads a file to the system. Maximum file size: {maxSizeMB}MB. " +
                    "Supported formats: PDF, DOC, DOCX, XLS, XLSX, TXT, images (PNG, JPG, JPEG, GIF).",
                    "Files")
                .RequireAuthentication()
                .Produces<TResponse>(StatusCodes.Status201Created)
                .Produces<string>(StatusCodes.Status400BadRequest)
                .Produces<string>(StatusCodes.Status413PayloadTooLarge)
                .Produces<string>(StatusCodes.Status415UnsupportedMediaType);
        }

        /// <summary>
        /// Adds search endpoint documentation
        /// </summary>
        public static RouteHandlerBuilder WithSearchDocumentation<TResponse>(
            this RouteHandlerBuilder builder,
            string operationId,
            string resourceType,
            string tag)
        {
            return builder
                .WithApiDocumentation(
                    operationId,
                    $"Search {resourceType}",
                    $"Performs advanced search across {resourceType.ToLower()} using text matching and optional filters. " +
                    "Supports pagination and sorting options.",
                    tag)
                .Produces<TResponse>(StatusCodes.Status200OK)
                .Produces<string>(StatusCodes.Status400BadRequest)
                .WithRateLimitingResponses();
        }
    }

    public enum CrudOperation
    {
        Create,
        Read,
        Update,
        Delete,
        List
    }
}
