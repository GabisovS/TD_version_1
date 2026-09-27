using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using UnityEngine;

//L4 - Доделываем механику движения с использованием rigidbody
namespace Assets._Project.Develop.Runtime.Gameplay.Common
{
    public class RigidbodyComponent : IEntityComponent
    {
        public Rigidbody Value;
    }
}
