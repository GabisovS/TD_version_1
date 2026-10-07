using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Attack
{
    //L5 - В чем заключается суть механики атаки
    //L5 - Процесс атаки, заводим необходимые данные
    //L5 - Добавляем данные для задержки перед выстрелом
    //L5 - Добавляем данные для реализации выстрела
    //L5 - Механика отмены атаки. Данные
    //L5 - Механика кулдауна
    public class StartAttackRequest : IEntityComponent
    {
        public ReactiveEvent Value; //Запрос на атаку
    }

    public class StartAttackEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class CanStartAttack : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class EndAttackEvent : IEntityComponent
    {
        public ReactiveEvent Value; //Событие по окончанию атаки
    }

    public class AttackProcessInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class AttackProcessCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class InAttackProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }

    public class AttackDelayTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class AttackDelayEndEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class InstantAttackDamage : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class ShootPoint : IEntityComponent
    {
        public Transform Value;
    }

    public class MustCancelAttack : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class AttackCanceledEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class AttackCooldownInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class AttackCooldownCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class InAttackCooldown : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
}
