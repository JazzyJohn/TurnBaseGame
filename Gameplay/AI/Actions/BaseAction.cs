using UnityEngine;
using System.Collections;
using Descriptor;

namespace AI
{
    public class ActionData
    {
        public ActionData()
        { }

    }
    public class BaseAction : MonoBehaviour
    {
        public virtual void ActionsEnd(PawnAI pawnAI)
        {

        }
        public virtual void StartAction(PawnAI pawnAI, GameObject go)
        {
        }

        public virtual bool CheckTarget(ActorDescriptor actorDescr)
        {
            return false;
        }

    }
}
