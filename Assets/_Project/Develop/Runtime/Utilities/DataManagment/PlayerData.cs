using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment
{
    //L2 - Из чего состоит задача сохранения данных
    public class PlayerData
    {
        public Dictionary<CurrencyTypes, int> WalletData; 
    }
}
