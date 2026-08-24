using System;

namespace Assets._Project.Develop.Runtime.Utilities.Reactive
{
    //L1 - Прокачиваем реактивность
    //L1 - Добавляем возможность отписаться
    public class Subscriber<T, K> : IDisposable
    {
        private Action<T, K> _action;
        private Action<Subscriber<T, K>> _onDispose; //Для отписки

        public Subscriber(Action<T, K> action, Action<Subscriber<T, K>> onDispose)
        {
            _action = action;
            _onDispose = onDispose;
        }

        public void Dispose() => _onDispose?.Invoke(this);

        public void Invoke(T arg1, K arg2) => _action?.Invoke(arg1, arg2);
    }
}
