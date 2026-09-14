using Assets._Project.Develop.Runtime.UI.Core;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    //L3 - Регистрируем сервис попапов для главного меню
    public class MainMenuPopupService : PopupService
    {
        private readonly MainMenuUIRoot _uiRoot;

        public MainMenuPopupService(
            ViewsFactory viewsFactory,
            ProjectPresentersFactory presentersFactory,
            MainMenuUIRoot uiRoot)
            : base(viewsFactory, presentersFactory)
        {
            _uiRoot = uiRoot;
        }

        protected override Transform PopupLayer => _uiRoot.PopupsLayer;
    }
}
