using System;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.UI.Wallet
{
    //L2 - Презентер валюты
    public class CurrencyPresenter
    {
        //Бизнес логика/ то что должны отобразить в UI  
        private readonly IReadOnlyVariable<int> _currency;          //ссылка на саму валюту
        private readonly CurrencyTypes _currencyType;               //тип этой валюты
        private readonly CurrencyIconsConfig _currencyIconsConfig;  //конфиг где записана инфа о иконках

        //Визуал. Выступает в качестве отображения
        private readonly IconTextView _view;

        private IDisposable _disposable; //для записи подписок и отписок

        public CurrencyPresenter(
            IReadOnlyVariable<int> currency,
            CurrencyTypes currencyType,
            CurrencyIconsConfig currencyIconsConfig,
            IconTextView view)
        {
            _currency = currency;
            _currencyType = currencyType;
            _currencyIconsConfig = currencyIconsConfig;
            _view = view;
        }

        public IconTextView View => _view;

        public void Initialize() //инициализация презентера: бизнес логика передается в визуал
        {
            UpdateValue(_currency.Value);
            _view.SetIcon(_currencyIconsConfig.GetSpriteFor(_currencyType));

            _disposable = _currency.Subscribe(OnCurrencyChanged); //подписываемся на изменение данных
        }

        public void Dispose() // происходит отписка 
        {
            _disposable.Dispose();
        }

        private void OnCurrencyChanged(int arg1, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}

