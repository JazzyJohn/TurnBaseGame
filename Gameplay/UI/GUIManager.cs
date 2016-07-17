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
        
    }
}
