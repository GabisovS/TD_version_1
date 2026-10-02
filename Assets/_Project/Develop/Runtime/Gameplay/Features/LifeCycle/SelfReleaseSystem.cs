using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.LifeCycle
{
    //L5 - Как пользоваться сервисами в системах. Делаем механику уничтожения при смерти
    //L5 - Продолжительная обработка смерти. Дорабатываем релиз
    //L5 - Внедряем новые условия в систему релиза и смерти
    //L5 - Промежуточный итог по фиче смерти
    public class SelfReleaseSystem : IInitializableSystem, IUpdatableSystem
    {
        private readonly EntitiesLifeContext _entitiesLifeContext;

        private Entity _entity;

        private ICompositeCondition _mustSelfRelease;

        public SelfReleaseSystem(EntitiesLifeContext entitiesLifeContext)
        {
            _entitiesLifeContext = entitiesLifeContext;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _mustSelfRelease = entity.MustSelfRelease;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_mustSelfRelease.Evaluate())
                _entitiesLifeContext.Release(_entity);
        }
    }
}
