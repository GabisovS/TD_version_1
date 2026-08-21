using System.Collections;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructure.EntryPoint
{
    //L1 - Реализуем точку входа
    public class GameEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Start project, setup properties");
            //Подгружаем первичные настройки
            SetupAppSettings();
                        
            Debug.Log("Process registration services");

            DIContainer container = new DIContainer();
            
            //Процесс регистрации сервисов
            EntryPointRegistrations.Process(container);

            //Запускаем точку входа
            container.Resolve<ICoroutinePerformer>().StartPerform(Initialize(container));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0; //выкл вертикальную синхронизацию
            Application.targetFrameRate = 60;//ограничение ФПС до 60кадров
        }

        //L1 - Точка входа. Инициализация всех сервисов
        //L1 - Внедряем загрузрчный экран
        //L1 - Организуем переход между сценами
        private IEnumerator Initialize(DIContainer container) //точка входа выполнена через коротину
        {
            ILoadingScreen loadingScreen = container.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneSwitcherService = container.Resolve<SceneSwitcherService>();

            loadingScreen.Show();

            Debug.Log("Наичнается инициализация сервисов");

            yield return container.Resolve<ConfigsProviderService>().LoadAsync();//т.к. метод асинхронный надо подождать его выполнение

            //строка для имитации загрузки других сервисов
            yield return new WaitForSeconds(1f);

            Debug.Log("Инициализация сервисов завершается");
           
            loadingScreen.Hide();

            yield return sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }
    }
}
