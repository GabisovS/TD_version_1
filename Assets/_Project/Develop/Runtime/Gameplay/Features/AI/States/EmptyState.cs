using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    //L5 - Реализуем состояние покоя
    //L5 - Делаем стейт машину для AI
    public class EmptyState : State, IUpdatableState
    {
        public void Update(float deltaTime)
        {
        }
    }
}
