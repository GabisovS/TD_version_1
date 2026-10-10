using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI
{
    //L5 - Делаем стейт машину для AI
    //L5 - Фиксим отписки
    public class AIStateMachine : StateMachine<IUpdatableState>
    {
        public AIStateMachine(List<IDisposable> disposables) : base(disposables)
        {
        }

        public AIStateMachine() : base(new List<IDisposable>())
        {
        }

        protected override void UpdateLogic(float deltaTime)
        {
            base.UpdateLogic(deltaTime);

            CurrentState?.Update(deltaTime);
        }
    }
}
