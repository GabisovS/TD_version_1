namespace Assets._Project.Develop.Runtime.UI.Core.TestPopup
{
    //L3 - Делаем презентер тестового попапа
    public class TestPopupPresenter : PopupPresenterBase
    {
        private readonly TestPopupView _view;

        /*            public TestPopupPresenter(TestPopupView view, ICoroutinesPerformer coroutinesPerformer) : base(coroutinesPerformer)
                    {
                        _view = view;
                    }*/

        public TestPopupPresenter(TestPopupView view)
        {
            _view = view;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.SetText("TEST TITLE");
        }

    }
}
