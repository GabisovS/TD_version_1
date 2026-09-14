using System;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    //L3 - Организация главного экрана
    //L3 - Возвращаем отображение кошелька на главный экран
    //L3 - Открываем тестовый попап
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        //public event Action OpenLevelsMenuButtonClicked;
        public event Action OpenTestPopupButtonClicked;

        [field: SerializeField] public IconTextListView WalletView { get; private set; }
        [SerializeField] private Button _openTestPopupButton;
        //  [SerializeField] private Button _openLevelsMenuButton;

        private void OnEnable()
        {
            //_openLevelsMenuButton.onClick.AddListener(OnOpenLevelsMenuButtonClicked);
            _openTestPopupButton.onClick.AddListener(OnOpenTestPopupButtonClicked);
        }

        private void OnDisable()
        {
            // _openLevelsMenuButton.onClick.RemoveListener(OnOpenLevelsMenuButtonClicked);
            _openTestPopupButton.onClick.RemoveListener(OnOpenTestPopupButtonClicked);
        }

       // private void OnOpenLevelsMenuButtonClicked() => OpenLevelsMenuButtonClicked?.Invoke();
        private void OnOpenTestPopupButtonClicked() => OpenTestPopupButtonClicked?.Invoke();
    }
}
