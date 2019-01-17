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

        protected override void EnterProcess(StateMachine stateMachine) {
            ScenesController.Instance.ChangeScene(_sceneName);
        }

        protected override void ExitProcess(StateMachine stateMachine) {
        }

        protected override State Process() {
            return null;
        }
    }
}