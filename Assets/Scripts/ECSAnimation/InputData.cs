using Unity.Entities;
using Unity.Mathematics;

namespace KC_ECSANIMATION
{
    public struct MoveInput : IComponentData
    {
        public float2 value;
    }

    public struct MoveSpeed : IComponentData
    {
        public float value;
    }

    public struct PlayerTag : IComponentData { }
    public struct FireProjectileTag : IComponentData, IEnableableComponent { }
    public struct ProjectilePrefab : IComponentData
    {
        public Entity value;
    }

    public struct ProjectileMoveSpeed : IComponentData
    {
        public float Value;
    }
}