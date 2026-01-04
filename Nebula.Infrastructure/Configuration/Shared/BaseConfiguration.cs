namespace Nebula.Infrastructure.Configuration.Shared;

/// <summary>
/// Base class for configuration sections providing a standardized section name.
/// </summary>
/// <typeparam name="T">The derived configuration type.</typeparam>
internal abstract class BaseConfiguration<T> where T : BaseConfiguration<T>
{
    /// <summary>
    /// Gets the configuration section name, derived from the class name.
    /// </summary>
    public static string SectionName => typeof(T).Name;
}