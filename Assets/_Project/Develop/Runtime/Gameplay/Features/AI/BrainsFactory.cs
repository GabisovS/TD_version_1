using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI.States;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.Timer;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI
{
    //L5 - Заводим фабрику для создания ИИ
    //L5 - Настраиваем переходы между состояниями
    //L5 - Фиксим отписки
    public class BrainsFactory
    {
        private readonly DIContainer _container;
        private readonly TimerServiceFactory _timerServiceFactory;
        //private readonly AIBrainsContext _brainsContext;
        //private readonly IInputService _inputService;
        //private readonly EntitiesLifeContext _entitiesLifeContext;

        public BrainsFactory(DIContainer container)
        {
            _container = container;
            _timerServiceFactory = _container.Resolve<TimerServiceFactory>();
            //_brainsContext = _container.Resolve<AIBrainsContext>();
            //_inputService = _container.Resolve<IInputService>();
            //_entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
        }

        //public StateMachineBrain CreateMainHeroBrain(Entity entity, ITargetSelector targetSelector)
        //{
        //    AIStateMachine combatState = CreateAutoAttackStateMachine(entity);

        //    PlayerInputMovementState movementState = new PlayerInputMovementState(entity, _inputService);

        //    ReactiveVariable<Entity> currentTarget = entity.CurrentTarget;

        //    ICompositeCondition fromMovementToCombatStateCondition = new CompositeCondition()
        //        .Add(new FuncCondition(() => currentTarget.Value != null))
        //        .Add(new FuncCondition(() => _inputService.Direction == Vector3.zero));

        //    ICompositeCondition fromCombatToMovementStateCondition = new CompositeCondition(LogicOperations.Or)
        //        .Add(new FuncCondition(() => currentTarget.Value == null))
        //        .Add(new FuncCondition(() => _inputService.Direction != Vector3.zero));

        //    AIStateMachine behaviour = new AIStateMachine();

        //    behaviour.AddState(movementState);
        //    behaviour.AddState(combatState);

        //    behaviour.AddTransition(movementState, combatState, fromMovementToCombatStateCondition);
        //    behaviour.AddTransition(combatState, movementState, fromCombatToMovementStateCondition);

        //    FindTargetState findTargetState = new FindTargetState(targetSelector, _entitiesLifeContext, entity);
        //    AIParallelState parallelState = new AIParallelState(findTargetState, behaviour);

        //    AIStateMachine rootStateMachine = new AIStateMachine();
        //    rootStateMachine.AddState(parallelState);

        //    StateMachineBrain brain = new StateMachineBrain(rootStateMachine);
        //    _brainsContext.SetFor(entity, brain);

        //    return brain;
        //}


        //L5 - Делаем реализацию мозгов на основе машины состояний
        public StateMachineBrain CreateGhostBrain(Entity entity)
        {
            AIStateMachine stateMachine = CreateRandomMovementStateMachine(entity);
            StateMachineBrain brain = new StateMachineBrain(stateMachine);

            //_brainsContext.SetFor(entity, brain);

            return brain;
        }

        //L5 - Добиваем стейт машину случайного движения
        //L5 - Делаем реализацию мозгов на основе машины состояний
        private AIStateMachine CreateRandomMovementStateMachine(Entity entity)
        {
            List<IDisposable> disposables = new List<IDisposable>();

            RandomMovementState randomMovementState = new RandomMovementState(entity, 0.5f);

            EmptyState emptyState = new EmptyState();

            TimerService movementTimer = _timerServiceFactory.Create(2f);
            disposables.Add(movementTimer);
            disposables.Add(randomMovementState.Entered.Subscribe(movementTimer.Restart));

            TimerService idleTimer = _timerServiceFactory.Create(3f);
            disposables.Add(idleTimer);
            disposables.Add(emptyState.Entered.Subscribe(idleTimer.Restart));

            FuncCondition movementTimerEndedCondition = new FuncCondition(() => movementTimer.IsOver);
            FuncCondition idleTimerEndedCondition = new FuncCondition(() => idleTimer.IsOver);

            AIStateMachine stateMachine = new AIStateMachine(disposables);
            
            stateMachine.AddState(randomMovementState);
            stateMachine.AddState(emptyState);

            stateMachine.AddTransition(randomMovementState, emptyState, movementTimerEndedCondition);
            stateMachine.AddTransition(emptyState, randomMovementState, idleTimerEndedCondition);

            return stateMachine;
        }

        //private AIStateMachine CreateAutoAttackStateMachine(Entity entity)
        //{
        //    RotateToTargetState rotateToTargetState = new RotateToTargetState(entity);

        //    AttackTriggerState attackTriggerState = new AttackTriggerState(entity);

        //    ICondition canAttack = entity.CanStartAttack;
        //    Transform transform = entity.Transform;
        //    ReactiveVariable<Entity> currentTarget = entity.CurrentTarget;

        //    ICompositeCondition fromRotateToAttackCondition = new CompositeCondition()
        //        .Add(canAttack)
        //        .Add(new FuncCondition(() =>
        //        {
        //            Entity target = currentTarget.Value;

        //            if (target == null)
        //                return false;

        //            float angleToTarget = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(target.Transform.position - transform.position));
        //            return angleToTarget < 1f;
        //        }));

        //    ReactiveVariable<bool> inAttackProcess = entity.InAttackProcess;

        //    ICondition fromAttackToRotateStateCondition = new FuncCondition(() => inAttackProcess.Value == false);

        //    AIStateMachine stateMachine = new AIStateMachine();

        //    stateMachine.AddState(rotateToTargetState);
        //    stateMachine.AddState(attackTriggerState);

        //    stateMachine.AddTransition(rotateToTargetState, attackTriggerState, fromRotateToAttackCondition);
        //    stateMachine.AddTransition(attackTriggerState, rotateToTargetState, fromAttackToRotateStateCondition);

        //    return stateMachine;
        //}
    }
}
