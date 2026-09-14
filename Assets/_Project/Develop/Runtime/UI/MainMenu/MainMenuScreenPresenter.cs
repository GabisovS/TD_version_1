using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Wallet;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    //L3 - Организация главного экрана
    //L3 - Возвращаем отображение кошелька на главный экран
    //L3 - Открываем тестовый попап
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _screen;

        private readonly ProjectPresentersFactory _projectPresentersFactory;

        private readonly MainMenuPopupService _popupService;

        private readonly List<IPresenter> _childPresenters = new();

        public MainMenuScreenPresenter(
            MainMenuScreenView screen,
            ProjectPresentersFactory projectPresentersFactory,
            MainMenuPopupService popupService)
        {
            _screen = screen;
            _projectPresentersFactory = projectPresentersFactory;
            _popupService = popupService;
        }

        public void Initialize()
        {
            // _screen.OpenLevelsMenuButtonClicked += OnOpenLevelsMenuButtonClicked;
            _screen.OpenTestPopupButtonClicked += OnOpenTestPopupButtonClicked;

            CreateWallet();

            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            //_screen.OpenLevelsMenuButtonClicked -= OnOpenLevelsMenuButtonClicked;
            _screen.OpenTestPopupButtonClicked -= OnOpenTestPopupButtonClicked;

            foreach (IPresenter presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }

        private void CreateWallet()
        {
            WalletPresenter walletPresenter = _projectPresentersFactory.CreateWalletPresenter(_screen.WalletView);

            _childPresenters.Add(walletPresenter);
        }

        /*        private void OnOpenLevelsMenuButtonClicked()
                {
                    //_popupService.OpenLevelsMenuPopup();
                }*/

        private void OnOpenTestPopupButtonClicked()
        {
            _popupService.OpenTestPopup();
        }
    }
}
