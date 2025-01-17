using GraphQL.Server.Ui.SmartPlayground;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Extensions for <see cref="IEndpointRouteBuilder"/> to add <see cref="SmartPlaygroundMiddleware"/> in the HTTP request pipeline.
/// </summary>
/*
public static class SmartPlaygroundEndpointRouteBuilderExtensions
{
    /// <summary>
    /// Add the Playground middleware to the HTTP request pipeline
    /// </summary>
    /// <param name="endpoints">Defines a contract for a route builder in an application. A route builder specifies the routes for an application.</param>
    /// <param name="pattern">The route pattern.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> received as parameter</returns>
    public static SmartPlaygroundEndpointConventionBuilder MapGraphQLPlayground(this IEndpointRouteBuilder endpoints, string pattern = "ui/smartplayground")
        => endpoints.MapGraphQLPlayground(new SmartPlaygroundOptions(), pattern);

    /// <summary>
    /// Add the Playground middleware to the HTTP request pipeline
    /// </summary>
    /// <param name="endpoints">Defines a contract for a route builder in an application. A route builder specifies the routes for an application.</param>
    /// <param name="options">Options to customize <see cref="SmartPlaygroundMiddleware"/>. If not set, then the default values will be used.</param>
    /// <param name="pattern">The route pattern.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> received as parameter</returns>
    public static SmartPlaygroundEndpointConventionBuilder MapGraphQLPlayground(this IEndpointRouteBuilder endpoints, SmartPlaygroundOptions options, string pattern = "ui/smartplayground")
    {
        if (endpoints == null)
            throw new ArgumentNullException(nameof(endpoints));

        var requestDelegate = endpoints.CreateApplicationBuilder().UseMiddleware<SmartPlaygroundMiddleware>(options ?? new SmartPlaygroundOptions()).Build();
        return new SmartPlaygroundEndpointConventionBuilder(endpoints.MapGet(pattern, requestDelegate).WithDisplayName("SMART GraphQL Playground"));
    }
}*/

/// <summary>
/// Builds conventions that will be used for customization of Microsoft.AspNetCore.Builder.EndpointBuilder instances.
/// Special convention builder that allows you to write specific extension methods for ASP.NET Core routing subsystem.
/// </summary>
public class SmartPlaygroundEndpointConventionBuilder : IEndpointConventionBuilder
{
    private readonly IEndpointConventionBuilder _builder;

    internal SmartPlaygroundEndpointConventionBuilder(IEndpointConventionBuilder builder)
    {
        _builder = builder;
    }

    /// <inheritdoc />
    public void Add(Action<EndpointBuilder> convention) => _builder.Add(convention);
}
