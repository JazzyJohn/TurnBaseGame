using UnityEngine;
using System.Collections;
using AI;

namespace Interaction
{
    public class Door : InteractableObject
    {
        public bool locked;
        public int chance;
        public Animator animator;
        public void Start()
        {
            animator = GetComponent<Animator>();
        }

        public override void Use()
        {
            if(!locked)
            {
                animator.SetTrigger("toggle");
            }
        }

        public void TryUnlock(float dice, PawnAI opener)
        {
            Debug.Log("Door unlock check :" +( dice < chance));
            if(dice < chance)
            {
                GamePlayEvent gameplayEvent = new GamePlayEvent(gameObject, GamePlayEventType.LockDoorOpen);
                gameplayEvent.victim = opener.gameObject;
                EventHandler.SendEvent(gameplayEvent);
                locked = false;
            }
            else
            {
                GamePlayEvent gameplayEvent = new GamePlayEvent(gameObject, GamePlayEventType.LockDoorTryingToOpen);
                gameplayEvent.victim = opener.gameObject;
                EventHandler.SendEvent(gameplayEvent);
            }
        }
    }
}