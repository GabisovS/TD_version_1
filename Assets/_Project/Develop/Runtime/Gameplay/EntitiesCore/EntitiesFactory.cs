using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.LifeCycle;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    //L4 - Фабрика сущностей
    //L4 - Парочка красивостей
    //L4 - Тестируем работу систем
    //L4 - Тестируем работу с MonoEntity
    //L4 - Пользуемся сгенерированным кодом
    //L4 - Промежуточное заключение по организации сущностей в нашей игре
    public class EntitiesFactory
    {
        private readonly DIContainer _container;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        private readonly MonoEntitiesFactory _monoEntitiesFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
           _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
        }

        //L4 - Подключаем систему движения к тестовой сущности
        //L5 - Подготовка к созданию призрака
        //L5 - Конфигурируем призрака и тестим
        //L5 - Добавляем механику смерти призраку
        //L5 - Добавляем систему релиза к сущности
        //L5 - Тестирование и новые проблемки
        //L5 - Внедряем условия для движения и поворота
        //L5 - Дорабатываем остальные системы. Новые условия
        //L5 - Промежуточный итог по фиче смерти
        public Entity CreateGhost(Vector3 position)
        {
            Entity entity = CreateEmpty();

            //entity
            //    .AddComponent(new MoveDirection() { Value = new ReactiveVariable<Vector3>(Vector3.forward) })
            //    .AddComponent(new MoveSpeed() { Value = new ReactiveVariable<float>(10) });
            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(900))
                .AddMaxHealth(new ReactiveVariable<float>(100))
                .AddCurrentHealth(new ReactiveVariable<float>(100))
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(2))
                .AddDeathProcessCurrentTime();
                //.AddAttackDelayEndEvent()
                //.AddInstantAttackDamage(new ReactiveVariable<float>(50))
                //.AddAttackCanceledEvent()
                //.AddAttackCooldownInitialTime(new ReactiveVariable<float>(2))
                //.AddAttackCooldownCurrentTime()
                //.AddInAttackCooldown();

            ICompositeCondition canMove = new CompositeCondition()
               .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canRotate = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            //ICompositeCondition canApplyDamage = new CompositeCondition()
            //    .Add(new FuncCondition(() => entity.IsDead.Value == false));

            entity
                  .AddCanMove(canMove)
                  .AddCanRotate(canRotate)
                  .AddMustDie(mustDie)
                  .AddMustSelfRelease(mustSelfRelease);
                  //.AddCanApplyDamage(canApplyDamage);

            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                //.AddSystem(new BodyContactsDetectingSystem())
                //.AddSystem(new BodyContactsEntitiesFilterSystem(_collidersRegistryService))
                //.AddSystem(new DealDamageOnContactSystem())
               // .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
               // .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            _monoEntitiesFactory.Create(entity, position, "Entities/Ghost");


            //entity.AddSystem(new RigidbodyMovementSystem());

            //Сущность автоматически добавляется в сервис жизненного цикла
            _entitiesLifeContext.Add(entity);

            return entity;
        }

        //Метод создания пустой сущности, которую потом наполняем компонентами
        private Entity CreateEmpty() => new Entity();
    }
}
