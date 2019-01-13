using Wokarol.StateSystem;

namespace Wokarol.LevelBrains
{
    public class ChangeScene : State
    {
        string _sceneName;

        public ChangeScene(string sceneName) {
            _sceneName = sceneName;
        }

        public override bool CanTransitionToSelf => false;

        public override void Enter(StateMachine stateMachine) {
            ScenesController.Instance.ChangeScene(_sceneName);
        }

        public override void Exit(StateMachine stateMachine) {
        }

        protected override State Process() {
            return null;
        }
    }
}