using Unity.Mathematics;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace KC_ECSANIMATION
{

    [UpdateInGroup(typeof(PresentationSystemGroup), OrderFirst = true)]
    public partial struct PlayerAnimateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach(var(playerGameObjectPrefab, entity) in SystemAPI.Query<PlayerGameObjectPrefab>().WithEntityAccess())
            {
                var newCompanionGameObject = Object.Instantiate(playerGameObjectPrefab.value);
                var newAnimatorRef = new PlayerAnimatorReference
                {
                    value = newCompanionGameObject.GetComponent<Animator>()
                };
                ecb.AddComponent(entity, newAnimatorRef);
            }

            foreach(var (transform, animatorRef, moveInput) in SystemAPI.Query<LocalTransform, PlayerAnimatorReference, MoveInput>())
            {
                animatorRef.value.SetBool("IsMoving", math.length(moveInput.value) > 0.0f);
                animatorRef.value.transform.position = transform.Position;
                animatorRef.value.transform.rotation = transform.Rotation;
            }

            foreach(var (animatorRef, entity) in SystemAPI.Query<PlayerAnimatorReference>().WithNone<PlayerGameObjectPrefab, LocalTransform>().WithEntityAccess())
            {
                Object.Destroy(animatorRef.value.gameObject);
                ecb.RemoveComponent<PlayerAnimatorReference>(entity);
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}