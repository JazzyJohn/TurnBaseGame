﻿using UnityEngine;
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
        PerceptionService perceptionService;
        EventHandler eventHandler;
        MoodService moodService;
        public void Init(Pawn owner)
        {
            this.owner = owner;
            navigationService = GetComponent<NavigationService>();
            navigationService.Init(owner);
            actionService = GetComponent<ActionService>();
            actionService.Init(owner);
            paramsService = GetComponent<ParamsService>();
            perceptionService = GetComponent<PerceptionService>();
            eventHandler = GetComponent<EventHandler>();
            moodService = GetComponent<MoodService>();

        }

        public override bool CanMove()
        {
            return actionService.CanMove();
        }
        public override void MoveTo(Grid.Cell selectedCell, float overridedReachDistance = 0)
        {
            navigationService.StartPath(selectedCell, overridedReachDistance);
        }
        public void CancelNavigation()
        {
            navigationService.CancelNavigation();
        }

        public void StartMoveOnPath(bool isRun)
        {
            actionService.StartMoveOnPath(isRun);
        }
        public void NewTurn()
        {
            actionService.NewTurn();
            perceptionService.UpdateDetectList();
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

        public virtual bool CouldRun()
        {
            return actionService.CouldRun();
        }

        public void CellChanged()
        {
            perceptionService.UpdateDetectList();
            //TODO: UpdateDetectList of other Characters;
            SendMessage("OnCellChanged", SendMessageOptions.DontRequireReceiver);
        }

        public PerceptionService GetPerceptionService()
        {
            return perceptionService;
        }

        public void StartDeath()
        {
            perceptionService.enabled = false;
            navigationService.CancelNavigation();
            navigationService.enabled = false;
            paramsService.enabled = false;
            actionService.enabled = false;
            if(eventHandler != null)
            {
                GamePlayEvent gameplayEvent = new GamePlayEvent(gameObject, EventType.Kill);
                gameplayEvent.victim = gameObject;
                eventHandler.SendEvent(gameplayEvent);
            }
        }

        public EventHandler GetEventHandler()
        {
            return eventHandler;
        }
        public Mood GetMood()
        {
            if (moodService == null)
                return Mood.Neutral;
            return moodService.GetMood();
        }
        public MoodService GetMoodService()
        {
            return moodService;
        }

        public void MoodChanged()
        {
            perceptionService.ResetPerceptionValue();
            //TODO REWORK:
            perceptionService.ClearLastPushed();
        }

        public void LowerMood()
        {
            if (moodService == null)
                return;
            moodService.LowerMood();
        }

        public void UpMood()
        {
            if (moodService == null)
                return;
            moodService.UpMood();
        }

        public void PushPerception(PerceptionValue perceptionValue)
        {
            perceptionService.PushPerception(perceptionValue);
        }
    }
}