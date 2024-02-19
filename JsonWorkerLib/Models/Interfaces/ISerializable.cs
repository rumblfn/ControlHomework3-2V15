namespace JsonWorkerLib.Models.Interfaces;

internal interface ISerializable
{
    /// <summary>
    /// Serialize model to json string.
    /// </summary>
    /// <returns>Json string.</returns>
    public string ToJSON();
}