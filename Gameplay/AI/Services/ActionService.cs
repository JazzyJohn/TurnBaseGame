using UnityEngine;
using System.Collections;
using PawnLogic;

namespace AI
{
    public class ActionService : MonoBehaviour
    {
        Pawn owner;
        public int startActionPoints = 2;
        public int startUrgentActionPoint = 1;
        public ActionData actionData;
        public delegate ActionData CreateNewData();
        int actionPoints;
        int urgentActionPoint;
        const int MOVE_COST = 1;
        const int RUN_COST = 2;
        public void Init(Pawn owner)
        {
            this.owner = owner;
            NewTurn();
        }

        public bool CanMove()
        {
            return actionPoints > 0;
        }


        public void StartMoveOnPath(bool isRun)
        {
            if(isRun)
            {
                actionPoints -= RUN_COST;
            }
            else
            {
                actionPoints -= MOVE_COST;
            }
           
        }

        public void NewTurn()
        {
            actionPoints = startActionPoints;
            urgentActionPoint = startUrgentActionPoint;
        }

        public void ClearOfAnyData()
        {
            actionData.waiterForAi = null;
            actionData = null;
        }

        public T CreateOrAquireData<T>(CreateNewData maker) where T: ActionData
        {
            if (actionData == null)
            {
                actionData = maker();
            }
            if( actionData as T == null)
            {
                actionData = maker();
            }
            return actionData as T;
        }

        public void AddActionCallback(BaseAction action)
        {
            if(actionData == null)
            {
                Debug.LogWarning("NO Action Data info inside AddActionCallback");
                return;
            }
            actionData.waiterForAi = action;
        }
        public void ForceFinishAction()
        {
            FinishAction(AIEvent.Invalid);
            actionData = null;         
        }
        void FinishAction(AIEvent aIEvent)
        {
            BaseAction action = actionData.waiterForAi;
            SourceOfAction source = actionData.context.source;  
            actionData.waiterForAi = null;
           
            if (action.FinishAction(aIEvent, owner.GetAI()))
            {
                switch (source)
                {
                    case SourceOfAction.Event:
                        Debug.Log("Event Finished");
                        if (owner.GetAI().GetEventHandler() != null)
                        {
                            owner.GetAI().GetEventHandler().ReactionEnded();
                        }
                        break;
                    case SourceOfAction.UI:
                        //TODO: Unfreeze UI
                        break;
                }
                actionData = null;
            }
        }
        public void SendInfoForActions(AIEvent aIEvent)
        {                   
            if (actionData!= null && actionData.waiterForAi.IsSuitableEvent(aIEvent, owner.GetAI()))
            {
                FinishAction(aIEvent);
            }          
        }


        public bool CouldRun()
        {
            return actionPoints >= RUN_COST;
        }

        public int GetAmountOfUrgentPoints()
        {
            return urgentActionPoint;
        }

        public void ReduceUrgentPoints(int cost)
        {
            urgentActionPoint -= cost;
        }

       
    }
}