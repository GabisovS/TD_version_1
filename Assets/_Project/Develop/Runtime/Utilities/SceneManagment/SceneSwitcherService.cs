using System;
using System.Collections;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagment
{
    //L1 - Организуем переход между сценами
    public class SceneSwitcherService
    {
        private readonly SceneLoaderService _sceneLoaderService; //для загрузки сцен
        private readonly ILoadingScreen _loadingScreen; //Для вызова загрузочного экрана
        private readonly DIContainer _projectContainer; //Для передачи контейнера между сценами

        public SceneSwitcherService(
            SceneLoaderService sceneLoaderService, 
            ILoadingScreen loadingScreen, 
            DIContainer projectContainer)
        {
            _sceneLoaderService = sceneLoaderService;
            _loadingScreen = loadingScreen;
            _projectContainer = projectContainer;
        }

        //асинхронная подругзка сцен
        //L1 - Передача доп параметров на сцену
        //L2 - Добавляем NonLazy регистрации
        public IEnumerator ProcessSwitchTo(string sceneName, IInputSceneArgs sceneArgs = null)
        {
            _loadingScreen.Show();

            //ожидаем подгузки пустой сцены чтобы все ресурсы 
            yield return _sceneLoaderService.LoadAsync(Scenes.Empty);

            //после загрузки пустой сцены - загружаем нужную сцену
            yield return _sceneLoaderService.LoadAsync(sceneName);


            //L1 - Организуем точки входа и передачу контейнера
            SceneBootstrap sceneBootstrap = Object.FindObjectOfType<SceneBootstrap>();

            if (sceneBootstrap == null)
                throw new NullReferenceException(nameof(sceneBootstrap) + " not found");

            //Создаем новый экз ДИконтейнера и передаем в него родителя, чтобы каждый раз создавался новый контейнер
            //L1 - Поддержка глобального контейнера и контейнера сцены
            DIContainer sceneContainer = new DIContainer(_projectContainer);

            sceneBootstrap.ProcessRigstrations(sceneContainer,sceneArgs);

            sceneContainer.Initialize();

            //нашли бутстрап на сцене, проинициализировали его и закрыли загрузочный экран
            yield return sceneBootstrap.Initialize();

            _loadingScreen.Hide();
             
            //запускаем сцену
            sceneBootstrap.Run();
        }

    }
}
