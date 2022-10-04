using Unity.Entities;

// Authoring MonoBehaviours are regular GameObject components.
// They constitute the inputs for the baking systems which generates ECS data.
class TurretAuthoring1 : UnityEngine.MonoBehaviour
{
    public UnityEngine.GameObject CannonBallPrefab;
    public UnityEngine.Transform CannonBallSpawn;
    class TurretBaker : Baker<TurretAuthoring1>
    {
        public override void Bake(TurretAuthoring1 authoring)
        {
            AddComponent(new Turret
            {
                // By default, each authoring GameObject turns into an Entity.
                // Given a GameObject (or authoring component), GetEntity looks up the resulting Entity.
                CannonBallPrefab = GetEntity(authoring.CannonBallPrefab),
                CannonBallSpawn = GetEntity(authoring.CannonBallSpawn)
            });

            // Enableable components are always initially enabled.
            AddComponent<Shooting>();
        }
    }
}
