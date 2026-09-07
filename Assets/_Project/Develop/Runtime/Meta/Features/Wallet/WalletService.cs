using System.Collections.Generic;
using System.Linq;
using System;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System.Data;
using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;


namespace Assets._Project.Develop.Runtime.Meta.Features.Wallet
{
    //L2 - Создаем сервис кошелька
    //L2 - Регистрируем сервис кошелька
    public class WalletService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly Dictionary<CurrencyTypes, ReactiveVariable<int>> _currencies;

        public WalletService(
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies,
            PlayerDataProvider playerDataProvider)
        {
            //принимаем через конструктор словарь и записываем его в копию для безопасности
            _currencies = new Dictionary<CurrencyTypes, ReactiveVariable<int>>(currencies);
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public List<CurrencyTypes> AvailableCurrencies => _currencies.Keys.ToList();

        // Метод по обращению к кошельку на получение конкретной валюты по ее типу
        // чтобы посмотреть или подписаться на ее значение
        // возвращаем валюту под интерфейсом IReadOnlyVariable чтобы нельзя было изменить валюту в обход методов кошелька
        public IReadOnlyVariable<int> GetCurrency(CurrencyTypes type) => _currencies[type];
        
        public bool Enough(CurrencyTypes type, int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            return _currencies[type].Value >= amount;
        }

        public void Add(CurrencyTypes type, int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _currencies[type].Value += amount;
        }

        public void Spend(CurrencyTypes type, int amount)
        {
            if (Enough(type, amount) == false)
                throw new InvalidOperationException("Not enough: " + type.ToString());
            
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _currencies[type].Value -= amount;
        }

        public void ReadFrom(PlayerData data)
        {
            foreach (KeyValuePair<CurrencyTypes, int> currency in data.WalletData)
            {
                if (_currencies.ContainsKey(currency.Key))
                    _currencies[currency.Key].Value = currency.Value;
                else
                    _currencies.Add(currency.Key, new ReactiveVariable<int>(currency.Value));
            }
        }

        public void WriteTo(PlayerData data)
        {
            foreach (KeyValuePair<CurrencyTypes, ReactiveVariable<int>> currency in _currencies)
            {
                if (data.WalletData.ContainsKey(currency.Key))
                    data.WalletData[currency.Key] = currency.Value.Value;
                else
                    data.WalletData.Add(currency.Key, currency.Value.Value);
            }
        }
    }
}
