using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Data
{
    public static List<string> tempList = new List<string>();
    public static List<string> Items = new List<string>();
    public static List<string> Stores = new List<string>();
    public static List<string> Links= new List<string>();
    public static string path;
    public const string password = "Cucumber";

    public const string FirstGroupfileName = "Links";
    public const string SecondGroupfileName = "Items";
    public const string ThirdGroupfileName = "Stores";
    public const string fileExtension = ".dat";

    public static string ActiveGroupName;
    public static string ActiveGroup;
}
