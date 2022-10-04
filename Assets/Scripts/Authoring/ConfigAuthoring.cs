using Unity.Entities;
using UnityEngine;

class ConfigAuthoring : UnityEngine.MonoBehaviour
{
    public GameObject EnemyPrefab;
    public int EnemyCount;
    public float EnemyDistance;
    public int EnemyRowCount;
    public float Range;
}

class ConfigBaker : Baker<ConfigAuthoring>
{
    public override void Bake(ConfigAuthoring authoring)
    {
        AddComponent(new ConfigData
        {
            EnemyPrefab = GetEntity(authoring.EnemyPrefab),
            EnemyCount = authoring.EnemyCount,
            EnemyDistance = authoring.EnemyDistance,
            EnemyRowCount = authoring.EnemyRowCount,
            Range = authoring.Range,
        });
    }
}