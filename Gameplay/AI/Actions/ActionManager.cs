using UnityEngine;
using System.Collections;
using PawnLogic;
using Descriptor;
namespace AI
{
    public class ActionManager : MonoBehaviour
    {
        static ActionManager s_Instance;
        public BaseAction[] all_actions;
        // Use this for initialization
        void Start()
        {
            s_Instance = this;
        }

        public static void DoAction(int i, GameObject target, Pawn pawn)
        {
            s_Instance._DoAction(i, target, pawn);
        }

        private void _DoAction(int i, GameObject target, Pawn pawn)
        {
            if (target == null || pawn == null)
            {
                Debug.Log("Target Is Null During Actions");
                return;
            }
            ActorDescriptor descrt = target.GetComponent<ActorDescriptor>();
            if (descrt == null)
            {
                Debug.Log("ActorDescriptor Is Null During Actions");
                return;
            }
            if(i >= all_actions.Length)
            {
                Debug.Log(string.Format("No such action with %d index", i));
                return;
            }

            if (all_actions[i].CheckTarget(descrt))
            {
                all_actions[i].StartAction(new Context(pawn.GetAI(), target));
            }

        }

    }
}
