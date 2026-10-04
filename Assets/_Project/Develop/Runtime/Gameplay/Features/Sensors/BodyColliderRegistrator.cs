using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Sensors
{
    //L5 - Отслеживание контактов. Прикрепляем данные

    //Для регистрации компонентов сенсора через геймобджект
    public class BodyColliderRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private CapsuleCollider _body;

        public override void Register(Entity entity)
        {
            entity.AddBodyCollider(_body);
        }
    }
}
