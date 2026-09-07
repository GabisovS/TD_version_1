namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders
{
        //L2 - Реализуем идею актуализации данных
        public interface IDataReader<TData> where TData : ISaveData
        {
            void ReadFrom(TData data);
        }
}
