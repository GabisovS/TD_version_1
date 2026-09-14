using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Core.TestPopup;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;

namespace Assets._Project.Develop.Runtime.UI
{
    //L2 - Фабрика для презентеров
    //L2 - Важный нюанс по работе с контейнером
    public class ProjectPresentersFactory
    {
        private readonly DIContainer _container;

        public ProjectPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        //Создаем CurrencyPresenter
        public CurrencyPresenter CreateCurrencyPresenter(
            IconTextView view,
            IReadOnlyVariable<int> currency,
            CurrencyTypes currencyType)
        {
            return new CurrencyPresenter(
                currency,
                currencyType,
                _container.Resolve<ConfigsProviderService>().GetConfig<CurrencyIconsConfig>(),
                view);
        }


        //L3 - Презентер кошелька
        public WalletPresenter CreateWalletPresenter(IconTextListView view)
        {
            return new WalletPresenter(
                _container.Resolve<WalletService>(),
                this,
                _container.Resolve<ViewsFactory>(),
                view);
        }

        //L3 - Делаем презентер тестового попапа
        public TestPopupPresenter CreateTestPopupPresenter(TestPopupView view)
 {
            return new TestPopupPresenter(
                view);
         //_container.Resolve<ICoroutinesPerformer>());
 }

 /*public LevelTilePresenter CreateLevelTilePresenter(LevelTileView view, int levelNumber)
 {
     return new LevelTilePresenter(
         _container.Resolve<LevelsProgressionService>(),
         _container.Resolve<SceneSwitcherService>(),
         _container.Resolve<ICoroutinesPerformer>(),
         levelNumber,
         view);
 }

 public LevelsMenuPopupPresenter CreateLevelsMenuPopupPresenter(LevelsMenuPopupView view)
 {
     return new LevelsMenuPopupPresenter(
         _container.Resolve<ICoroutinesPerformer>(),
         _container.Resolve<ConfigsProviderService>(),
         this,
         _container.Resolve<ViewsFactory>(),
         view);
 }*/
    }
}
