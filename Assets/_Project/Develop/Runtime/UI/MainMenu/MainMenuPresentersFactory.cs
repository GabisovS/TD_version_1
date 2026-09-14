using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Runtime.Infrastructure.DI;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    //L3 - Организация главного экрана
    //L3 - Возвращаем отображение кошелька на главный экран
    //L3 - Открываем тестовый попап
    public class MainMenuPresentersFactory
    {
        private readonly DIContainer _container;

        public MainMenuPresentersFactory(DIContainer container)
        {
            _container = container;
        }

         public MainMenuScreenPresenter CreateMainMenuScreen(MainMenuScreenView view)
        {
            return new MainMenuScreenPresenter(
                view,
                _container.Resolve<ProjectPresentersFactory>(),
                _container.Resolve<MainMenuPopupService>());
        }
    }
}
