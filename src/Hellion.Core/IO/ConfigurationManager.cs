using Newtonsoft.Json;
using System.IO;

namespace Hellion.Core.IO
{
    /// <summary>
    /// Configuration Manager.
    /// </summary>
    public static class ConfigurationManager
    {
        /// <summary>
        /// Loads a JSON configuration file.
        /// </summary>
        /// <typeparam name="T">Configuration type</typeparam>
        /// <param name="path">Configuration file path</param>
        /// <returns></returns>
        public static T Load<T>(string path)
        {
            string value = "";

            if (File.Exists(path) == false)
                return default(T);

            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fileStream))
                value = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Saves a JSON configuration file.
        /// </summary>
        /// <typeparam name="T">Configuration type</typeparam>
        /// <param name="value">Configuration value</param>
        /// <param name="path">Configuration file path</param>
        public static void Save<T>(T value, string path)
        {
            var jsonValue = JsonConvert.SerializeObject(value, Formatting.Indented);

            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fileStream))
                writer.Write(jsonValue);
        }
    }
}
