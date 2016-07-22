using UnityEngine;
using System.Collections;

namespace AI
{
    public class SendEvent : BaseAction
    {
        public GamePlayEventType type;

        protected override bool DoAction(Context context)
        {
            EventHandler eventHandler = context.pawnAI.GetEventHandler();
            if(eventHandler == null)
            {
                return true;
            }
            GamePlayEvent gameplayEvent = new GamePlayEvent(context.pawnAI.gameObject, type);
            gameplayEvent.victim = context.go;
            EventHandler.SendEvent(gameplayEvent);
            return true;
        }
    }
}
