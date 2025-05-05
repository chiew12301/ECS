using Unity.Entities;
using UnityEngine;

namespace KC_ECSANIMATION
{
    public class ProjectileMoveSpeedAuthoring : MonoBehaviour
    {
        public float ProjectileMoveSpeed;

        public class ProjectileMoveSpeedBaker : Baker<ProjectileMoveSpeedAuthoring>
        {
            public override void Bake(ProjectileMoveSpeedAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new ProjectileMoveSpeed { Value = authoring.ProjectileMoveSpeed });
            }
        }
    }
}