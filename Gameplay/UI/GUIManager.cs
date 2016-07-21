using UnityEngine;
using System.Collections;
using InputLogic;
using UnityEngine.UI;

namespace UI
{
    public class GUIManager : MonoBehaviour {

        public Image[] actionSelectionList;
        public Image endTurnDisable;
        bool waitForNewTurn = false;
        int currentSelection = -2;
        static GUIManager s_Instance;
        void Awake()
        {
            s_Instance = this;
        }
        public void Update()
        {
            int newSelection  = InputManager.SelecetedAction();
            if(currentSelection != newSelection)
            {
                currentSelection = newSelection;
                for(int i=0; i <actionSelectionList.Length; ++i)
                {
                    if(currentSelection == i)
                    {
                        actionSelectionList[i].enabled = true;
                    }
                    else
                    {
                        actionSelectionList[i].enabled = false;
                    }
                }
            }
        }

        public void EndTurnBtn()
        {
            if (!waitForNewTurn)
            {
                waitForNewTurn = true;
                endTurnDisable.enabled = true;
                InputManager.EndTurn();               
            }
        }
        public void _NewTurn()
        {
            waitForNewTurn = false;
            endTurnDisable.enabled = false;
        }
        public static void NewTurn()
        {
            s_Instance._NewTurn();
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
