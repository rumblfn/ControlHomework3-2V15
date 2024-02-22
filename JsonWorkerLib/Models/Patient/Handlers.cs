using JsonWorkerLib.Models._shared;

namespace JsonWorkerLib.Models.Patient;

/// <summary>
/// Handlers for patient model.
/// </summary>
public static class Handlers
{
    /// <summary>
    /// Returns status of changed data.
    /// </summary>
    /// <param name="oldValue">Old field value.</param>
    /// <param name="newValue">New field value.</param>
    /// <param name="minValue">Minimum acceptable value.</param>
    /// <param name="maxValue">Maximum acceptable value.</param>
    /// <returns>Changed field status.</returns>
    public static StateChange GetChangedStatus(double oldValue, double newValue, double minValue, double maxValue)
    {
        if (Math.Abs(oldValue - newValue) < double.Epsilon)
        {
            return StateChange.NothingChanged;
        }
        
        if ((oldValue < minValue || oldValue > maxValue) &&
            newValue >= minValue && newValue <= maxValue)
        {
            return StateChange.ReturnedToNormal;
        }
        if ((newValue < minValue || newValue > maxValue) &&
            oldValue >= minValue && oldValue <= maxValue)
        {
            return StateChange.ExceededThresholds;
        }

        return StateChange.Default;
    }
}