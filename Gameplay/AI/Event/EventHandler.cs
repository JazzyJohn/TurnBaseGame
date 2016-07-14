using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI
{
    public enum EventType
    {
        LockDoorOpen,
        Kill,
        SeeAgent
    }
    public class GamePlayEvent
    {
        public EventType type;
        public GameObject sender;
        public GameObject victim;
        public Vector3 originPosition;
        public GamePlayEvent(GameObject gameObject, EventType type)
        {
            this.type = type;
            this.sender = gameObject;
        }
    }
    public enum ReciverType
    {
        Victim,
        Source,
        Witness
    }
    [System.Serializable]
    public abstract class ReactionFilter:ScriptableObject
    {
        public bool negative;
        public abstract bool CheckFilter(PawnAI pawnAI);
    }
    [System.Serializable]
    public class EventReaction
    {
        public EventType type;
        public ReciverType reciverType;
        public BaseAction action;
        public ReactionFilter[] filters;


        public bool IsPassFilter(PawnAI pawnAi)
        {
            if(filters == null || filters.Length ==0)
            {
                return true;
            }
            foreach(ReactionFilter filter in filters)
            {
                if(!filter.CheckFilter(pawnAi))
                {
                    return false;
                }
            }
            return true;

        }
    }
    public class EventHandler : MonoBehaviour
    {
        public List<EventReaction> reactionList;
        PawnAI pawnAi;
        PerceptionService perceptionService;

        void Start()
        {
            pawnAi = GetComponent<PawnAI>();
            perceptionService = GetComponent<PerceptionService>();
        }

        void ReciveEvent(GamePlayEvent gameplayEvent)
        {
            //Debug.Log("recived event" + gameplayEvent.type);
            ReciverType reciverType;
            if (gameplayEvent.victim == transform.gameObject)
                reciverType = ReciverType.Victim;
            else if (gameplayEvent.sender == transform.gameObject)
                reciverType = ReciverType.Source;
            else
                reciverType = ReciverType.Witness;

            EventReaction reaction  = reactionList.Find(x => x.type == gameplayEvent.type && x.reciverType == reciverType && x.IsPassFilter(pawnAi));

            if(reaction == null)
            {
                Debug.Log("Event has no reaction");
                return;
            }
            if(perceptionService != null && reciverType != ReciverType.Source &&!perceptionService.IsInPerception(gameplayEvent.originPosition,gameplayEvent.sender))
            {
                Debug.Log("Event not in Perception");
                return;
            }

            
            Context context = new Context(pawnAi, gameplayEvent.sender);
            context.AddToAdditional(gameplayEvent.victim);
            context.allowSwitchTarget = EventManager.GetAllowSwitchInAction(gameplayEvent.type);
            reaction.action.StartAction(context);
        }
       

        public void HandleGamePlayEvent(GamePlayEvent gameplayEvent)
        {
            ReciveEvent(gameplayEvent);
        }
        public void SendEvent(GamePlayEvent gameplayEvent)
        {
            gameplayEvent.originPosition = gameplayEvent.sender.transform.position;
            gameplayEvent.sender.SendMessage("HandleGamePlayEvent", gameplayEvent,SendMessageOptions.DontRequireReceiver);
            if(gameplayEvent.sender != gameplayEvent.victim)
                gameplayEvent.victim.SendMessage("HandleGamePlayEvent", gameplayEvent, SendMessageOptions.DontRequireReceiver);

            float spreadDistance = EventManager.GetSpreadDistance(gameplayEvent.type);
            if(spreadDistance > 0.0f)
            {
                Collider[] colliders = Physics.OverlapSphere(gameplayEvent.originPosition, spreadDistance);
                List<Transform> alreadySended = new List<Transform>();
                foreach(Collider collider in colliders)
                {
                    Transform rootOfWitness = collider.transform.root;
                    if (alreadySended.Contains(rootOfWitness))
                    {
                        continue;
                    }
                    alreadySended.Add(rootOfWitness);
                    if (rootOfWitness == gameplayEvent.sender.transform.root || rootOfWitness == gameplayEvent.victim.transform.root)
                    {
                        continue;
                    }                   
                    rootOfWitness.SendMessage("HandleGamePlayEvent", gameplayEvent, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
     
    }

}