using System;

namespace Assets._Project.Develop.Runtime.Utilities.Conditions
{
    //L5 - Какие остались неприятности
    //L5 - Базовая единица условий
    public class FuncCondition : ICondition
    {
        private Func<bool> _condition;

        public FuncCondition(Func<bool> condition)
        {
            _condition = condition;
        }

        public bool Evaluate() => _condition.Invoke();
    }
}
