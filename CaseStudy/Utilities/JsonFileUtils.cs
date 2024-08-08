using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Case.Utilities
{
    public static class JsonFileUtils
    {
        public static async Task<T> ReadAsync<T>(string filePath)
        {
            try
            {
                string jsonString = await File.ReadAllTextAsync(filePath);
                T obj = JsonConvert.DeserializeObject<T>(jsonString);
                return obj;
            }
            catch (FileNotFoundException fileNotFoundEx)
            {
                Console.WriteLine($"File not found: {fileNotFoundEx.Message}");
                throw;
            }
            catch (UnauthorizedAccessException unauthorizedEx)
            {
                Console.WriteLine($"Access denied: {unauthorizedEx.Message}");
                throw;
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"File IO error: {ioEx.Message}");
                throw;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON Deserialize error: {jsonEx.Message}");
                throw;
            }
        }

        public static async Task WriteAsync<T>(string filePath, T obj)
        {
            try
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },
                    Formatting = Formatting.Indented
                };

                string jsonString = JsonConvert.SerializeObject(obj, jsonSettings);
                await File.WriteAllTextAsync(filePath, jsonString);
            }
            catch (UnauthorizedAccessException unauthorizedEx)
            {
                Console.WriteLine($"Access denied: {unauthorizedEx.Message}");
                throw;
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"File IO error: {ioEx.Message}");
                throw;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON Serialize error: {jsonEx.Message}");
                throw;
            }
        }
    }
}
