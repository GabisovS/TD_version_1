using System.Collections;
using UnityEngine;

public interface ICoroutinePerformer
{
    //для защиты сервиса корутин от ручного удаления

    Coroutine StartPerform(IEnumerator coroutineFunction);
    void StopPerform(Coroutine coroutine);
}
