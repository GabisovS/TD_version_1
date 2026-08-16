using UnityEngine;

public class ResourcesAssetsLoader 
{
 public T Load<T>(string resourcePath) where T : Object
     => Resources.Load<T>(resourcePath);
}
