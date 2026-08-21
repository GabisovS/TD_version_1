namespace Assets._Project.Develop.Runtime.Utilities.LoadingScreen
{
    //L1 - Внедряем загрузрчный экран
    public interface ILoadingScreen
    {
        bool IsShown { get; }
        void Show();
        void Hide();
    }
}
