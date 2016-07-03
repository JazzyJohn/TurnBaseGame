using UnityEngine;
using System.Collections;
using PawnLogic;
namespace AI
{
    public class PawnAI : BaseAI
    {
        Pawn owner;
        NavigationService navigationService;
        ActionService actionService;
        public void Init(Pawn owner)
        {
            this.owner = owner;
            navigationService = GetComponent<NavigationService>();
            navigationService.Init(owner);
            actionService = GetComponent<ActionService>();
            actionService.Init(owner);
        }

        public override bool CanMove()
        {
            return actionService.CanMove();
        }
        public override void MoveTo(Grid.Cell selectedCell)
        {
            navigationService.StartPath(selectedCell);
        }

        public void StartMoveOnPath()
        {
            actionService.StartMoveOnPath();
        }
        public void NewTurn()
        {
            actionService.NewTurn();
        }

        public void EndTurn()
        {
           
        }
    }
}