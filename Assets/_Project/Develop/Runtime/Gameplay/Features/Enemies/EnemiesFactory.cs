using System;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Enemies
{
    //L7 - Создаем фабрику врагов
    public class EnemiesFactory
    {
        private readonly DIContainer _container;

        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        public EnemiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
        }

        //принимаем конфиг врага здесь, потмоу что враги конфигурируются в зависимости от уровня
        public Entity Create(Vector3 position, EntityConfig config)
        {
            Entity entity;

            //создаем врага в зависимости от конфига врага
            switch (config)
            {
                case GhostConfig ghostConfig:
                    entity = _entitiesFactory.CreateGhost(position, ghostConfig);
                    _brainsFactory.CreateGhostBrain(entity);
                    break;

                default:
                    throw new ArgumentException($"Not support {config.GetType()} type config");
            }
            
            //L7 - Добавляем фичу команд
            entity.AddTeam(new ReactiveVariable<Teams>(Teams.Enemies));

            //перед выдачей врага передаем его в жизненный цикл сущности
            _entitiesLifeContext.Add(entity);

            return entity;
        }
    }
}
