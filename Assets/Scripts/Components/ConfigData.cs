using Unity.Entities;

struct ConfigData : IComponentData
{
    public Entity EnemyPrefab;
    public int EnemyCount;
    public float EnemyDistance;
    public int EnemyRowCount;
    public float Range;
}
