namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.KeysStorage
{
    //L2 - Ключ для сохранений
    public interface IDataKeysStorage
    {
        string GetKeyFor<TData>() where TData : ISaveData;
    }
}
