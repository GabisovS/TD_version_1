namespace Assets._Project.Develop.Runtime.Utilities
{
    //L5 - Использовать ли NonAlloc касты
    //L5 - Делаем буффер
    public class Buffer<T>
    {
        public T[] Items;
        public int Count;

        public Buffer(int initialSize)
        {
            Items = new T[initialSize];
            Count = 0;
        }
    }
}
