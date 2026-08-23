using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructure
{
    //L1 - Организуем точки входа и передачу контейнера
    //L1 - Передача доп параметров на сцену
    //L1 - Поддержка глобального контейнера и контейнера сцены
    public abstract class SceneBootstrap : MonoBehaviour
    {
        // выделяем отдельно этап регистрации зависимостей
        // Метод будет вызыватьс самым первым, поэтому паремтры передаются сюда
        public abstract void ProcessRigstrations(DIContainer container, IInputSceneArgs sceneArgs = null);

        //метод с подготовкой к тому чтобы стартануть работу и передачи контейнера
        public abstract IEnumerator Initialize();

        //метод где запускается работа сцены
        public abstract void Run();

    }
}
