using System;

namespace Assets._Project.Develop.Runtime.Infrastructure.DI
{
    //L1 - Первая версия DI container
    //L2 - Добавляем NonLazy регистрации
    public class Registration : IRegistrationOptions
    {
        private Func<DIContainer, object> _creator;
        private object _cachedInstance;

        public bool IsNonLazy { get; private set; }


        public Registration(Func<DIContainer, object> creator) => _creator = creator;

        public object CreateInstanceFrom(DIContainer container)
        {
            if (_cachedInstance != null) 
                return _cachedInstance;

            if (_creator == null)
                throw new InvalidOperationException("Not has instance or creator");

            //создаем зависимость
            _cachedInstance = _creator.Invoke(container);

            return _cachedInstance;
        }

        public void NonLazy() => IsNonLazy = true;
    }
}
