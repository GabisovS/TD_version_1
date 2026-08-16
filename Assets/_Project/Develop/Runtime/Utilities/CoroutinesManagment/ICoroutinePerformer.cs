using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment
{
    public interface ICoroutinePerformer
    {
        //для защиты сервиса корутин от ручного удаления

        Coroutine StartPerform(IEnumerator coroutineFunction);
        void StopPerform(Coroutine coroutine);
    }
}