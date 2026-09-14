using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.UI;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.MainMenu;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    //L1 - Поддержка глобального контейнера и контейнера сцены
    //L3 - Фиксим проблему с деинициализацией презентера кошелька
    public class MainMenuContextRegistrations
    {
        //Процесс регистрации сервисов на сцене главного меню
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateMainMenuUIRoot).NonLazy();
            container.RegisterAsSingle(CreateMainMenuPresentersFactory);
            container.RegisterAsSingle(CreateMainMenuScreenPresenter).NonLazy(); //Нонлази чтобы инишиалайз отработал
            container.RegisterAsSingle(CreateMainMenuPopupService);
        }

        /*        //L2 - Фиксим проблему с деинициализацией презентера кошелька
                private static WalletPresenter CreateWalletPresenter(DIContainer c)
                {
                    IconTextListView walletView = Object.FindObjectOfType<IconTextListView>();

                    WalletPresenter walletPresenter = c.Resolve<ProjectPresentersFactory>().CreateWalletPresenter(walletView);

                    return walletPresenter;
                }*/

        //L3 - Регистрируем сервис попапов для главного меню
        private static MainMenuPopupService CreateMainMenuPopupService(DIContainer c)
        {
            return new MainMenuPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                c.Resolve<MainMenuUIRoot>());
        }

        //L3 - Как получать доступ к слоям
        private static MainMenuUIRoot CreateMainMenuUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            MainMenuUIRoot mainMenuUIRootPrefab = resourcesAssetsLoader
                .Load<MainMenuUIRoot>("UI/MainMenu/MainMenuUIRoot");

            return Object.Instantiate(mainMenuUIRootPrefab);
        }

        //L3 - Организация главного экрана
        private static MainMenuPresentersFactory CreateMainMenuPresentersFactory(DIContainer c)
        {
            return new MainMenuPresentersFactory(c);
        }

        private static MainMenuScreenPresenter CreateMainMenuScreenPresenter(DIContainer c)
        {
            MainMenuUIRoot uiRoot = c.Resolve<MainMenuUIRoot>();

            MainMenuScreenView view = c
                .Resolve<ViewsFactory>()
                .Create<MainMenuScreenView>(ViewIDs.MainMenuScreen, uiRoot.HUDLayer);

            MainMenuScreenPresenter presenter = c
                .Resolve<MainMenuPresentersFactory>()
                .CreateMainMenuScreen(view);

            return presenter;
        }
    }
}
