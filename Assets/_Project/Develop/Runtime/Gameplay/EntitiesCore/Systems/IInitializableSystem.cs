namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems
{
    //L4 - Начинаем организацию систем
    public interface IInitializableSystem : IEntitySystem
    {
        //Маркировочный интерфейс для подписки на коллбеки инициализации системы
        void OnInit(Entity entity);
    }
}
