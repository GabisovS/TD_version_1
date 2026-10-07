using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using Assets._Project.Develop.Runtime.Gameplay.Features.Attack;
using Assets._Project.Develop.Runtime.Gameplay.Features.Attack.Shoot;
using Assets._Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage;
using Assets._Project.Develop.Runtime.Gameplay.Features.LifeCycle;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.Sensors;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities;
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
        private readonly CollidersRegistryService _collidersRegistryService;
        private readonly MonoEntitiesFactory _monoEntitiesFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
           _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
            _collidersRegistryService = _container.Resolve<CollidersRegistryService>();
        }

        //L5 - Готовим метод создания основного героя
        //L5 - Начинаем добавлять данные для работы процесса атаки
        //L5 - Добавляем компонент для определения движения сущности
        //L5 - Добиваем и проверяем процесс атаки
        public Entity CreateHero(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/Hero");

            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(900))
                .AddMaxHealth(new ReactiveVariable<float>(100))
                .AddCurrentHealth(new ReactiveVariable<float>(100))
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(2))
                .AddDeathProcessCurrentTime()
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddAttackProcessInitialTime(new ReactiveVariable<float>(3))
                .AddAttackProcessCurrentTime()
                .AddInAttackProcess()
                .AddStartAttackRequest()
                .AddStartAttackEvent()
                .AddEndAttackEvent()
                .AddAttackDelayTime(new ReactiveVariable<float>(1)) // L5 -Проверяем работу задержки
                .AddAttackDelayEndEvent() // L5 -Проверяем работу задержки
                .AddInstantAttackDamage(new ReactiveVariable<float>(50)) //L5 - Проверяем механику выстрела
                .AddAttackCanceledEvent()    //L5 - Механика отмены атаки. Данные
                .AddAttackCooldownInitialTime(new ReactiveVariable<float>(2)) //L5 - Тестируем кулдаун атаки
                .AddAttackCooldownCurrentTime() //L5 - Тестируем кулдаун атаки
                .AddInAttackCooldown(); //L5 - Тестируем кулдаун атаки

            ICompositeCondition canMove = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canRotate = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canStartAttack = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false))
                .Add(new FuncCondition(() => entity.InAttackProcess.Value == false))
                .Add(new FuncCondition(() => entity.IsMoving.Value == false))
                .Add(new FuncCondition(() => entity.InAttackCooldown.Value == false)); //L5 - Тестируем кулдаун атаки

            //L5 - Механика отмены атаки. Данные
            ICompositeCondition mustCancelAttack = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.IsMoving.Value));

            entity
                .AddCanMove(canMove)
                .AddCanRotate(canRotate)
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanApplyDamage(canApplyDamage)
                .AddCanStartAttack(canStartAttack)
                .AddMustCancelAttack(mustCancelAttack);     //L5 - Механика отмены атаки. Данные

            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new AttackCancelSystem())  //L5 - Проверяем механику отмены атаки
                .AddSystem(new StartAttackSystem())
                .AddSystem(new AttackProcessTimerSystem())
                .AddSystem(new AttackDelayEndTriggerSystem()) // L5 -Проверяем работу задержки
                .AddSystem(new InstantShootSystem(this)) //L5 - Проверяем механику выстрела
                .AddSystem(new EndAttackSystem())
                .AddSystem(new AttackCooldownTimerSystem()) //L5 - Тестируем кулдаун атаки
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            _entitiesLifeContext.Add(entity);

            return entity;
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
        //L5 - Фича получения урона. Компоненты
        //L5 - Отслеживание контактов. Прикрепляем данные
        //L5 - Как будем получать сущности для нанесения урона
        //L5 - Фича нанесения урона касанием. Данные
        //L5 - Добавляем компонент для определения движения сущности
        public Entity CreateGhost(Vector3 position)
        {
            Entity entity = CreateEmpty();
            
            _monoEntitiesFactory.Create(entity, position, "Entities/Ghost");

            //entity
            //    .AddComponent(new MoveDirection() { Value = new ReactiveVariable<Vector3>(Vector3.forward) })
            //    .AddComponent(new MoveSpeed() { Value = new ReactiveVariable<float>(10) });
            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(900))
                .AddMaxHealth(new ReactiveVariable<float>(100))
                .AddCurrentHealth(new ReactiveVariable<float>(100))
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(2))
                .AddDeathProcessCurrentTime()
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddContactsDetectingMask(1 << LayerMask.NameToLayer("Characters"))
                .AddContactCollidersBuffer(new Buffer<Collider>(64))
                .AddContactEntitiesBuffer(new Buffer<Entity>(64))
                .AddBodyContactDamage(new ReactiveVariable<float>(50));

            ICompositeCondition canMove = new CompositeCondition()
               .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canRotate = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            entity
                  .AddCanMove(canMove)
                  .AddCanRotate(canRotate)
                  .AddMustDie(mustDie)
                  .AddMustSelfRelease(mustSelfRelease)
                  .AddCanApplyDamage(canApplyDamage);

            //L5 - Проверяем систему получения урона
            //L5 - Тестируем системы детектирования контактов
            //L5 - Проверяем систему нанесения урона касанием
            //L5 - Тестируем систему (отклшючения коллайдеров)
            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_collidersRegistryService))
                .AddSystem(new DealDamageOnContactSystem())
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            //entity.AddSystem(new RigidbodyMovementSystem());

            //Сущность автоматически добавляется в сервис жизненного цикла
            _entitiesLifeContext.Add(entity);

            return entity;
        }

        //Метод создания пустой сущности, которую потом наполняем компонентами
        private Entity CreateEmpty() => new Entity();
    }
}
