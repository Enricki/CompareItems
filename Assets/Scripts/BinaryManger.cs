using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BinaryManger : MonoBehaviour
{
    private void Start()
    {
        GroupManager.SetActiveGroupName(Data.FirstGroupfileName);
    }

    public static void AddElementToTemp(string str)
    {
        Data.tempList.Clear();
        if (str != string.Empty)
        {
            string[] strings = str.Split(new char[] { '\n' });
            for (int i = 0; i < strings.Length; i++)
            {
                Data.tempList.Add(strings[i]);
            }
        }
    }
    private void SaveListFromTemp(BinaryWriter writer)
    {
        writer.Write(Data.tempList.Count);
        for (int i = 0; i < Data.tempList.Count; i++)
        {
            writer.Write(Data.tempList[i]);
        }
        Data.tempList.Clear();
        Debug.Log("Saved");
    }

    private static void LoadList(BinaryReader reader, List<string> output)
    {
        if (output != null)
        {
            output.Clear();
        }
        int elementsCount = reader.ReadInt32();
        for (int i = 0; i < elementsCount; i++)
        {
            output.Add(reader.ReadString());
        }
    }

    public void SaveAll()
    {
        using (
            BinaryWriter writer =
            new BinaryWriter(File.Open(Data.path, FileMode.Create))
            )
        {
            SaveListFromTemp(writer);
        }
    }

    public static void Load(string input ,List<string> output)
    {
        if (!File.Exists(Data.path))
        {
            Debug.LogError("File does not exist " + input);
            return;
        }
        using (BinaryReader reader = new BinaryReader(File.OpenRead(input)))
        {
            LoadList(reader, output);
        }
    }
}
