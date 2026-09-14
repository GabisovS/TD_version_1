namespace Assets._Project.Develop.Runtime.UI.Core
{
    //L3 - Готовим базу под визуальную часть
    public interface IShowableView : IView
    {
        void Hide();
        // Tween Hide();

        void Show();
        // Tween Show();
    }
}
