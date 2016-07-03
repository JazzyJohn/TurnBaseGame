using UnityEngine;
using System.Collections;
using PawnLogic;

namespace AI
{
    public class ActionService : MonoBehaviour
    {
        Pawn owner;
        public int startActionPoints = 2;
        int actionPoints;
        const int MOVE_COST = 1;
        public void Init(Pawn owner)
        {
            this.owner = owner;
            actionPoints = startActionPoints;
        }

        public bool CanMove()
        {
            return actionPoints > 0;
        }


        public void StartMoveOnPath()
        {
            actionPoints-= MOVE_COST ;
        }

        public void NewTurn()
        {
            actionPoints = startActionPoints;
        }
    }
}