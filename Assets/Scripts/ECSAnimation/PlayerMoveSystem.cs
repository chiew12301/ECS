using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace KC_ECSANIMATION
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct PlayerMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            new PlayerMoveJob
            {
                DeltaTime = deltaTime
            }.Schedule();
        }

        [BurstCompile]
        public partial struct PlayerMoveJob : IJobEntity
        {
            public float DeltaTime;

            [BurstCompile]
            private void Execute(ref LocalTransform transform, in MoveInput moveinput, MoveSpeed moveSpeed)
            {
                transform.Position.xz += moveinput.value * moveSpeed.value * DeltaTime;
                if(math.lengthsq(moveinput.value) > float.Epsilon)
                {
                    var forward = new float3(moveinput.value.x, 0.0f, moveinput.value.y);
                    transform.Rotation = quaternion.LookRotation(forward, math.up());
                }
            }
        }
    }
}