using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.LifeCycle
{
    //L5 - Фича здоровья и смерти. Логика
    public class DeathSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<bool> _isDead;

        private ReactiveVariable<float> _currentHealth;

        //private ICompositeCondition _mustDie;

        public void OnInit(Entity entity)
        {
            _isDead = entity.IsDead;
            //_mustDie = entity.MustDie;

            _currentHealth = entity.CurrentHealth;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value)
                return;

            //if (_mustDie.Evaluate())
            //    _isDead.Value = true;

            if (_currentHealth.Value <= 0)
            {
                _isDead.Value = true;
                Debug.Log("Сущность умерла");
            }
        }
    }
}
