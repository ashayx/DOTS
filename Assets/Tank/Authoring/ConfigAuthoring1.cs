using Unity.Entities;

class ConfigAuthoring1 : UnityEngine.MonoBehaviour
{
    public UnityEngine.GameObject TankPrefab;
    public int TankCount;
    public float SafeZoneRadius;
    class ConfigBaker1 : Baker<ConfigAuthoring1>
    {
        public override void Bake(ConfigAuthoring1 authoring)
        {
            AddComponent(new Config
            {
                TankPrefab = GetEntity(authoring.TankPrefab),
                TankCount = authoring.TankCount,
                SafeZoneRadius = authoring.SafeZoneRadius
            });
        }
    }
}
