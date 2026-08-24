using System;

namespace Assets._Project.Develop.Runtime.Utilities.Reactive
{
    //L1 - Прокачиваем реактивность
    //L1 - Добавляем возможность отписаться
    public interface IReadOnlyVariable<T>
    {
        T Value { get; }

        //метод для подписки на изменение варибла
        IDisposable Subscribe(Action<T, T> action);

    }
}
