using UnityEngine;
using System.Collections;

namespace AI
{
    public class SendEvent : BaseAction
    {
        public GamePlayEventType type;

        protected override void _StartAction(Context context)
        {
            EventHandler eventHandler = context.pawnAI.GetEventHandler();
            if(eventHandler == null)
            {
                return;
            }
            GamePlayEvent gameplayEvent = new GamePlayEvent(context.pawnAI.gameObject, type);
            gameplayEvent.victim = context.go;
            EventHandler.SendEvent(gameplayEvent);
        }
    }
}
