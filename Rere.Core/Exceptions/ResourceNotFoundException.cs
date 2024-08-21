namespace Rere.Core.Exceptions;

/// <summary>
/// Exception when a resource is not found in Database
/// </summary>
/// <typeparam name="T">The type of resource</typeparam>
public class ResourceNotFoundException<T> : Exception
{
    public ResourceNotFoundException() : base($"The requested model {typeof(T)} was not found.")
    {
    }

    public ResourceNotFoundException(int resourceId) : base(
        $"The requested model {typeof(T)} with {resourceId} was not found.")
    {
    }
}