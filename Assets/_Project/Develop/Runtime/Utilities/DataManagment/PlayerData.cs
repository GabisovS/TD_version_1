using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment
{
    //L2 - Из чего состоит задача сохранения данных
    //L2 - Как разбивать данные
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData; 
    }
}
