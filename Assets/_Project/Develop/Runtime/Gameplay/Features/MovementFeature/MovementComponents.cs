using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    //L4 - Заводим первые компоненты
    //L4 - Про реактивность в этом подходе
    // Реактивность нужна для поддержки событий (подписка и отписка на изменения)
    //L4 - Доделываем механику движения с использованием rigidbody
    //L5 - Добавляем механику поворота. Данные
    //L5 - Внедряем условия для движения и поворота
    public class MoveDirection : IEntityComponent
    {
        public ReactiveVariable<Vector3> Value;
    }

    public class MoveSpeed : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    //public class IsMoving : IEntityComponent
    //{
    //    public ReactiveVariable<bool> Value;
    //}

    public class CanMove : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class RotationDirection : IEntityComponent
    {
        public ReactiveVariable<Vector3> Value;
    }

    public class RotationSpeed : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class CanRotate : IEntityComponent
    {
        public ICompositeCondition Value;
    }
}
