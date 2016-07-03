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
    }
}
