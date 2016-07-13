using UnityEngine;
using System.Collections;

namespace AI
{
    public class SendEvent : BaseAction
    {
        public EventType type;

        public override void StartAction(Context context)
        {
            EventHandler eventHandler = context.pawnAI.GetEventHandler();
            if(eventHandler == null)
            {
                return;
            }
            GamePlayEvent gameplayEvent = new GamePlayEvent(context.pawnAI.gameObject, type);
            gameplayEvent.victim = context.go;
            eventHandler.SendEvent(gameplayEvent);
        }
    }
}
