using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;

public class save : MonoBehaviour {

    void Start(){InvokeRepeating("SaveFile", 1f, .5f); LoadFile();}
    public void SaveFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if(File.Exists(destination)) {file = File.OpenWrite(destination);}
        else {file = File.Create(destination);}

        GameData data = new GameData(GameObject.Find("Player").GetComponent<PlayerSwipe>().gold, IdleManager.gtg, UiScript.goodGraphics, PlayerSwipe.magnetRadius, PlayerSwipe.timeSpeed);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();

        if(!IdleManager.gtg) IdleManager.gtg = true;
    }

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if(File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData) bf.Deserialize(file);
        file.Close();

        GameObject.Find("Player").GetComponent<PlayerSwipe>().gold = data._gold;
        IdleManager.gtg = data._gtg;
        UiScript.goodGraphics = data._goodGraphics;
        PlayerSwipe.magnetRadius = data._magnetRadius;
        PlayerSwipe.timeSpeed = data._timeSpeed;
    }
}