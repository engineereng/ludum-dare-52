using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    static string path = Application.persistentDataPath + "/levels.save";
    public static void SaveLevelsUnlocked (LevelsUnlocked levelsUnlocked)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        ;
        FileStream stream = new FileStream(path, FileMode.Create);
        LevelData data = new LevelData(levelsUnlocked);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LevelData LoadLevelsUnlocked ()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        } else 
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
