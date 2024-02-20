namespace JsonWorkerLib.Models.Patient;

public static class Handlers
{
    public static StateChange GetChangedStatus(double oldValue, double newValue, double minValue, double maxValue)
    {
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