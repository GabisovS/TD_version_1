using System;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders
{
    //L2- Задача сброса данных до дефолтного состояния
    //L2 - Откуда брать стартовое состояние данных
    public class PlayerDataProvider : DataProvider<PlayerData> //ограничиваем тип данных
    {
        private readonly ConfigsProviderService _configsProviderService;

        public PlayerDataProvider(
            ISaveLoadService saveLoadSerivce,
            ConfigsProviderService configsProviderService) : base(saveLoadSerivce)
        {
            _configsProviderService = configsProviderService;
        }


        protected override PlayerData GetOriginData()
        {
            return new PlayerData()
            {
                //Инициализируем все поля у PlaeyrData
                WalletData = InitWalletData(),
            };
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Dictionary<CurrencyTypes, int> walletData = new();

            StartWalletConfig walletConfig = _configsProviderService.GetConfig<StartWalletConfig>();

             foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                 walletData[currencyType] = walletConfig.GetValueFor(currencyType);

            return walletData;
        }
    }
}
