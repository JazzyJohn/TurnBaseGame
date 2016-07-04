using UnityEngine;
using System.Collections;

namespace AI
{
    public class GoToAction : BaseAction
    {

        public override void StartAction(PawnAI pawnAI, GameObject go)
        {
            pawnAI.MoveTo(Grid.GridController.GetCellFromCoord(go.transform.position));
        }

        public override bool CheckTarget(Descriptor.ActorDescriptor actorDsecr)
        {
            return true;
        }
    }
}
