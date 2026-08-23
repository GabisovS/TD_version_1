using UnityEngine;
using Assets._Project.Develop.Runtime.Infrastructure.DI;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    //L1 - Поддержка глобального контейнера и контейнера сцены
    public class GameplayContextRegistrations
    {
        //Процесс регистрации сервисов на сцене геймплея
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Процесс регистрации сервисов на сцене геймплея");
        }
    }
}
