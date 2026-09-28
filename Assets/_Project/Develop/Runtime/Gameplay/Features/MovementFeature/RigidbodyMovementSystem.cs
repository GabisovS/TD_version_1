using Assets._Project.Develop.Runtime.Gameplay.Common;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    //L4 - Реализуем первую систему
    //L4 - Небольшие особенности нашей реализации
    //L4 - Доделываем механику движения с использованием rigidbody
    //L4 - Пользуемся сгенерированным кодом
    public class RigidbodyMovementSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<float> _moveSpeed;
        private Rigidbody _rigidbody;


        public void OnInit(Entity entity)
        {
            _moveDirection = entity.MoveDirection;
            _moveSpeed = entity.MoveSpeed;
            _rigidbody = entity.Rigidbody;

            //_moveDirection = entity.GetComponent<MoveDirection>().Value;
            //_moveSpeed = entity.GetComponent<MoveSpeed>().Value;
            //_rigidbody = entity.GetComponent<RigidbodyComponent>().Value;
        }

        public void OnUpdate(float deltaTime)
        {
            Vector3 velocity = _moveDirection.Value.normalized * _moveSpeed.Value;
            //Debug.Log("Скорость: " + velocity.ToString());
            _rigidbody.velocity = velocity;
        }

        ////////////////////////////////////////////////////////////////////
        //private Entity _entity;
        //public void OnInit(Entity entity)
        //{
        //    _entity = entity;
        //}

        //public void OnUpdate(float deltaTime)
        //{
        //    ReactiveVariable<Vector3> moveDir = _entity.GetComponent<MoveDirection>().Value;
        //    ReactiveVariable<float> moveSpeed = _entity.GetComponent<MoveSpeed>().Value;
        //    Vector3 velocity = moveDir.Value.normalized * moveSpeed.Value;

        //    Debug.Log("Скорость: " + velocity.ToString());
        //}
    }
}
