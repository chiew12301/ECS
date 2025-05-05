using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KC_ECSANIMATION
{
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial
        class GetPlayerInputSystem : SystemBase
    {
        private PlayerInput m_playerMovementAction;
        private Entity m_playerEntity;

        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
            RequireForUpdate<MoveInput>();

            m_playerMovementAction = new PlayerInput();
        }


        protected override void OnStartRunning()
        {
            m_playerMovementAction.Enable();
            m_playerMovementAction.PlayerMap.PlayerJump.performed += OnPlayerShoot;

            m_playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        }

        protected override void OnUpdate()
        {
            var curMoveInput = m_playerMovementAction.PlayerMap.PlayerMovement.ReadValue<Vector2>();

            SystemAPI.SetSingleton(new MoveInput { value = curMoveInput });
        }

        protected override void OnStopRunning()
        {
            m_playerMovementAction?.Disable();
            m_playerEntity = Entity.Null;
        }

        private void OnPlayerShoot(InputAction.CallbackContext context) 
        {
            if (!SystemAPI.Exists(m_playerEntity)) return;

            SystemAPI.SetComponentEnabled<FireProjectileTag>(m_playerEntity, true);
        }
    }
}