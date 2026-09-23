using System;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    //L3 - Создаем View динамически. Фабрика вьюх
    //L3 - Динамический вариант создания вьюхи
    //L3 - Организация главного экрана
    //L3 - Прикручиваем вьюху к тестовому попапу
    //L3 - Разбираем скрипты слоя View
    public partial class ViewsFactory
    {
        private readonly ResourcesAssetsLoader _resourcesAssetsLoader;

        private readonly Dictionary<string, string> _viewIDToResourcesPath = new Dictionary<string, string>()
        {
            {ViewIDs.CurrencyView, "UI/Wallet/CurrencyView" },
            {ViewIDs.MainMenuScreen, "UI/MainMenu/MainMenuScreenView" },
            {ViewIDs.TestPopup, "UI/TestPopup" },
            {ViewIDs.LevelTile, "UI/LevelsMenuPopup/LevelTile" },
            {ViewIDs.LevelsMenuPopup, "UI/LevelsMenuPopup/LevelsMenuPopup" }
        };

        public ViewsFactory(ResourcesAssetsLoader resourcesAssetsLoader)
        {
            _resourcesAssetsLoader = resourcesAssetsLoader;
        }

        public TView Create<TView>(string viewID, Transform parent = null) where TView : MonoBehaviour, IView
        {
            if (_viewIDToResourcesPath.TryGetValue(viewID, out string resourcePath) == false)
                throw new ArgumentException($"You didn;t set reource path for {typeof(TView)}, searched id: {viewID}");

            GameObject prefap = _resourcesAssetsLoader.Load<GameObject>(resourcePath);
            GameObject instance = Object.Instantiate(prefap, parent);
            TView view = instance.GetComponent<TView>(); //получаем с объекта компонент типа ТВью

            if (view == null)
                throw new InvalidOperationException($"Not found {typeof(TView)} component on view instance");

            return view;
        }


        //Метод для дестроя вьюх
        public void Release<TView>(TView view) where TView : MonoBehaviour, IView
        {
            Object.Destroy(view.gameObject);
        }
    }
}
