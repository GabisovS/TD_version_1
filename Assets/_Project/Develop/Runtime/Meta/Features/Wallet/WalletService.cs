using System.Collections.Generic;
using System.Linq;
using System;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Meta.Features.Wallet
{
    //L2 - Создаем сервис кошелька
    public class WalletService
    {
        private readonly Dictionary<CurrencyTypes, ReactiveVariable<int>> _currencies;

        public WalletService(Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies)
        {
            //принимаем через конструктор словарь и записываем его в копию для безопасности
            _currencies = new Dictionary<CurrencyTypes, ReactiveVariable<int>>(currencies);
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
    }
}
