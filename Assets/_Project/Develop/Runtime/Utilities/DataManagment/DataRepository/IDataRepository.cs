using System;
using System.Collections;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataRepository
{
    //L2 - Хранилище данных
    public interface IDataRepository
    {
        IEnumerator Read(string key, Action<string> onRead); // акшон вызывается как реакция что чтение прошло
        IEnumerator Write(string key, string serializedData);
        IEnumerator Remove(string key);
        IEnumerator Exists(string key, Action<bool> onExistsResult); //акшон как реакция о завершении процесса проверки
    }
}
