using Assets._Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    //L7 -  Добавляем поддержку команд в нанесение урона
    //L7 - Не боимся пользоваться статикой
    public class EntitiesHelper
    {
        //должна быть только логика, без сохранения каких либо данных
        public static bool TryTakeDamageFrom(Entity source, Entity damageable, float damage)
        {
            if (damageable.TryGetTakeDamageRequest(out ReactiveEvent<float> takeDamageRequest) == false)
                return false;

            if (source.TryGetTeam(out ReactiveVariable<Teams> sourceTeam)
                && damageable.TryGetTeam(out ReactiveVariable<Teams> damageableTeam))
            {
                if (sourceTeam.Value == damageableTeam.Value)
                    return false;
            }

            takeDamageRequest.Invoke(damage);
            return true;
        }
    }
}
