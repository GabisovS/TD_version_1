using System;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataRepository;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.KeysStorage;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.Serializes;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Infrastructure.EntryPoint
{
    //L1 - Реализуем точку входа
    public class ProjectContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinePerformer>(CreateCoroutinePerformer);
            container.RegisterAsSingle(CreateConfigsProvuderService);
            container.RegisterAsSingle(CreateResourcesAssetsLoader);
            container.RegisterAsSingle(CreateSceneLoaderService);
            container.RegisterAsSingle(CreateSceneSwitcherService);
            container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreen);
            
            //L2 - Проверяем работу DataProvider
            container.RegisterAsSingle(CreateWalletService).NonLazy();
            
            container.RegisterAsSingle<ISaveLoadService>(CreateSaveLoadService);
            container.RegisterAsSingle(CreatePlayerDataProvider);
        }

        //L2 - Задача сброса данных до дефолтного состояния
        //L2 - Реализуем сброс данных на старте игры
        //L2 - Откуда брать стартовое состояние данных
        private static PlayerDataProvider CreatePlayerDataProvider(DIContainer c)
    => new PlayerDataProvider(
        c.Resolve<ISaveLoadService>(), //достаем из контейнера ISaveloadService
        c.Resolve<ConfigsProviderService>());


        //L2 - Регистрируем сервис
        private static SaveLoadService CreateSaveLoadService(DIContainer c)
        {
            IDataSerializer dataSerializer = new JsonSerializer();
            IDataKeysStorage dataKeysStorage = new MapDataKeysStorage();

            string saveFolderPath = Application.isEditor ? Application.dataPath : Application.persistentDataPath;

            IDataRepository dataRepository = new LocalFileDataRepository(saveFolderPath, "json");

            return new SaveLoadService(dataSerializer, dataKeysStorage, dataRepository);
        }

        //L2 - регистрируем и првоеряем сервис кошелька
        private static WalletService CreateWalletService(DIContainer c)
        {
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                currencies[currencyType] = new ReactiveVariable<int>();

            return new WalletService(currencies, c.Resolve<PlayerDataProvider>());
        }

        //L1 - Организуем переход между сценами
        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
            => new SceneSwitcherService(
                c.Resolve<SceneLoaderService>(),
                c.Resolve<ILoadingScreen>(),
                c);

        //L1 - Сервис подгрузки сцен
        private static SceneLoaderService CreateSceneLoaderService(DIContainer c)
            => new SceneLoaderService();

        //L1 - Реализуем точку входа
        private static ConfigsProviderService CreateConfigsProvuderService(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();  
            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);

            return new ConfigsProviderService(resourcesConfigsLoader);
        }

        //L1 - Реализуем точку входа
        private static ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c) 
            => new ResourcesAssetsLoader();

        //L1 - Реализуем точку входа
        private static CoroutinesPerformer CreateCoroutinePerformer(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            CoroutinesPerformer coroutinesPerformerPrefab = resourcesAssetsLoader
                .Load<CoroutinesPerformer>("Utilities/CoroutinePerformer");

            return Object.Instantiate(coroutinesPerformerPrefab); 
        }

        //L1 - Внедряем загрузрчный экран
        private static StandartLoadingScreen CreateLoadingScreen(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            StandartLoadingScreen standartLoadingScreenPrefab = resourcesAssetsLoader
                .Load<StandartLoadingScreen>("Utilities/StandartLoadingScreen");

            return Object.Instantiate(standartLoadingScreenPrefab);
        }
    }
}

