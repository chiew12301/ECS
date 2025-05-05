using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace KC_ECSANIMATION
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial struct ResetInputSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (tag, entity) in SystemAPI.Query<FireProjectileTag>().WithEntityAccess())
            {
                ecb.SetComponentEnabled<FireProjectileTag>(entity, false);
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}