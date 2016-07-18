using UnityEngine;
using System.Collections;
using PawnLogic;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
        public  void Start()
        {
            all_pawns.AddRange(FindObjectsOfType<Pawn>());
        }
        public static void EndTurn()
        {
            s_instance._EndTurn();
        }

        private void _EndTurn()
        {
            foreach(Pawn pawn in all_pawns)
            {
                pawn.GetAI().EndTurn();
            }
            _NextTurn();
        }
        private void _NextTurn()
        {
            foreach (Pawn pawn in all_pawns)
            {
                pawn.GetAI().NewTurn();
            }
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