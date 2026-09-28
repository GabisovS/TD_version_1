using System;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    //L4 - Организуем Entity
    //L4 - Организуем подключение систем к Entity
    //L4 - Реализуем методы жизненного цикла Entity
    //L4 - Решаем проблему засорения класса Entity
    public partial class Entity : IDisposable
    {
        private readonly Dictionary<Type, IEntityComponent> _components = new();

        private readonly List<IEntitySystem> _systems = new();

        private readonly List<IInitializableSystem> _initializables = new();
        private readonly List<IUpdatableSystem> _updatables = new();
        private readonly List<IDisposableSystem> _disposables = new();

        private bool _isInit;

        //Методы для организации жизненного цикла системы
        public void Initialize()
        {
            foreach (IInitializableSystem initializable in _initializables)
                initializable.OnInit(this);

            _isInit = true;
        }

        //Методы для организации жизненного цикла системы
        public void OnUpdate(float deltaTime)
        {
            if (_isInit == false)
                return;

            foreach (IUpdatableSystem updatable in _updatables)
                updatable.OnUpdate(deltaTime);
        }

        //Методы для организации жизненного цикла системы
        public void Dispose()
        {
            foreach (IDisposableSystem disposable in _disposables)
                disposable.OnDispose();

            _isInit = false;
        }

        //Добавлеяем новые компоненты
        //L4 - Парочка красивостей
        public Entity AddComponent<TComponent>(TComponent component) where TComponent : class, IEntityComponent
        {
            _components.Add(typeof(TComponent), component);
            return this; //чтобы можно было вызывать компонент несколько раз
        }

        //Метод для проверки наличия какого-либо компонента на объекте
        public bool HasComponent<TComponent>() where TComponent : class, IEntityComponent
        {
            return _components.ContainsKey(typeof(TComponent));
        }

        //Метод для попытки получения компонента по типу, если не уверены что такой компонент есть
        public bool TryGetComponent<TComponent>(out TComponent component) where TComponent : class, IEntityComponent
        {
            if (_components.TryGetValue(typeof(TComponent), out IEntityComponent findedObject))
            {
                component = (TComponent)findedObject;
                return true;
            }

            component = null;
            return false;
        }

        //Метод для взятия компонента по типу, если уверены что такой компонент есть
        public TComponent GetComponent<TComponent>() where TComponent : class, IEntityComponent
        {
            if (TryGetComponent(out TComponent component) == false)
                throw new ArgumentException($"Entity not exist {typeof(TComponent)}");

            return component;
        }

        //Логика добавления системы
        public Entity AddSystem(IEntitySystem system)
        {
            //Проверка: есть ли уже в списке система 
            if (_systems.Contains(system))
                throw new ArgumentException(system.GetType().ToString());

            _systems.Add(system);

            //Проверяем какая это система и добавляем ее в соответствующим список
            if (system is IInitializableSystem initializable)
            {
                _initializables.Add(initializable);

               //Если сущность уже проинициализирована, то сразу вызывается OnInit/
               //Это доп проверка, если добавляем систему к сущности уже после инициализации самой сущности
                if (_isInit)
                    initializable.OnInit(this);
            }
           
            //Проверяем какая это система и добавляем ее в соответствующим список
            if (system is IUpdatableSystem updatable)
                _updatables.Add(updatable);
            
            //Проверяем какая это система и добавляем ее в соответствующим список
            if (system is IDisposableSystem disposable)
                _disposables.Add(disposable);

            return this;
        }
    }
}
