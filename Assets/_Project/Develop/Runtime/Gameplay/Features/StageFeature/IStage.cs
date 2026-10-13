using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.StageFeature
{
    //L7 - Определяем то, как должны работать этапы
    public interface IStage : IDisposable
    {
        IReadOnlyEvent Completed { get; }

        void Start();
        void Update(float deltaTime);
        void Cleanup();
    }
}
