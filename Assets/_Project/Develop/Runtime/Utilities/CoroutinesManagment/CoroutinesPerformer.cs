using System.Collections;
using UnityEngine;

public class CoroutinesPerformer : MonoBehaviour, ICoroutinePerformer
{
    private void Awake()
    {
        //Чтобы этот сервис не уничтожался при переходе между сценами
        DontDestroyOnLoad(this);
    }

    public Coroutine StartPerform(IEnumerator coroutineFunction)
        => StartCoroutine(coroutineFunction);

    public void StopPerform(Coroutine coroutine)
        => StopCoroutine(coroutine);
}
