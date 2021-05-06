using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
    public static void Save(GameManager data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "MyData.Data";
        FileStream file = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(data);
        formatter.Serialize(file, playerData);
        file.Close();
    }

    public static PlayerData Load()
    {
        string path = Application.persistentDataPath + "MyData.Data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);

            PlayerData LoadData = formatter.Deserialize(file) as PlayerData;
            file.Close();

            return LoadData;


        }
        else
        {
            Debug.Log("File Not Found");
            return null;
        }
    }
}
