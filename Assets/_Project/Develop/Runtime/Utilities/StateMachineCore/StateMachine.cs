using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Runtime.Utilities.Conditions;

namespace Assets._Project.Develop.Runtime.Utilities.StateMachineCore
{
    //L5 - Стейт машина. Внутреннее устройство
    //L5 - Как конфигурировать стейт машину
    //L5 - Как будут выполняться переходы
    //L5 - Анализ возможности перехода
    //L5 - Начинаем делать ИИ для призрака
    //L5 - Делаем стейт машину для AI
    //public abstract class StateMachine<TState> : State, IDisposable, IUpdatableState where TState : class, IState
    public abstract class StateMachine<TState> : State, IDisposable  where TState : class, IState

    {
        private List<StateNode<TState>> _states = new();

        private StateNode<TState> _currentState; //текущее активное состояние

        private bool _isRunning; //включать-выключать стайт машину

        private List<IDisposable> _disposables;     //L5 - Фиксим отписки

        protected StateMachine(List<IDisposable> disposables)     //L5 - Фиксим отписки
        {
            _disposables = new List<IDisposable>(disposables);
        }

        protected TState CurrentState => _currentState.State; //св-во на чтение текущего состояния

        public void AddState(TState state) => _states.Add(new StateNode<TState>(state));

        public void AddTransition(TState fromState, TState toState, ICondition condition)
        {
            StateNode<TState> from = _states.First(stateNode => stateNode.State == fromState);
            StateNode<TState> to = _states.First(stateNode => stateNode.State == toState);

            from.AddTransition(new StateTransition<TState>(to, condition));
        }

        public void Update(float deltaTime)
        {
            if (_isRunning == false)
                return;

            foreach (StateTransition<TState> transition in _currentState.Transitions)
            {
                if (transition.Condition.Evaluate())
                {
                    SwitchState(transition.ToState);
                    break;
                }
            }

            UpdateLogic(deltaTime);
        }

        protected virtual void UpdateLogic(float deltaTime) { }

        public void Dispose()
        {
            _isRunning = false;

            foreach (StateNode<TState> stateNode in _states)
                if (stateNode.State is IDisposable disposableState)
                    disposableState.Dispose();

            _states.Clear();

            //L5 - Фиксим отписки
            foreach (IDisposable disposable in _disposables)
                disposable.Dispose();

            _disposables.Clear();
        }

        public override void Enter()
        {
            base.Enter();

            if (_currentState == null)
                SwitchState(_states[0]);
            else
                _currentState.State.Enter();

                _isRunning = true;
        }

        public override void Exit()
        {
            base.Exit();

            _currentState?.State.Exit();

            _isRunning = false;
        }

        private void SwitchState(StateNode<TState> nextState)
        {
            // вышли из текцущего состояния
            _currentState?.State.Exit(); //? - проверка на null, состояние должно быть
            //приравняли к новому состоянию
            _currentState = nextState; 
            //зашли в новое состояние
            _currentState.State.Enter();
        }
    }
}
