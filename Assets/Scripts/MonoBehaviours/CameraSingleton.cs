// There are many ways of getting access to the main camera, but the approach using
// a singleton (as we use here) works for any kind of MonoBehaviour.
using System.Numerics;
using Unity.Mathematics;

class CameraSingleton : UnityEngine.MonoBehaviour
{
    public static CameraSingleton Instance;

    public float3 LookAtOffset;
    public float3 Offset;
    void Awake()
    {
        Instance = this;
    }

    public void SetTartget(float3 pos)
    {
        transform.position = pos + Offset;
        transform.LookAt(pos + LookAtOffset);
    }
}