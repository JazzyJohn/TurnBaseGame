using UnityEngine;
using System.Collections;
using Interaction;
using Descriptor;
namespace AI
{
    public class UseObject : BaseAction
    {
        protected override bool DoAction(Context context)
        {

            InteractableObject interactableObject = context.go.GetComponent<InteractableObject>();
           
            if (interactableObject != null)
                interactableObject.Use();

            return true;
        }

        public override bool CheckTarget(ActorDescriptor actorDescr)
        {
            return actorDescr.IsInteractable && base.CheckTarget(actorDescr);
        }
        
    }
}
