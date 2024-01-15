using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace keksikq9.Json
{
    public static class JsonSavingSystem<T>
    {
        public static void Save(T savingObject, string path)
        {
            File.WriteAllText(path,
            JsonConvert.SerializeObject(savingObject, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }
            ));
        }

        public static void SaveArray(T[] savingObjects, string path)
        {
            File.WriteAllText(path,
            JsonConvert.SerializeObject(savingObjects, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }
            ));
        }

        public static void SaveList(List<T> savingObjects, string path)
        {
            File.WriteAllText(path,
            JsonConvert.SerializeObject(savingObjects, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }
            ));
        }

        public static void SaveDictionary(Dictionary<T, T> savingObjects, string path)
        {
            File.WriteAllText(path,
            JsonConvert.SerializeObject(savingObjects, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }
            ));
        }

        public static T Load(string path)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }

        public static T[] LoadArray(string path)
        {
            return JsonConvert.DeserializeObject<T[]>(File.ReadAllText(path));
        }

        public static List<T> LoadList(string path)
        {
            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path));
        }

        public static Dictionary<T, T> LoadDictionary(string path)
        {
            return JsonConvert.DeserializeObject<Dictionary<T, T>>(File.ReadAllText(path));
        }
    }
}