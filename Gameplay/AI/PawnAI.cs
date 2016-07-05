using UnityEngine;
using System.Collections;
using PawnLogic;
namespace AI
{
    public enum AIEvent
    {
        NavigationComplited,
        Invalid
    }

    public class PawnAI : BaseAI
    {
        Pawn owner;
        NavigationService navigationService;
        ActionService actionService;
        ParamsService paramsService;
        public void Init(Pawn owner)
        {
            this.owner = owner;
            navigationService = GetComponent<NavigationService>();
            navigationService.Init(owner);
            actionService = GetComponent<ActionService>();
            actionService.Init(owner);
            paramsService = GetComponent<ParamsService>();
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

        public Pawn GetOwner()
        {
            return owner;
        }

        public void ClearOfAnyData()
        {
            actionService.ClearOfAnyData();
        }

        public T CreateOrAquireData<T>(ActionService.CreateNewData maker) where T : ActionData
        {
            return actionService.CreateOrAquireData<T>(maker);
        }

        public void AddActionCallback(BaseAction actionSequence)
        {
            actionService.AddActionCallback(actionSequence);
        }

        public void NavigationComplited()
        {
            actionService.SendInfoForActions(AIEvent.NavigationComplited);
        }

        public void ChangeParam(CharacterParam param, float amount)
        {
            paramsService.SetParam(param, paramsService.GetValue(param) + amount);
        }

        public ParamsService GetParamsService()
        {
            return paramsService;
        }
    }
}