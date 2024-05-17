using System;
using System.IO;
using UnityEngine;

namespace Test.Database.Helper
{
    public static class JsonHelper
    {
        [Serializable]
        public class Wrapper<T>
            where T : class
        {
            public T[] Datas;
        }

        public static void WriteData<T>(T data, string path)
            where T : class
        {
            using (var writer = new StreamWriter(path))
            {
                var json = JsonUtility.ToJson(data, true);
                writer.Write(json);
            }
        }

        public static void WriteDatas<T>(T[] datas, string path)
            where T : class
        {
            using (var writer = new StreamWriter(path))
            {
                var wrapper = new Wrapper<T> { Items = datas };
                var json = JsonUtility.ToJson(wrapper, true);
                writer.Write(json);
            }
        }

        public static T ReadData<T>(string path)
            where T : class
        {
            if (!File.Exists(path))
            {
                UnityEngine.Debug.LogError("Can't get data");
                return null;
            }
            using (var reader = new StreamReader(path))
            {
                var json = reader.ReadToEnd();
                return JsonUtility.FromJson<T>(json);
            }
        }
        public static T[] ReadDatas<T>(string path)
            where T : class
        {
            if (!File.Exists(path))
            {
                UnityEngine.Debug.LogError("Can't get data");
                return null;
            }
            using (var reader = new StreamReader(path))
            {
                var json = reader.ReadToEnd();
                var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
                return wrapper.Datas;
            }
        }
    }
}
