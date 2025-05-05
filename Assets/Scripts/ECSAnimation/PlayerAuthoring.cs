using Unity.Entities;
using UnityEngine;

namespace KC_ECSANIMATION
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float MoveSpeed;
        public GameObject ProjectilePrefab;
    }

    public class PlayerAuthoringBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var playerEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<PlayerTag>(playerEntity);
            AddComponent<MoveInput>(playerEntity);

            AddComponent<FireProjectileTag>(playerEntity);
            SetComponentEnabled<FireProjectileTag>(playerEntity, false);

            AddComponent(playerEntity, new MoveSpeed
            {
                value = authoring.MoveSpeed
            });
            AddComponent(playerEntity, new ProjectilePrefab
            {
                value = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}