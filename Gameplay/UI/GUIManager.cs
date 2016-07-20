using UnityEngine;
using System.Collections;
using InputLogic;

namespace UI
{
    public class GUIManager : MonoBehaviour {

       

        public void EndTurnBtn()
        {
            InputManager.EndTurn();
        }

        public void DoCover()
        {
            InputManager.StartAction(0);
        }
        public void DoSmallTalk()
        {
            InputManager.StartAction(1);
        }
        public void DoOpenDoor()
        {
            InputManager.StartAction(2);
        }
        public void DoUnlockDoor()
        {
            InputManager.StartAction(3);
        }
        public void DoHeal()
        {
            InputManager.StartAction(4);
        }
        
    }
}
