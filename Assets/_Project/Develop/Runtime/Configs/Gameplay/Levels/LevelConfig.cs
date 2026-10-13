using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Stages;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Levels
{
    //L3 - Делаем конфиги уровней
    //L7 - Добиваем конфигурацию уровней
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/NewLevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        //добавляем для уровня то из каких этапов он состоит
        [SerializeField] private List<StageConfig> _stageConfigs;

        public IReadOnlyList<StageConfig> StageConfigs => _stageConfigs;
    }
}
