namespace JsonWorkerLib._interfaces;

/// <summary>
/// Interface for classes with serialization.
/// </summary>
internal interface ISerializable
{
    /// <summary>
    /// Serialize model to json string.
    /// </summary>
    /// <returns>Json string.</returns>
    public string ToJson();
}