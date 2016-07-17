using UnityEngine;
using System.Collections;
using Grid;
using AI;
using Descriptor;

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
        public const string ON_GROUND = "OnGround";
        public const float MIN_ANGLE = 2.0f;
        Cell position;
        public State[] states;
        public float rotationSpeed;
        public float cacheAngle;        
        public bool isControllable;
        float currentSpeed = 0.0f;
        float speedTime = 0.0f;
        float tempLerpSpeed = 0.0f;
        State currentState;
        PawnAI pawnAI;
        Animator animator;
        ActorDescriptor actorDescriptor;
        [HideInInspector]
        public Transform myTransform;
        void Awake()
        {
            currentState = states[0];
            pawnAI = GetComponent<PawnAI>();
            pawnAI.Init(this);
            animator = GetComponent<Animator>();
            myTransform = transform;
            actorDescriptor = GetComponent<ActorDescriptor>();
        }
        public void Start()
        {
            position = GridController.GetCellFromCoord(myTransform.position);
            position.Enter(this);
            myTransform.position = GridController.GetV3FromCell(position);
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
            direction.y = 0;
            direction.Normalize();
            Vector3 flatForward = myTransform.forward;
            flatForward.y = 0;
            flatForward.Normalize();
            float angle = Vector3.Angle(flatForward, direction) * Mathf.Sign(Vector3.Cross(flatForward,direction).y);
            if (Mathf.Sign(angle) != Mathf.Sign(cacheAngle) || Mathf.Abs(angle) > Mathf.Abs(cacheAngle))
            {
                cacheAngle = angle;
            }
            if( Mathf.Abs(angle) > MIN_ANGLE)
            {
               
                //animator.SetFloat(FORWARD_SPEED, 0);
                animator.SetFloat(TURNING_ANGLE, Mathf.Min(1.0f,cacheAngle / 90.0f));
                myTransform.LookAt(myTransform.position+ Vector3.RotateTowards(myTransform.forward, direction, rotationSpeed*Time.deltaTime, float.MaxValue));
                if (currentSpeed == 0)
                    return;
            }
            else
            {
                animator.SetFloat(TURNING_ANGLE,0.0f);
                myTransform.LookAt(myTransform.position + Vector3.RotateTowards(myTransform.forward, direction, rotationSpeed * Time.deltaTime, float.MaxValue));
            }
            if(speedTime > 1.0f)
            {
                tempLerpSpeed = currentSpeed;
                speedTime = 0.0f;
            }
            else
            {
                if (Mathf.Abs(speed - tempLerpSpeed) > Mathf.Epsilon)
                {
                    speedTime += Time.fixedDeltaTime;
                    currentSpeed = Mathf.Lerp(tempLerpSpeed, speed, speedTime);
                }
            }
           
            
            cacheAngle = 0.0f;
            animator.SetFloat(FORWARD_SPEED, currentSpeed);
        }
        public void StopMove()
        {
            animator.SetFloat(TURNING_ANGLE, 0);
            animator.SetFloat(FORWARD_SPEED, 0);
            currentSpeed = 0.0f;
            tempLerpSpeed = 0.0f;
            speedTime = 0.0f;
        }
        public void Update()
        {
            Cell newCell = GridController.GetCellFromCoord(myTransform.position);
            if(newCell != position)
            {
                position.Leave(this);
                position = newCell;
                position.Enter(this);
                pawnAI.CellChanged();
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
            Debug.Log("I'm DEAD" + gameObject.name);
            animator.SetBool(TAKE_COVER, true);
            pawnAI.StartDeath();
            Destroy(gameObject, 10.0f);
        }

        public void Jump()
        {
            animator.SetBool(ON_GROUND, false);
        }

        public ActorDescriptor GetActorDescriptor()
        {
            return actorDescriptor;
        }
    }
}