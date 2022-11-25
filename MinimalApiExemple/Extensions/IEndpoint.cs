using System.Reflection;

namespace MinimalApiExemple.Extensions;

public interface IEndpoint
{
    static abstract void Register(IEndpointRouteBuilder endpoints);
}

/// <summary>
/// Extension methods for <see cref="IEndpointRouteBuilder"/> dedicated to <see cref="IEndpoint"/> objects.
/// </summary>
public static class EndpointServiceCollectionExtensions
{
    private const string ErrorMessageRegisterMethodNotFound = $"The \"{nameof(IEndpoint.Register)}\" method could not be found on the type \"{{0}}\".";

    /// <summary>
    /// Retrieves all endpoints defined by <see cref="IEndpoint"/> in the current executing assembly and register them automatically to the givent <see cref="IEndpointRouteBuilder"/>.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder to register endpoints in.</param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // Retrieve all handlers independantly from their types and their implementations
        var endpointTypesToRegister = Assembly.GetExecutingAssembly().DefinedTypes
            .Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                t.GetInterfaces().Any(i => i == typeof(IEndpoint))).ToList();

        var registerMethodParameters = new object[] { endpoints };
        foreach (var endpointType in endpointTypesToRegister)
        {
            var registerMethod = endpointType.GetMethod(nameof(IEndpoint.Register), BindingFlags.Static | BindingFlags.Public);
            if (registerMethod is null)
            {
                throw new InvalidOperationException(string.Format(ErrorMessageRegisterMethodNotFound, endpointType.FullName));
            }
            registerMethod.Invoke(null, registerMethodParameters);
        }

        return endpoints;
    }
}
