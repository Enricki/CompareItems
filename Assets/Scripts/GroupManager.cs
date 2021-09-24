using UnityEngine;
using System.IO;

public class GroupManager
{
    public static void SetDataPath(string str)
    {
        Data.path = Path.Combine(Application.persistentDataPath, str + Data.fileExtension);
    }
    public static void SetActiveGroupName(string groupName)
    {
        Data.ActiveGroupName = groupName;
        SetDataPath(groupName);
    }
}
