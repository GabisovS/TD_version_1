using UnityEngine;
using Assets._Project.Develop.Runtime.Infrastructure.DI;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    //L1 - Поддержка глобального контейнера и контейнера сцены
    public class MainMenuContextRegistrations
    {
        //Процесс регистрации сервисов на сцене главного меню
        public static void Process(DIContainer container)
        {
            Debug.Log("Процесс регистрации сервисов на сцене меню");
        }
    }
}
