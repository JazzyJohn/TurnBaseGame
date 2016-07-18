using UnityEngine;
using System.Collections;
using Descriptor;
namespace GameFlow
{
    public class FinishZone : MonoBehaviour
    {
        BoxCollider collider;
        public int goalNumber;
        int numberOfAgent;
        void Start()
        {

        }

        void FixedUpdate()
        {
            if(numberOfAgent >= goalNumber)
            {
                GameFlowController.FinishLevel();
            }
            numberOfAgent = 0;
        }
        void OnTriggerStay(Collider other)
        {
            ActorDescriptor descr = other.GetComponent<ActorDescriptor>();
           
            if(descr != null && descr.IsAgent)
            {
                numberOfAgent++;
            }
        }
    }
}
