using System;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;

namespace Assets._Project.Develop.Runtime.Gameplay.States
{
    //L7 - Заводим реализацию машины состояния под геймплей
    public class GameplayStateMachine : StateMachine<IUpdatableState>
    {
        public GameplayStateMachine(List<IDisposable> disposables) : base(disposables)
        {
        }

        public GameplayStateMachine() : base(new List<IDisposable>())
        {
        }

        protected override void UpdateLogic(float deltaTime)
        {
            base.UpdateLogic(deltaTime);

            CurrentState?.Update(deltaTime);
        }
    }
}
