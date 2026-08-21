using System.Collections;
using UnityEngine;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    //L1 - Организуем точки входа и передачу контейнера
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        public override IEnumerator Initialize(DIContainer container)
        {
            _container = container;

            Debug.Log("Инициаплизация сцены меню");

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
                ICoroutinePerformer coroutinePerformer =_container.Resolve<ICoroutinePerformer>();
                coroutinePerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay));
            }
        }
    }
}
