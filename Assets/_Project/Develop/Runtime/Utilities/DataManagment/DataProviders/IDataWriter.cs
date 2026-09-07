namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders
{
        //L2 - Реализуем идею актуализации данных
        public interface IDataWriter<TData> where TData : ISaveData
        {
            void WriteTo(TData data);   
        }

}
