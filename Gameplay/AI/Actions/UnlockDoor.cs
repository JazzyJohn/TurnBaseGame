using UnityEngine;
using System.Collections;
using Interaction;
using Descriptor;

namespace AI
{
    public class UnlockDoor : BaseAction
    {

        protected override void _StartAction(Context context)
        {
            Door interactableObject = context.go.GetComponent<Door>();

            float dice = Random.Range(0, 100);
            Debug.Log("Trying to open Door:" + dice);
            interactableObject.TryUnlock(dice, context.pawnAI);
        }

        public override bool CheckTarget(ActorDescriptor actorDescr)
        {
            Door interactableObject = actorDescr.GetComponent<Door>();
            return actorDescr.IsInteractable && interactableObject.locked;
        }
        
    }

}