using System.Text.Json;
using JsonWorkerApp.Components;
using JsonWorkerLib;
using JsonWorkerLib.Models;
using Utils;

namespace JsonWorkerApp;

// /Users/samilvaliahmetov/Projects/ControlHomework3-2V15/assets/15V.json

internal static class JsonWorker
{
    private static MenuGroup[] _groups = Templates.DataSelectActionTypePanel();

    private static void HandleAction(ActionType action)
    {
        switch (action)
        {
            case ActionType.Show:
                break;
            case ActionType.Sort:
                break;
            case ActionType.Update:
                break;
            case ActionType.Filter:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }
    
    public static void Run()
    {
        ConsoleMethod.NicePrint("> Enter path to json data", Color.Condition);
        string jsonFilePath = ConsoleMethod.ReadLine();
        string jsonString = File.ReadAllText(jsonFilePath);

        var autoSaver = new AutoSaver(jsonFilePath);
        var patients = JsonSerializer.Deserialize<List<Patient>>(jsonString);

        if (patients == null)
        {
            ConsoleMethod.NicePrint("Data not provided.");
            return;
        }
        
        foreach (Patient patient in patients)
        {
            autoSaver.SubscribeToEvents(patient);
        }
        
        do
        {
            var dp = new DataPanel(_groups);
            MenuItem? inputTypeItem = dp.Run("Data parsed.");

            if (inputTypeItem != null)
                HandleAction(inputTypeItem.Action);
            else
                break;
        } while (true);
    }
}