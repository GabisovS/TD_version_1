using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    //L4 - Реализуем обновление жизненного цикла сущностей
    //Сервис для реализации жизненного цикла сущности: начинать свою работу, обновления, реализации логики и т.д.
    public class EntitiesLifeContext : IDisposable
    {
        public event Action<Entity> Added; //для получения уведомления что новая сущность начала свою жизнь
        public event Action<Entity> Released;//для получения уведомления что новая сущность закончила свою жизнь

        private readonly List<Entity> _entities = new();
        private readonly List<Entity> _releaseRequests = new(); //список запросов на освобождения от системы

        public void Add(Entity entity)
        {
            _entities.Add(entity);

            entity.Initialize();

           Added?.Invoke(entity);
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < _entities.Count; i++)
                _entities[i].OnUpdate(deltaTime);

            foreach (Entity entity in _releaseRequests)
            {
                _entities.Remove(entity);
                entity.Dispose();
               Released?.Invoke(entity);
            }

            _releaseRequests.Clear();
        }

        //Метод для завершения жизненного цикла системы
        public void Release(Entity entity)
        {
            _releaseRequests.Add(entity);
        }

        public void Dispose()
        {
            foreach (Entity entity in _entities)
                entity.Dispose();

            _entities.Clear();
            _releaseRequests.Clear();
        }
    }
}
