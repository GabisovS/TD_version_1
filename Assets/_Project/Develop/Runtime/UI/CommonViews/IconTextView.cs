using Assets._Project.Develop.Runtime.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Assets._Project.Develop.Runtime.UI.CommonViews
{
    //L2 - Отображение отдельной валюты с помощью MVP
    //L2 - Создаем View динамически. Фабрика вьюх
    public class IconTextView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _icon;

        public void SetText(string text) => _text.text = text;

        public void SetIcon(Sprite icon) => _icon.sprite = icon;
    }
}
