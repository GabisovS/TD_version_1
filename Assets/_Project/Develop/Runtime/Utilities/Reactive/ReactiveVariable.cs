
using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.Reactive
{
    //L1 - Прокачиваем реактивность
    //L1 - Фиксим неприятную проблему
    //L6 - Прокачиваем реактивность
    public class ReactiveVariable<T> : IReadOnlyVariable<T> 
    {
        private readonly List<Subscriber<T, T>> _subcribers = new();
        private readonly List<Subscriber<T, T>> _toAdd = new();
        private readonly List<Subscriber<T, T>> _toRemove = new();

        private T _value;
        private IEqualityComparer<T> _comparer;

        public ReactiveVariable() : this(default)
        {
        }

        public ReactiveVariable(T value) : this(value, EqualityComparer<T>.Default)
        {
        }

        public ReactiveVariable(T value, IEqualityComparer<T> comparer)
        {
            _value = value;
            _comparer = comparer;
        }

        //L6 - Тестируем поведение. Проблема текущей реактивности
        //L6 - Как улучшить реактивность. EqualityComparer
        public T Value
        {
            get => _value;
            set
            {
                T oldValue = _value;

                _value = value;

                if (_comparer.Equals(oldValue, value) == false)
                    Invoke(oldValue, value);
            }
        }

        //L1 - Добавляем возможность отписаться
        //L1 - Фиксим неприятную проблему
        public IDisposable Subscribe(Action<T, T> action)
        {
            Subscriber<T, T> subcriber = new Subscriber<T, T>(action, Remove);
            _toAdd.Add(subcriber);

            _subcribers.Add(subcriber);
            return subcriber;   
        }

        //L1 - Добавляем возможность отписаться
        //L1 - Фиксим неприятную проблему
        private void Remove(Subscriber<T, T> subscriber) => _toRemove.Add(subscriber);

        //L1 - Фиксим неприятную проблему
        private void Invoke(T oldValue, T newValue)
        {
            //буфер на проверку обновления списка
            if (_toAdd.Count > 0)
            {
                _subcribers.AddRange(_toAdd);
                _toAdd.Clear();
            }

            //буфер на проверку для удаления
            if (_toRemove.Count > 0)
            {
                foreach (Subscriber<T, T> subcriber in _toRemove)
                    _subcribers.Remove(subcriber);

                _toRemove.Clear();
            }

            foreach (Subscriber<T, T> subscriber in _subcribers)
                subscriber.Invoke(oldValue, newValue);
        }
    }
}
