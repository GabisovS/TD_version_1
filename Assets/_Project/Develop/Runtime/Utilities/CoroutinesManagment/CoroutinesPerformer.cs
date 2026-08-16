using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment
{
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
}