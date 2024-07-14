using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.DataHandler
{
    [PublicAPI]
    public static class DataHandler
    {
        private static string DataDirectory => Application.persistentDataPath + "/";
        
        public static void Save<T>(string key, T data)
        {
            var path = DataDirectory + key;
            FileStream dataStream = new FileStream(path, FileMode.Create);

            BinaryFormatter converter = new BinaryFormatter();
            converter.Serialize(dataStream, new Data<T>() { data = data });
            
            dataStream.Close();
        }
        
        public static T Load<T>(string key, T defaultValue = default)
        {
            var path = DataDirectory + key;
            
            if(File.Exists(path))
            {
                // File exists 
                FileStream dataStream = new FileStream(path, FileMode.Open);

                BinaryFormatter converter = new BinaryFormatter();
                var saveData = converter.Deserialize(dataStream) as Data<T>;

                dataStream.Close();
                return saveData != null ? saveData.data : defaultValue;
            }
            else
            {
                // File does not exist
                Debug.LogError("Save file not found in " + path);
                return defaultValue;
            }
        }
    }

    public interface IData
    {
        
    }

    [Serializable]
    public class Data<T>
    {
        public T data;
    }

    [Serializable]
    public class OriginalData<T> : IData where T : IConvertible
    {
        public T data;
    }
}