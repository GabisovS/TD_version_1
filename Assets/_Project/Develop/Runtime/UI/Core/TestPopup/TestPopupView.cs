using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Assets._Project.Develop.Runtime.UI.Core.TestPopup
{
    //L3 - Прикручиваем вьюху к тестовому попапу
    //L3 - Поддержка анимаций в дочерних классах
    public class TestPopupView : PopupViewBase
    {
        [SerializeField] private TMP_Text _text;

        public void SetText(string text) => _text.text = text;

        protected override void ModifyShowAnimation(Sequence animation)
        {
            base.ModifyShowAnimation(animation);

            animation
                .Append(_text
                    .DOFade(1, 0.2f)
                    .From(0));
        }
    }
}
