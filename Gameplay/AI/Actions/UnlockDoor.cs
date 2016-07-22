using UnityEngine;
using System.Collections;
using Interaction;
using Descriptor;

namespace AI
{
    public class UnlockDoor : AnimatedAction
    {

        protected override bool DoAction(Context context)
        {
            context.pawnAI.GetOwner().PlayUnlockDoor();
            return false;
        }

        public override bool CheckTarget(ActorDescriptor actorDescr)
        {
            Door interactableObject = actorDescr.GetComponent<Door>();
            return actorDescr.IsInteractable && interactableObject.locked;
        }

        public override bool FinishAction(AIEvent aIEvent, PawnAI pawnAI)
        {
            ActionData data = pawnAI.CreateOrAquireData<ActionData>(maker);
            if(data == null)
            {
                Debug.LogError("No data for UnlockDoor:" + this);
                return true;
            }

            Door interactableObject = data.context.go.GetComponent<Door>();

            float dice = Random.Range(0, 100);
            Debug.Log("Trying to open Door:" + dice);
            interactableObject.TryUnlock(dice, data.context.pawnAI);
            return true;
            
        }
    }

}