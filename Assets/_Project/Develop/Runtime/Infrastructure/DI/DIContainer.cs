using System;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;

namespace Assets._Project.Develop.Runtime.Infrastructure.DI
{

    //L1 - Первая версия DI container
    //L1 - решаем проблему цикличесих зависимостей
    //L1 - Реализуем систему родительских контейнеров
    public class DIContainer
    {
        private readonly Dictionary<Type, Registration> _container = new();

        private readonly List<Type> _requests = new();

        private readonly DIContainer _parent;

        public DIContainer() : this(null)
        { 
        }

        public DIContainer(DIContainer parent) => _parent= parent;

        //Регистрация в единственном экземпляре
        //L2 - Добавляем NonLazy регистрации
        public IRegistrationOptions RegisterAsSingle<T>(Func<DIContainer, T> creator)
        {
            if (IsAlreadyRegister<T>())
                throw new InvalidOperationException($"{typeof(T)} already register");

            //регистрация способа создания сервиса в виде делегата и типа по которому будет запрашиваться сервис
            Registration registration = new Registration(container => creator.Invoke(container));
            _container.Add(typeof(T), registration);

            return registration;
        }

        //Метод для проверки уже регистрированных типов
        //L1 - Реализуем систему родительских контейнеров
        public bool IsAlreadyRegister<T>()
        {
            if (_container.ContainsKey(typeof(T)))
                return true;

            if (_parent != null)
                return _parent.IsAlreadyRegister<T>();

            return false;   
        }

        //Метод получения зависимостей
        public T Resolve<T>()
        {
            if(_requests.Contains(typeof(T))) //проверка на количество запросов одного и того же типа
                throw new InvalidOperationException($"Cycle resolve for {typeof(T)}");

            _requests.Add(typeof(T)); // добавление запроса

            try
            {
                if (_container.TryGetValue(typeof(T), out Registration registration))
                    return (T)registration.CreateInstanceFrom(this); //регистрируем сервис

                //Запрашиваем регистрацию у родительского контейнера
                if (_parent != null)
                    return _parent.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T)); //удаление запроса
            }
            throw new InvalidOperationException($"Registration for {typeof(T)} not exist");
        }

        //L2 - Добавляем NonLazy регистрации
        public void Initialize()
        {
            foreach (Registration registration in _container.Values)
            {
                if (registration.IsNonLazy)
                    registration.CreateInstanceFrom(this);
            }
        }

    }
}
