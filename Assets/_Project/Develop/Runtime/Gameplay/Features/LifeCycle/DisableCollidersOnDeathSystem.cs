using System;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.LifeCycle
{
    //L5 - Система отключения коллайдеров при смерти
    public class DisableCollidersOnDeathSystem : IInitializableSystem, IDisposableSystem
    {
        private List<Collider> _colliders;
        private ReactiveVariable<bool> _isDead;

        private IDisposable _isDeadChangedDisposable;

        public void OnInit(Entity entity)
        {
            _colliders = entity.DisableCollidersOnDeath;
            _isDead = entity.IsDead;

            _isDeadChangedDisposable = _isDead.Subscribe(OnIsDeadChanged);
        }

        private void OnIsDeadChanged(bool arg1, bool isDead)
        {
            if (isDead)
                foreach (Collider collider in _colliders)
                    collider.enabled = false;
        }

        public void OnDispose()
        {
            _isDeadChangedDisposable.Dispose();
        }
    }
}
