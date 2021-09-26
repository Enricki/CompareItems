using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System;

public class UIManager : MonoBehaviour
{
    public List<GameObject> UIElements;
    public List<Sprite> Sprites;
    public ToggleGroup tGroupInstance;
    public Image ActiveImage;


    private void Start()
    {
        LoadData();
        LoadOnUI();
        GetFinalList(Data.Stores[0]);
        ActiveImage.sprite = Sprites[0];
    }


    public void ChangeActiveImage(int i)
    {
        ActiveImage.sprite = Sprites[i];
    }


    public void EnterPass(TMP_InputField input)
    {
        if (input.text == Data.password)
        {
            Close(UIElements[2]);
            Close(UIElements[4]);
            Open(UIElements[3]);
            input.text = string.Empty;
        }
        else
        {
            input.text = "Wrong Pass!";
        }
    }
    //public Toggle currentSelection
    //{
    //    get { return tGroupInstance.ActiveToggles().FirstOrDefault(); }
    //}

    //public void SelectedToggle(int id)
    //{
    //    var toggles = tGroupInstance.GetComponentsInChildren<Toggle>();
    //    toggles[id].isOn = true;
    //}


    public void ClearField(TMP_InputField inputField)
    {
        inputField.text = string.Empty;
    }

    public void AddElementFromInput(TMP_InputField inputField)
    {
        if (inputField.text != string.Empty)
        {
            BinaryManger.AddElementToTemp(inputField.text);
            Debug.Log("Added");
            ClearField(inputField);
        }
    }

    public void ShowTemp()
    {
        for (int i = 0; i < Data.tempList.Count; i++)
        {
            Debug.Log(Data.tempList[i]);
        }
    }

    public void Close(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void Open(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }



    public void ChangeGroup(int i)
    {
        switch (i)
        {
            case 0:
                GroupManager.SetActiveGroupName(Data.FirstGroupfileName);
                break;
            case 1:
                GroupManager.SetActiveGroupName(Data.SecondGroupfileName);
                break;
            case 2:
                GroupManager.SetActiveGroupName(Data.ThirdGroupfileName);
                break;
        }
    }

    public void ShowGroup(TMP_Text textField)
    {
        textField.text = Data.ActiveGroupName;
    }

    public void FillText(GameObject gameObject, List<string> current)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<LayoutElement>().GetComponentInChildren<TMP_Text>().text = current[i];
            gameObject.transform.GetChild(i).name = current[i];
        }
    }

    private void LoadData()
    {
        BinaryManger.ReadFromStreamingAssets();
        GroupManager.SetActiveGroupName(Data.FirstGroupfileName);
        BinaryManger.Load(Data.path, Data.Links);
        GroupManager.SetActiveGroupName(Data.SecondGroupfileName);
        BinaryManger.Load(Data.path, Data.Items);
        GroupManager.SetActiveGroupName(Data.ThirdGroupfileName);
        BinaryManger.Load(Data.path, Data.Stores);
    }

    public void LoadOnUI()
    {
        FillText(UIElements[0], Data.Items);
        FillText(UIElements[1], Data.Stores);
    }



    public void LoadOnAction()
    {
        BinaryManger.Load(Data.path, Data.tempList);
    }

    public void OpenURL(int i)
    {
        Application.OpenURL(Data.tempList[i]);
    }

    public void GetFinalList(string str)
    {
        Data.tempList.Clear();
        foreach (string line in Data.Links)
            if (line.Contains(str.Substring(0, 3)))
            {
                Data.tempList.Add(line);
            }
    }

    public void ChangeItemSource(int i)
    {
        var toggles = tGroupInstance.GetComponentsInChildren<Toggle>();
        if (toggles[i].isOn)
        {
            GetFinalList(Data.Stores[i]);
            ChangeActiveImage(i);
        }
    }
}
