using System;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Stages;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI.States;
using Assets._Project.Develop.Runtime.Gameplay.Features.Enemies;
using Assets._Project.Develop.Runtime.Gameplay.Features.MainHero;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.StageFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;


namespace Assets._Project.Develop.Runtime.Gameplay
{
    //L4 - Стартовая подготовка. Тестовый геймплей
    //L4 - Пользуемся фабрикой
    //L4 - Проверка системы и итог по MonoEntity
    //L4 - Пользуемся сгенерированным кодом
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;
        private BrainsFactory _brainsFactory; //L5 - Тестируем работу ИИ

        //L7 - Поддержка конфигов для персонажей
        [SerializeField] private HeroConfig _heroConfig;
        //[SerializeField] private GhostConfig _ghostConfig; //L7 - Тестируем стейдж зачистки врагов
        [SerializeField] private StageConfig _stageConfig; //L7 - Тестируем стейдж зачистки врагов
        private StagesFactory _stagesFactory;
        private IStage _stage;

        //L7 - Тестируем фабрики
        private MainHeroFactory _mainHeroFactory;
        private EnemiesFactory _enemiesFactory;

        private Entity _entity;
        private Entity _ghost; //L5 - Тестируем работу ИИ

        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();

            _mainHeroFactory = _container.Resolve<MainHeroFactory>();
            _enemiesFactory = _container.Resolve<EnemiesFactory>();

            _stagesFactory = _container.Resolve<StagesFactory>();
        }

        //L4 - Тестируем работу с MonoEntity
        //L7 - Тестируем фабрики
        public void Run()
        {
            _entity = _mainHeroFactory.Create(Vector3.zero);

            //L7 - Тестируем стейдж зачистки врагов
            _stage = _stagesFactory.Create(_stageConfig);
            _stage.Completed.Subscribe(OnCompl);
            _stage.Start();

            //_ghost = _enemiesFactory.Create(Vector3.zero + Vector3.forward * 5, _ghostConfig);

            ////L5 - Готовим метод создания основного героя
            //_entity = _entitiesFactory.CreateHero(Vector3.zero, _heroConfig);

            ////L5 - Тестируем получившуюся реализацию
            //_entity.AddCurrentTarget();
            ////L6 - Тестируем поведение. Проблема текущей реактивности
            //_brainsFactory.CreateMainHeroBrain(_entity, new NearestDamageableTargetSelector(_entity));

            ////L5 - Добавляем коллайдеры. Механика призрака
            ////L6 - Тестируем работу ИИ
            //_ghost = _entitiesFactory.CreateGhost(Vector3.zero+ Vector3.forward*5, _ghostConfig);

            _isRunning = true;
        }

        private void OnCompl()
        {
            Debug.Log("WIN WIN WIN");
            _stage.Cleanup();
        }

        //L5 - Добавляем механику смерти призраку
        private void Update()
        {
            if (_isRunning == false) //если геймплей еще не запущен
                return;

            //L7 - Тестируем стейдж зачистки врагов
            _stage.Update(Time.deltaTime);

            //L7 - Тестируем фабрики
            ////L5 - Фича получения урона. Компоненты
            //if (Input.GetKeyDown(KeyCode.Space))
            //    _entity.TakeDamageRequest.Invoke(50);

            ////L5 - Добиваем и проверяем процесс атаки
            //if (Input.GetKeyDown(KeyCode.R))
            //    _entity.StartAttackRequest.Invoke();

            ////L6 - Тестируем работу ИИ
            //if (Input.GetKeyDown(KeyCode.I))
            //    _brainsFactory.CreateGhostBrain(_ghost);


        }
    }
}
