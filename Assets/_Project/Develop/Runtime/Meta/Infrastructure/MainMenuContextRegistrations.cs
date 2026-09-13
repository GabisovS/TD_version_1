using UnityEngine;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    //L1 - Поддержка глобального контейнера и контейнера сцены
    //L2 - Фиксим проблему с деинициализацией презентера кошелька
    public class MainMenuContextRegistrations
    {
        //Процесс регистрации сервисов на сцене главного меню
        public static void Process(DIContainer container)
        {
            //container.RegisterAsSingle(CreateWalletPresenter).NonLazy();
        }

/*        //L2 - Фиксим проблему с деинициализацией презентера кошелька
        private static WalletPresenter CreateWalletPresenter(DIContainer c)
        {
            IconTextListView walletView = Object.FindObjectOfType<IconTextListView>();

            WalletPresenter walletPresenter = c.Resolve<ProjectPresentersFactory>().CreateWalletPresenter(walletView);

            return walletPresenter;
        }*/
    }
}
