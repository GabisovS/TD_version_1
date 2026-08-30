using System.Collections;
using UnityEngine;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    //L1 - Организуем точки входа и передачу контейнера
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;

        //test
        private WalletService _walletService;

        //L1 - Поддержка глобального контейнера и контейнера сцены
        public override void ProcessRigstrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }
        public override IEnumerator Initialize()
        {
            Debug.Log("Инициаплизация сцены меню");

            _walletService = _container.Resolve<WalletService>();

            yield break; //брейк потому что ожидать тут нечего
        }


        public override void Run()
        {
            Debug.Log("Старт сцены меню");

        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinePerformer coroutinePerformer = _container.Resolve<ICoroutinePerformer>();
                //L1 - Передача доп параметров на сцену
                coroutinePerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(2)));
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _walletService.Add(CurrencyTypes.Gold, 10);
                Debug.Log("Золота осталось: " +_walletService.GetCurrency(CurrencyTypes.Gold).Value);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (_walletService.Enough(CurrencyTypes.Gold, 10))
                {
                    _walletService.Spend(CurrencyTypes.Gold, 10);
                    Debug.Log("Золота осталось: " + _walletService.GetCurrency(CurrencyTypes.Gold).Value);
                }
            }
        }
    }
}
