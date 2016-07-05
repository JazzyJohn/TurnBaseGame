using UnityEngine;
using System.Collections;
using AI;
namespace World
{
    public class ActionTrigger : MonoBehaviour
    {
        public BaseAction action;
        public void OnTriggerEnter(Collider other)
        {
            PawnAI pawnAi = other.GetComponent<PawnAI>();
            if(pawnAi != null)
            {
                action.StartAction(pawnAi,other.gameObject);
            }
        }

    }
}
