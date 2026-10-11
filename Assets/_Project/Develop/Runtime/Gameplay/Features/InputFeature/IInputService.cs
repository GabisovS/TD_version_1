using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature
{
    //L6 - Реализуем сервис ввода пользователя
    public interface IInputService
    {
        bool IsEnabled { get; set; }

        Vector3 Direction { get; }
    }
}
