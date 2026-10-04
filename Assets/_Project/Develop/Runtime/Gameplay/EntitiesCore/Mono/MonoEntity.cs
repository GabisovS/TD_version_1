using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono
{
    //L4 - Проблема связи сущностей и Unity
    //L4 - MonoEntity – мостик между Entity и миром Unity
    //L4 - Подвязываем компоненты из Unity
    //L5 - Решаем задачу связи коллайдеров и сущности
    //L5 - Сервис регистрации коллайдеров
    //L5 - Делаем регистрации коллайдеров
    public class MonoEntity : MonoBehaviour
    {
        private CollidersRegistryService _collidersRegistryService;

        private Entity _linkedEntity;

        public Entity LinkedEntity => _linkedEntity;

        public void Initialize(CollidersRegistryService collidersRegistryService)
        {
            _collidersRegistryService = collidersRegistryService;
        }

        public void Link(Entity entity)
        {
            _linkedEntity = entity;

            MonoEntityRegistrator[] registrators = GetComponentsInChildren<MonoEntityRegistrator>();

            if (registrators != null)
                foreach (MonoEntityRegistrator registrator in registrators)
                    registrator.Register(entity);

            foreach (Collider collider in GetComponentsInChildren<Collider>())
                _collidersRegistryService.Register(collider, entity);
        }

        public void Cleanup(Entity entity)
        {
            foreach (Collider collider in GetComponentsInChildren<Collider>())
                _collidersRegistryService.Unregister(collider);

            _linkedEntity = null;
        }
    }
}
