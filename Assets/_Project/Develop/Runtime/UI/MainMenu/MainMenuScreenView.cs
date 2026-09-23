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
    //L3 - Тестируем попап
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action OpenLevelsMenuButtonClicked;

        [field: SerializeField] public IconTextListView WalletView { get; private set; }
         [SerializeField] private Button _openLevelsMenuButton;

        private void OnEnable()
        {
            _openLevelsMenuButton.onClick.AddListener(OnOpenLevelsMenuButtonClicked);
      
        }

        private void OnDisable()
        {
            _openLevelsMenuButton.onClick.RemoveListener(OnOpenLevelsMenuButtonClicked);
 
        }

        private void OnOpenLevelsMenuButtonClicked() => OpenLevelsMenuButtonClicked?.Invoke();

    }
}
