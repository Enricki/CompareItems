using System.Collections;
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
    private static void SaveListFromTemp(BinaryWriter writer)
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
    public static void Save(string path)
    {
        using (
            BinaryWriter writer =
            new BinaryWriter(File.Open(path, FileMode.Create))
    )
        {
            SaveListFromTemp(writer);
        }
    }


    public static void Load(string input ,List<string> output)
    {
        if (!File.Exists(input))
        {
            Debug.LogError("File does not exist " + input);
            return;
        }
        using (BinaryReader reader = new BinaryReader(File.OpenRead(input)))
        {
            LoadList(reader, output);
        }
    }



    public static void ReadFromStreamingAssets()
    {
        List<string> persistance = new List<string> {
        Path.Combine(Application.persistentDataPath, "Links.dat"),
        Path.Combine(Application.persistentDataPath, "Items.dat"),
        Path.Combine(Application.persistentDataPath, "Stores.dat")};

        List<string> streaming = new List<string> {
        Path.Combine(Application.streamingAssetsPath, "Links.dat"),
        Path.Combine(Application.streamingAssetsPath, "Items.dat"),
        Path.Combine(Application.streamingAssetsPath, "Stores.dat")};

        
        for (int i = 0; i < persistance.Count; i++)
        {
            if (!File.Exists(persistance[i]))
            {
                Data.tempList.Clear();
                Load(streaming[i], Data.tempList);
                Save(persistance[i]);
            }
        }
    }
}
