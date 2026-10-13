using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.TeamsFeature
{
    //L7 - Добавляем фичу команд
    public class Team : IEntityComponent
    {
        public ReactiveVariable<Teams> Value;
    }
}
