using UnityEngine;
using System.Collections;
using PawnLogic;

namespace AI
{
    public class ActionService : MonoBehaviour
    {
        Pawn owner;
        public int startActionPoints = 2;
        public ActionData actionData;
        public delegate ActionData CreateNewData();
        int actionPoints;
        const int MOVE_COST = 1;
        const int RUN_COST = 2;
        public void Init(Pawn owner)
        {
            this.owner = owner;
            actionPoints = startActionPoints;
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
        }

        public void ClearOfAnyData()
        {
            actionData = null;
        }

        public T CreateOrAquireData<T>(CreateNewData maker) where T: ActionData
        {
            if (actionData == null)
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

        public void SendInfoForActions(AIEvent aIEvent)
        {
            if (actionData!= null && actionData.waiterForAi.IsSuitableEvent(aIEvent, owner.GetAI()))
            {
                BaseAction action =   actionData.waiterForAi;
                actionData.waiterForAi = null;
                action.FinishAction(aIEvent, owner.GetAI());
            }
          
        }


        public bool CouldRun()
        {
            return actionPoints >= RUN_COST;
        }
    }
}