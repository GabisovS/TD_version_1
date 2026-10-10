using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI
{
    //L5 - Как будем реализовывать обработку ИИ
    //L5 - Заводим абстракцию для мозгов
    public interface IBrain : IDisposable
    {
        //Абстракция для мозгов, чтобы можно было делать различные реализации как можно мыслить
        void Enable();

        void Disable();

        void Update(float deltaTime);
    }
}
