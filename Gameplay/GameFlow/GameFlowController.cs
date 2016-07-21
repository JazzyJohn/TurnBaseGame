using UnityEngine;
using System.Collections;
using PawnLogic;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UI;
namespace GameFlow
{
    public class GameFlowController : MonoBehaviour
    {
        static GameFlowController s_instance;
        public void Awake()
        {
            s_instance = this;
        }

        List<Pawn> all_pawns = new List<Pawn>();
        public void Start()
        {
           
        }
        public static void EndTurn()
        {
            s_instance._EndTurn();
        }

        private void _EndTurn()
        {
            all_pawns.Clear();
            all_pawns.AddRange(FindObjectsOfType<Pawn>());
            foreach(Pawn pawn in all_pawns)
            {
                if(!pawn.GetAI().EndTurn())
                {
                    return;
                }
            }
            _NextTurn();
            
        }
        private void _NextTurn()
        {
            foreach (Pawn pawn in all_pawns)
            {
                pawn.GetAI().NewTurn();
            }
            GUIManager.NewTurn();
        }
        public static void FinishLevel()
        {
            s_instance._FinishLevel();
        }
        private void _FinishLevel()
        {
            Debug.Log("levelFinished");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}