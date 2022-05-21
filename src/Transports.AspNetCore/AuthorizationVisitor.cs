using System.Security.Claims;
using GraphQL.Validation;
using Microsoft.AspNetCore.Authorization;

namespace GraphQL.Server.Transports.AspNetCore;

/// <inheritdoc/>
public class AuthorizationVisitor : AuthorizationVisitorBase
{
    /// <inheritdoc cref="AuthorizationVisitor"/>
    public AuthorizationVisitor(ValidationContext context, ClaimsPrincipal claimsPrincipal, IAuthorizationService authorizationService)
        : base(context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));
        ClaimsPrincipal = claimsPrincipal ?? throw new ArgumentNullException(nameof(claimsPrincipal));
        AuthorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
    }

    /// <summary>
    /// Gets the user that this authorization visitor will authenticate against.
    /// </summary>
    protected ClaimsPrincipal ClaimsPrincipal { get; }

    /// <summary>
    /// Gets the authorization service that is used to authorize policy requests.
    /// </summary>
    protected IAuthorizationService AuthorizationService { get; }

    /// <inheritdoc/>
    protected override bool Authorize()
        => ClaimsPrincipal.Identity?.IsAuthenticated ?? false;

    /// <inheritdoc/>
    protected override bool AuthorizeRole(string role)
        => ClaimsPrincipal.IsInRole(role);

    /// <inheritdoc/>
    protected override AuthorizationResult AuthorizePolicy(string policy)
        => AuthorizationService.AuthorizeAsync(ClaimsPrincipal, policy).GetAwaiter().GetResult();
}