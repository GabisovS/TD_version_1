using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.LifeCycle
{
    //L5 - Фича здоровья и смерти. Данные
    //L5 - Обработка смерти – продолжительный процесс
    //L5 - Продолжительная обработка смерти. Данные
    //L5 - Дорабатываем остальные системы.Новые условия
    public class CurrentHealth : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class MaxHealth : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class IsDead : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }

    public class MustDie : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class MustSelfRelease : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class DeathProcessInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class DeathProcessCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    //Флаг: находится сущность в процесси смерти или нет
    public class InDeathProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }

    //public class DisableCollidersOnDeath : IEntityComponent
    //{
    //    public List<Collider> Value;
    //}
}
