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
    public enum TalkType
    {
        SIMPLE_RESPONDER = 0,
        SIMPLE_REQUESTER = 1

    }
    public struct AIAdditionalData
    {
        public Vector3 directionOfLastDamage;
    }
    public class PawnAI : BaseAI
    {
        Pawn owner;
        AIAdditionalData addData = new AIAdditionalData();
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

        public void ChangeParam(CharacterParam param, float amount, PawnAI pawnAI = null)
        {
            paramsService.SetValue(param, paramsService.GetValue(param) + amount);
            SendMessage("ForceUpdateParam", SendMessageOptions.DontRequireReceiver);
            DoSomeAdditionalLogicAbout(param, amount, pawnAI);
        }

        private void DoSomeAdditionalLogicAbout(CharacterParam param, float amount, PawnAI pawnAI)
        {

            switch(param)
            {
                case CharacterParam.Health:
                    if(amount < 0)
                    {
                        if (pawnAI == null)
                            addData.directionOfLastDamage = transform.forward;
                        else
                            addData.directionOfLastDamage = (transform.position - pawnAI.transform.position).normalized;

                        owner.PlayHurt();
                    }
                    else
                    {
                        owner.StartHealAnim();
                    }
                    break;
            }
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
                GamePlayEvent gameplayEvent = new GamePlayEvent(gameObject, GamePlayEventType.Kill);
                gameplayEvent.victim = gameObject;
                EventHandler.SendEvent(gameplayEvent);
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
            owner.SetMood(GetMood());
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

        public void StartTalk(TalkType talkType)
        {
            owner.StartTalk(talkType);
        }
        public AIAdditionalData GetAIAddData()
        {
            return addData;
        }
   
    }
}