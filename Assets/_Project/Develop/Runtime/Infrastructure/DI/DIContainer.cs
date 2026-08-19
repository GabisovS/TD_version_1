using System;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;

namespace Assets._Project.Develop.Runtime.Infrastructure.DI
{
    //L1 - Первая версия DI container
    //L1 - решаем проблему цикличесих зависимостей
    public class DIContainer
    {
        private readonly Dictionary<Type, Registration> _container = new();

        private readonly List<Type> _requests = new();

        //Регистрация в единственном экземпляре
        public void RegisterAsSingle<T>(Func<DIContainer, T> creator)
        {
            //регистрация способа создания сервиса в виде делегата и типа по которому будет запрашиваться сервис
            Registration registration = new Registration(container => creator.Invoke(container));
            _container.Add(typeof(T), registration);

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
            }
            finally
            {
                _requests.Remove(typeof(T)); //удаление запроса
            }
            throw new InvalidOperationException($"Registration for {typeof(T)} not exist");
        }

    }
}
