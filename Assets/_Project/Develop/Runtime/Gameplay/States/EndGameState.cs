using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;

namespace Assets._Project.Develop.Runtime.Gameplay.States
{
    //Д7 - Стейты победы и поражения. База
    public abstract class EndGameState : State
    {
        private readonly IInputService _inputService;

        protected EndGameState(IInputService inputService)
        {
            _inputService = inputService;
        }

        public override void Enter()
        {
            base.Enter();

            _inputService.IsEnabled = false; //на входе отключаем сервис ввода 
        }

        public override void Exit()
        {
            base.Exit();

            _inputService.IsEnabled = true; //на выходе включаем сервис ввода 
        }
    }
}
