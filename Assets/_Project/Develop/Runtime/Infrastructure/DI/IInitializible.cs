namespace Assets._Project.Develop.Runtime.Infrastructure.DI
{
    //L2 - Проблема инициализации и деинициализации сцены
    //L2 - Прокачиваем контейнер
    public interface IInitializible
    {
        void Initialize(); //метод для реализации логики инициализации сущностей
    }
}
