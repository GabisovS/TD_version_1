using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage
{
    //L5 - Фича нанесения урона касанием. Данные
    public class BodyContactDamage : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
}
