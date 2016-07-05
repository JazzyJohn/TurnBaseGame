using UnityEngine;
using System.Collections;
using Pathfinding;
using PawnLogic;
using Grid;
namespace AI
{
    public class NavigationService : MonoBehaviour
    {
        public float commonSpeed = 1.0f;
        protected Seeker seeker;
        protected Path path;
        private int currentWaypoint = 0;
        bool waitForPath = false;
        protected bool pathComplete = false;
        Vector3 pathTarget;
        float desiredSpeed;
        Pawn owner;
        public const float REACH_DISTANCE = 0.1f;
        // Use this for initialization
        public void Start()
        {
            seeker = GetComponent<Seeker>();
            desiredSpeed = commonSpeed;
        }

        public void OnPathComplete(Path p)
        {
            if (waitForPath)
            {
                Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
                if (!p.error)
                {
                    path = p;
                    //Reset the waypoint counter
                    currentWaypoint = 0;
                    waitForPath = false;
                    owner.GetAI().StartMoveOnPath();
                }
            }
        }


        public void StartPath(Grid.Cell targetCell)
        {
            Vector3 target = GridController.GetV3FromCell(targetCell);
            if ((pathTarget - target).sqrMagnitude < 0.25f)
            {
                return;
            }
            if (!waitForPath)
            {
                pathTarget = target;
                waitForPath = true;
                pathComplete = false;
                seeker.StartPath(owner.myTransform.position, target, OnPathComplete);
            }
        }
      
        public void CancelNavigation()
        {
            path = null;
            pathComplete = false;
            waitForPath = false;
        }
        protected void FixedUpdate()
        {
            if (path != null)
            {

                //Direction to the next waypoint
                Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
                owner.MoveTo(dir, desiredSpeed);

                //Check if we are close enough to the next waypoint
                //If we are, proceed to follow the next waypoint
                if (IsReached(transform.position, path.vectorPath[currentWaypoint]))
                {
                    currentWaypoint++;
                    if (currentWaypoint >= path.vectorPath.Count)
                    {
                        owner.GetAI().NavigationComplited();
                        
                        pathComplete = true;
                        path = null;
                    }
                    return;
                }
               
            }
            else
            {
                owner.StopMove();
            }

        }
        public bool OnPath()
        {
            return path != null;
        }
        public bool IsPathComplete()
        {
            return pathComplete;
        }
        public bool IsReached(Vector3 point, Vector3 target, float inputSize = 0.0f)
        {
            if (inputSize == 0.0f)
            {
                inputSize = REACH_DISTANCE;
            }
          
            
            Vector3 flatPoint = point, flatPostion = target;
            flatPostion.y = 0;
            flatPoint.y = 0;

            if ((flatPoint - flatPostion).sqrMagnitude < inputSize * inputSize)
            {
                return true;

            }
            

            return false;
        }
        public void SetSpeed(float speed)
        {
            this.desiredSpeed = speed;
        }
        public void Init(Pawn owner)
        {
            this.owner = owner;
        }
    }
}