namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems
{
    //L4 - Начинаем организацию систем
    public interface IDisposableSystem : IEntitySystem
    {
        //Маркировочный интерфейс для подписки на коллбеки диспоуза системы
        void OnDispose();
    }
}
