using UnityEngine;
using Unity.Entities;

namespace KC_ECSANIMATION
{
    public class PlayerGameObjectPrefab : IComponentData
    {
        public GameObject value;
    }

    public class PlayerAnimatorReference : ICleanupComponentData
    {
        public Animator value;
    }

    public class PlayerAnimatorAuthoring : MonoBehaviour
    {
        public GameObject playerGameObjectPrefab;

        public class PlayerGameObjectPrefabBaker : Baker<PlayerAnimatorAuthoring>
        {
            public override void Bake(PlayerAnimatorAuthoring auth)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponentObject(entity, new PlayerGameObjectPrefab { value = auth.playerGameObjectPrefab });
            }
        }
    }
}