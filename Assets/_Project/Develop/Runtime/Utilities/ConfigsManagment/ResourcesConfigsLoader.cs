using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//L1 - Откуда брать конфиги
public class ResourcesConfigsLoader : IConfigsLoader
{
    private readonly ResourcesAssetsLoader _resources;

    //словарь который хранит тип и путь конфига
    private readonly Dictionary<Type, string> _configsResourcesPaths = new()
    {

    };

    public ResourcesConfigsLoader(ResourcesAssetsLoader resources)
    {
        _resources = resources;
    }

    public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded)
    {
        Dictionary<Type, object> loadedConfigs = new();

        foreach (KeyValuePair<Type, string> configResourcesPath in _configsResourcesPaths)
        {
            ScriptableObject config = _resources.Load<ScriptableObject>(configResourcesPath.Value);
            loadedConfigs.Add(configResourcesPath.Key, config);
            yield return null;
        }

        onConfigsLoaded?.Invoke(loadedConfigs);
    }
}
