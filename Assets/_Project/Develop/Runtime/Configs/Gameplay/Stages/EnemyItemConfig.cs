using System;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Stages
{
    //L7 - Как будем организовывать волны
    //L7 - Какая информация нам нужна
    [Serializable]
    public class EnemyItemConfig
    {
        //Храниться позиция спавна и конфиг сущности которую нужно заспавнить
        [field: SerializeField] public Vector3 SpawnPosition { get; private set; }
        [field: SerializeField] public EntityConfig EnemyConfig { get; private set; }
    }
}
