using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Application = UnityEngine.Application;
using Debug = Frost.Debug;

public static class SaveManager
{
    public static bool Save<T>(T _object,string _saveName = "Save")
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            string saveFolder = $"{Application.persistentDataPath}/SaveGame";
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            string savePath = $"{Application.persistentDataPath}/SaveGame/{_saveName}.save";

            FileStream stream = new FileStream(savePath, FileMode.OpenOrCreate);

            formatter.Serialize(stream, _object);
            stream.Close();
            return true;
        } 
        catch (Exception _e)
        {
            Debug.LogError(_e);
            return false;
        }
    }
    
    public static bool SaveAtPath<T>(T _object,string _path)
    {
        try
        {
            if (!System.IO.Directory.Exists(_path))
                System.IO.Directory.CreateDirectory(_path);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(_path, FileMode.OpenOrCreate);

            formatter.Serialize(stream, _object);
            stream.Close();
            return true;
        }
        catch (Exception _e)
        {
            Debug.LogError(_e);
            return false;
        }
    }

    public static T LoadAtPath<T> (string _path) where T : class,new()
    {
        if (!System.IO.File.Exists(_path))
            return null;

        T loadObject = new T();
        BinaryFormatter formatter = new BinaryFormatter();
        
        FileStream stream = File.Open(_path, FileMode.Open);
        loadObject = formatter.Deserialize(stream) as T;
        stream.Close();
        
        return loadObject;
    }
    
    public static T Load<T> (string _saveName) where T : class,new()
    {
        T loadObject = new T();
        BinaryFormatter formatter = new BinaryFormatter();
        
        string savePath = $"{Application.persistentDataPath}/SaveGame/{_saveName}.save";

        if (!System.IO.File.Exists(savePath))
            return null;

        FileStream stream = File.Open(savePath, FileMode.Open);
        loadObject = formatter.Deserialize(stream) as T;
        stream.Close();
        
        return loadObject;
    }

    public static List<T> GetAllSaves<T>(string _path = "") where T : class,new()
    {
        if(string.IsNullOrEmpty(_path))
            _path = $"{Application.persistentDataPath}/SaveGame";

        if (!System.IO.Directory.Exists(_path))
            return null;

        BinaryFormatter formatter = new BinaryFormatter();
        List<T> saves = new List<T>();
        string[] savesFile = Directory.GetFiles(_path);

        foreach (var saveFile in savesFile)
        {
            FileStream stream = File.Open(saveFile, FileMode.Open);
            T loadObject = formatter.Deserialize(stream) as T;
            stream.Close();

            saves.Add(loadObject);
        }

        return saves;
    }
    
    public static bool DeleteSaveAtPath (string _path)
    {
        try
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
                return true;
            }
            
            return false;
        } 
        catch (Exception _e)
        {
            Debug.LogError(_e);
            return false;
        }
    }

    public static bool DeleteSave (string _saveName)
    {
        try
        {
            string savePath = $"{Application.persistentDataPath}/SaveGame/{_saveName}.save";

            if (File.Exists(savePath))
            {
                File.Delete(savePath);
                return true;
            }
            
            return false;
        } 
        catch (Exception _e)
        {
            Debug.LogError(_e);
            return false;
        }
    }
}
