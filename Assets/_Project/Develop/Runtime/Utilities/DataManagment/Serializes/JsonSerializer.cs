using Newtonsoft.Json;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.Serializes
{
    //L2 - Реализация json сериализатора
    //L2 - Доп. настройки сериализации. Тип форматирования
    //L2 - Доп. настройки сериализации. Обработка имени типа
    public class JsonSerializer : IDataSerializer
    {
        public TData Deserialize<TData>(string serializedData)
        {
            return JsonConvert.DeserializeObject<TData>(serializedData, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
            });
        }

        public string Serialize<TData>(TData data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                //Formatting = Formatting.Indented, - для дебага и конфигов
                Formatting = Formatting.None,
                TypeNameHandling = TypeNameHandling.Auto,
            });
        }
    }
}
