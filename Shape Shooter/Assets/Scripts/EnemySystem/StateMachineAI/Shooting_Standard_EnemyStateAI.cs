using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Wokarol.InputSystem;
using Wokarol.StateSystem;

namespace Wokarol.AI.EnemyBrains
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Shooting_Standard_EnemyStateAI : InputData, IResetable , ICanModifyShootingInput
    {
        private const string TargetID = "AI_Target";
        public DebugBlock AIDebugBlock { get; } = new DebugBlock("Shooting AI");
        Vector2 ICanModifyShootingInput.AimDirection { get => AimDirection; set => AimDirection = RealAimDirection = value; }
        bool ICanModifyShootingInput.Shoot { get => Shoot; set => Shoot = value; }

        StateMachine aiMachine;
        Target target = new Target();
        float sqrTargetDistance = 0;
        NavMeshAgent agent;

        [SerializeField] LayerMask targetLayer = 0;
        [SerializeField] float seekingRange = 10;
        [SerializeField] float shootingRange = 5;
        [SerializeField] float startAttackRange = 3;

        private void Start() {
            agent = GetComponent<NavMeshAgent>();
            AIDebugBlock.Define("Target", TargetID);

            var wait = new WaitState("Wait in place");
            var goTowardsTarget = new GoTowardsTargetState("Go towards target", target, agent, false);
            var shootAtTarget = new ShootAtTargetState("Shoot at target", target, transform, this);

            wait.AddTransition(() => target.Transform != null, goTowardsTarget);

            goTowardsTarget.AddTransition(() => sqrTargetDistance < startAttackRange * startAttackRange, shootAtTarget);
            goTowardsTarget.AddTransition(() => sqrTargetDistance > seekingRange * seekingRange, wait);

            shootAtTarget.AddTransition(() => sqrTargetDistance > seekingRange * seekingRange, wait);
            shootAtTarget.AddTransition(() => sqrTargetDistance > shootingRange * shootingRange, goTowardsTarget);

            // Starting machine
            aiMachine = new StateMachine(wait, AIDebugBlock);
        }

        private void Update() {
            TargetCalculation();
            aiMachine.Tick();
        }

        private void TargetCalculation() {
            if (!target.Transform) {
                var collider = Physics2D.OverlapCircle(transform.position, seekingRange, targetLayer);
                if (collider) {
                    target.Transform = collider.transform;
                    AIDebugBlock.Change(TargetID, target.Transform.name);
                }
            }
            if (target.Transform) {
                sqrTargetDistance = Vector2.SqrMagnitude(transform.position - target.Transform.position);
                if (sqrTargetDistance > (seekingRange * seekingRange)) {
                    target.Transform = null;
                    AIDebugBlock.Change(TargetID, "null");
                }
            }
        }

        public void ResetObject() {
            aiMachine?.Restart();
        }

        private void OnDrawGizmosSelected() {
            Gizmos.DrawWireSphere(transform.position, seekingRange);
            if (target.Transform) {
                Gizmos.color = Color.red * Color.grey;
                Gizmos.DrawWireSphere(target.Transform.position, 0.2f);
            }
            if (agent) {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(agent.destination, 0.3f);
            }
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, shootingRange);
            Gizmos.color = Color.yellow * Color.grey;
            Gizmos.DrawWireSphere(transform.position, startAttackRange);
        }
    }
}
