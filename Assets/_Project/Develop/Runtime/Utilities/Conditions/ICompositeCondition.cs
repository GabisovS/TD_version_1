namespace Assets._Project.Develop.Runtime.Utilities.Conditions
{
    //L5 - Комбинирование условий
    public interface ICompositeCondition : ICondition
    {
        ICompositeCondition Add(ICondition condition);

        ICompositeCondition Remove(ICondition condition);
    }
}
