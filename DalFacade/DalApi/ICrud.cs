namespace DalApi;
using DO;

/// <summary>
/// Represents a generic CRUD (Create, Read, Update, Delete) interface for working with entity objects in the DAL.
/// </summary>
/// <typeparam name="T">The type of the entity object.</typeparam>
public interface ICrud<T> where T : class
{
    /// <summary>
    /// Creates a new entity object in the DAL.
    /// </summary>
    /// <param name="item">The entity object to create.</param>
    /// <returns>The ID of the created entity object.</returns>
    int Create(T item);

    /// <summary>
    /// Reads a single entity object from the DAL based on the provided filter.
    /// </summary>
    /// <param name="filter">The filter to apply when reading the entity object.</param>
    /// <returns>The matching entity object, or null if not found.</returns>
    T? Read(Func<T, bool> filter);

    /// <summary>
    /// Reads a single entity object from the DAL based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the entity object to read.</param>
    /// <returns>The matching entity object, or null if not found.</returns>
    T? Read(int id);

    /// <summary>
    /// Reads all entity objects from the DAL based on the optional filter.
    /// </summary>
    /// <param name="filter">The optional filter to apply when reading the entity objects.</param>
    /// <returns>An enumerable collection of entity objects.</returns>
    IEnumerable<T?> ReadAll(Func<T, bool>? filter = null);

    /// <summary>
    /// Updates an existing entity object in the DAL.
    /// </summary>
    /// <param name="item">The entity object to update.</param>
    void Update(T item);

    /// <summary>
    /// Deletes an entity object from the DAL based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the entity object to delete.</param>
    void Delete(int id);
}
