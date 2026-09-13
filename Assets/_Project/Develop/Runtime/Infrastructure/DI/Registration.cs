using System;
using Unity.VisualScripting;

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

        //L2 - Проблема инициализации и деинициализации сцены
        //L2 - Прокачиваем контейнер
        public void OnInitialize()
        {
            //если сущность создалась и она реализует интрефейс, то при вызыво метода она будет проинициализирована
            if (_cachedInstance != null)
                if (_cachedInstance is IInitializable initializable)
                    initializable.Initialize();
        }

        //L2 - Проблема инициализации и деинициализации сцены
        //L2 - Прокачиваем контейнер
        public void OnDispose()
        {
            if (_cachedInstance != null)
                if (_cachedInstance is IDisposable disposable)
                    disposable.Dispose();
        }

        public void NonLazy() => IsNonLazy = true;
    }
}
