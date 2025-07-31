using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

internal static class ResourceBuilderExtensions
{
    internal static IResourceBuilder<T> WithSwaggerUI<T>(this IResourceBuilder<T> builder)
        where T : IResourceWithEndpoints
    {
        return builder.WithOpenApiUi("swagger-ui-docs", "Swagger API Documentation", "swagger");
    }

    internal static IResourceBuilder<T> WithScalar<T>(this IResourceBuilder<T> builder, string version)
        where T : IResourceWithEndpoints
    {
        return builder.WithOpenApiUi($"scalar-{version}", $"Scalar API Documentation - {version.ToUpper()}", $"scalar/{version}");
    }

    internal static IResourceBuilder<T> WithReDoc<T>(this IResourceBuilder<T> builder, string version)
        where T : IResourceWithEndpoints
    {
        return builder.WithOpenApiUi($"redoc-docs-{version}", $"ReDoc API Documentation - {version.ToUpper()}", $"redoc/{version}");
    }

    /// <summary>
    /// Generic method to mount OpenAPI UI commands (Swagger, Scalar, ReDoc).
    /// </summary>
    private static IResourceBuilder<T> WithOpenApiUi<T>(
        this IResourceBuilder<T> builder,
        string name,
        string displayName,
        string path)
        where T : IResourceWithEndpoints
    {
        return builder.WithCommand(
            name,
            displayName,
            executeCommand: async _ =>
            {
                try
                {
                    var endpoint = builder.GetEndpoint("https");
                    var url = $"{endpoint.Url}/{path}";
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                    return new ExecuteCommandResult { Success = true };
                }
                catch (Exception ex)
                {
                    return new ExecuteCommandResult { Success = false, ErrorMessage = ex.ToString() };
                }
            },
            updateState: context =>
                context.ResourceSnapshot.HealthStatus == HealthStatus.Healthy
                    ? ResourceCommandState.Enabled
                    : ResourceCommandState.Disabled,
            iconName: "Document",
            iconVariant: IconVariant.Filled
        );
    }
}
