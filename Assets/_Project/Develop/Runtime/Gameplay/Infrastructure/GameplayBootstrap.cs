using System;
using System.Collections;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    //L1 - Организуем точки входа и передачу контейнера
    //L1 - Передача доп параметров на сцену
    //L4 - Стартовая подготовка. Тестовый геймплей
    //L4 - Тестируем работу систем
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;

        [SerializeField] private TestGameplay _testGameplay;
        private EntitiesLifeContext _entitiesLifeContext;

        //L1 - Поддержка глобального контейнера и контейнера сцены
        public override void ProcessRigstrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");
        
            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log($"Вы попали на уровень {_inputArgs.LevelNumber}");

            Debug.Log("Инициаплизация гейплейной сцены");

            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();

            _testGameplay.Initialize(_container);

            yield break; //брейк потому что ожидать тут нечего
        }

        public override void Run()
        {
            Debug.Log("Старт геймплейной сцены");

            _testGameplay.Run();
        }

        private void Update()
        {
            _entitiesLifeContext?.Update(Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinesPerformer coroutinePerformer = _container.Resolve<ICoroutinesPerformer>();
                coroutinePerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
            }
        }
    }
}
