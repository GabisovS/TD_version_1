using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    //L3 - Готовим базу под визуальную часть
    //L3 - Прикручиваем вьюху к тестовому попапу
    //L3 - Tween и правильное прерывание анимаций
    //L3 - Очередь анимаций. Sequence
    //L3 - Добавление коллбэков в анимацию
    //L3 - Синхронизация с Presenter
    //L3 - Небольшой бонус
    public abstract class PopupViewBase : MonoBehaviour, IShowableView
    {
        public event Action CloseRequest;

        [SerializeField] private CanvasGroup _mainGroup;
        [SerializeField] private Image _anticlicker;
        [SerializeField] private CanvasGroup _body;

        [SerializeField] private PopupAnimationTypes _animationType;

        private float _anticlickerDefaultAlpha;

        private Tween _currentAnimation;

        private void Awake()
        {
            _anticlickerDefaultAlpha = _anticlicker.color.a;
            _mainGroup.alpha = 0;
        }

        public void OnCloseButtonClicked() => CloseRequest?.Invoke();

        public Tween Show()
        {
           KillCurrentAnimation();

            OnPreShow();

            //тут потом появятся анимации
            _mainGroup.alpha = 1;

            Sequence animation = PopupAnimationsCreator
             .CreateShowAnimation(_body, _anticlicker, _animationType, _anticlickerDefaultAlpha);

            ModifyShowAnimation(animation);

            animation.OnComplete(OnPostShow);

            return _currentAnimation = animation.SetUpdate(true).Play();
        }

        public Tween Hide()
        {
           KillCurrentAnimation();

            OnPreHide();

            _mainGroup.alpha = 0;

            Sequence animation = PopupAnimationsCreator
                .CreateHideAnimation(_body, _anticlicker, _animationType, _anticlickerDefaultAlpha);

            ModifyHideAnimation(animation);

            animation.OnComplete(OnPostHide);

            return _currentAnimation = animation.SetUpdate(true).Play();

        }

        //L3 - Поддержка анимаций в дочерних классах
        protected virtual void ModifyShowAnimation(Sequence animation) { }
        protected virtual void ModifyHideAnimation(Sequence animation) { }

        protected virtual void OnPostShow() { }

        protected virtual void OnPreShow() { }

        protected virtual void OnPostHide() { }

        protected virtual void OnPreHide() { }

        private void OnDestroy() => KillCurrentAnimation();

        private void KillCurrentAnimation()
        {
                      if (_currentAnimation != null)
                            _currentAnimation.Kill();
        }


    }
}
