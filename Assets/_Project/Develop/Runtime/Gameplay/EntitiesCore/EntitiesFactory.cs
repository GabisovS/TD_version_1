using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
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
        public Entity CreateTestEntity(Vector3 position)
        {
            Entity entity = CreateEmpty();

            //entity
            //    .AddComponent(new MoveDirection() { Value = new ReactiveVariable<Vector3>(Vector3.forward) })
            //    .AddComponent(new MoveSpeed() { Value = new ReactiveVariable<float>(10) });
            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10));

            entity.AddSystem(new RigidbodyMovementSystem());

            _monoEntitiesFactory.Create(entity, position, "Entities/TestEntity");


            //entity.AddSystem(new RigidbodyMovementSystem());

            //Сущность автоматически добавляется в сервис жизненного цикла
            _entitiesLifeContext.Add(entity);

            return entity;
        }

        //Метод создания пустой сущности, которую потом наполняем компонентами
        private Entity CreateEmpty() => new Entity();
    }
}
