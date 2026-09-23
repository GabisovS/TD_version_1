using DG.Tweening;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    //L3 - Готовим базу под визуальную часть
    //L3 - Синхронизация с Presenter
    public interface IShowableView : IView
    {
        Tween Hide();

        Tween Show();
    }
}
