using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders
{
    //L2 - Создаем провайдер данных
    //L2 - Задача сброса данных до дефолтного состояния
    //L2 - Реализуем идею актуализации данных
    public abstract partial class DataProvider <TData> where TData : ISaveData
    {
        private readonly ISaveLoadService _saveLoadSerivce;

        private readonly List<IDataWriter<TData>> _writers = new();
        private readonly List<IDataReader<TData>> _readers = new();

        private TData _data;

        protected DataProvider(ISaveLoadService saveLoadSerivce)
        {
            _saveLoadSerivce = saveLoadSerivce;
        }

        public void RegisterWriter(IDataWriter<TData> writer)
        {
            if (_writers.Contains(writer))
                throw new ArgumentException(nameof(writer));

            _writers.Add(writer);
        }

        public void RegisterReader(IDataReader<TData> reader)
        {
            if (_readers.Contains(reader))
                throw new ArgumentException(nameof(reader));

            _readers.Add(reader);
        }

        public IEnumerator Load()
        {
            yield return _saveLoadSerivce.Load<TData>(loadedData => _data = loadedData);

            SendDataToReaders();
        }

        public IEnumerator Save()
        {
            UpdateDataFromWriters(); //перед сохранением

            yield return _saveLoadSerivce.Save(_data);
        }

        public IEnumerator Exists(Action<bool> onExistsResult)
        {
            yield return _saveLoadSerivce.Exists<TData>(result => onExistsResult?.Invoke(result));
        }

       public void Reset()
        {
            _data = GetOriginData();
            SendDataToReaders();
        }

        protected abstract TData GetOriginData(); // метод для сброса до дефолтного состояния

        private void SendDataToReaders()
        {
            foreach (IDataReader<TData> reader in _readers)
                reader.ReadFrom(_data);
        }

        private void UpdateDataFromWriters()
        {
            foreach (IDataWriter<TData> writer in _writers)
                writer.WriteTo(_data);
        }
    }
}
