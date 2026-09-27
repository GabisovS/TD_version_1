namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems
{
    //L4 - Начинаем организацию систем
    public interface IUpdatableSystem : IEntitySystem
    {
        //Маркировочный интерфейс для подписки на коллбеки события обновления

        void OnUpdate(float deltaTime);
    }
}
