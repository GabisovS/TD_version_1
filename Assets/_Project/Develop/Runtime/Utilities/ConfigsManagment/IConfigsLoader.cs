using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.ConfigsManagment
{

    //L1 - Откуда брать конфиги
    public interface IConfigsLoader
    {
        IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded);
    }
}