using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructure
{
    //L1 - Организуем точки входа и передачу контейнера
    public abstract class SceneBootstrap : MonoBehaviour
    {
        //метод с подготовкой к тому чтобы стартануть работу и передачи контейнера
        public abstract IEnumerator Initialize(DIContainer container);

        //метод где запускается работа сцены
        public abstract void Run();
    }
}
