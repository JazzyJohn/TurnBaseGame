using UnityEngine;
using System.Collections;
using Grid;
using AI;

namespace PawnLogic
{
    [System.Serializable]
    public class State
    {
        public string name;
        public bool isNavigating;
    }
    public class Pawn : MonoBehaviour
    {
        public const string TURNING_ANGLE = "turning angle";
        public const string FORWARD_SPEED = "forward speed";
        public const string TAKE_COVER = "cover";
        public const float MIN_ANGLE = 2.0f;
        Cell position;
        public State[] states;
        public float rotationSpeed;

        public bool isControllable;
        State currentState;
        PawnAI pawnAI;
        Animator animator;
        [HideInInspector]
        public Transform myTransform;
        void Awake()
        {
            currentState = states[0];
            pawnAI = GetComponent<PawnAI>();
            pawnAI.Init(this);
            animator = GetComponent<Animator>();
            myTransform = transform;
        }
        public void Start()
        {
            position = GridController.GetCellFromCoord(myTransform.position);
        }
        public void PlayState(State state)
        {

        }
        public void MoveTo(Vector3 direction, float speed)
        {
            if(!currentState.isNavigating)
            {
                return;
            }

            float angle = Vector3.Angle(myTransform.forward, direction);
            if( Mathf.Abs(angle) > MIN_ANGLE)
            { 
                //animator.SetFloat(FORWARD_SPEED, 0);
                animator.SetFloat(TURNING_ANGLE, angle /360.0f);
                myTransform.LookAt(myTransform.position+ Vector3.RotateTowards(myTransform.forward, direction, rotationSpeed*Time.deltaTime, float.MaxValue));
                return;
            }
            animator.SetFloat(FORWARD_SPEED, speed);
        }
        public void StopMove()
        {
            animator.SetFloat(TURNING_ANGLE, 0);
            animator.SetFloat(FORWARD_SPEED, 0);
        }
        public void Update()
        {
            Cell newCell = GridController.GetCellFromCoord(myTransform.position);
            if(newCell != position)
            {
                position.Leave(this);
                position = newCell;
                position.Enter(this);
            }
        }

        public void DoCover()
        {
            animator.SetBool(TAKE_COVER, true);
        }
        public bool IsControllable()
        {
            return isControllable;
        }

        public Cell GetPosition()
        {
            return position;
        }

        public PawnAI GetAI()
        {
            return pawnAI;
        }


        public void StartDeath()
        {
            Debug.Log("I'm DEAD");
            Destroy(gameObject);
        }
    }
}