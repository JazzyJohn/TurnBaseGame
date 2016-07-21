using UnityEngine;
using System.Collections;
using AI;
namespace Animations
{

    public class AnimationWithEventBehaviour : StateMachineBehaviour
    {
        public bool isStateMachine;

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (isStateMachine)
                SendEventToAI(animator);
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!isStateMachine)
                SendEventToAI(animator);
        }
        void SendEventToAI(Animator animator)
        {
            PawnAI pawnAI = animator.GetComponent<PawnAI>();

            if (pawnAI != null)
            {
               pawnAI.AnimationFinished();
            }
        }
    }
}