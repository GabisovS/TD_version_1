using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Levels
{
    //L3 - Делаем конфиги уровней
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/NewLevelsListConfig", fileName = "LevelsListConfig")]
    public class LevelsListConfig : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levels;

        public IReadOnlyList<LevelConfig> Levels => _levels;

        public LevelConfig GetBy(int levelNumber)
        {
            int levelIndex = levelNumber - 1;

            return _levels[levelIndex];
        }
    }
}
