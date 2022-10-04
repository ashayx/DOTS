using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

public class CannonBallAuthoring : MonoBehaviour
{
    class CannonBallBaker : Baker<CannonBallAuthoring>
    {
        public override void Bake(CannonBallAuthoring authoring)
        {
            // By default, components are zero-initialized.
            // So in this case, the Speed field in CannonBall will be float3.zero.
            AddComponent<CannonBall>();
            AddComponent<URPMaterialPropertyBaseColor>();

        }
    }
}
