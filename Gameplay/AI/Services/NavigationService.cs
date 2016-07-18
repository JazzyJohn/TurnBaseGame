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
        public float runSpeed = 2.0f;
        public int parkingWaypointAmount = 2;
        public int NormalDistance = 5;
        public int RunDistance = 10;
        protected Seeker seeker;
        protected ParamsService paramService;
        protected Path path;
        private int currentWaypoint = 0;
        bool waitForPath = false;
        protected bool pathComplete = false;
        public float cuttingEdgeDistance = 0.5f;
        Vector3 pathTarget;
        float desiredSpeed;
        Pawn owner;
        public const float REACH_DISTANCE = 0.1f;
        public float overridedReachDistance = 0.0f;
        // Use this for initialization
        public void Start()
        {
            seeker = GetComponent<Seeker>();
            desiredSpeed = commonSpeed;
            paramService = GetComponent<ParamsService>();
            if(paramService == null || seeker == null)
            {
                Debug.LogError("No ParamsService or No Seeker");
                enabled = false;
            }
            paramService.SetParam(CharacterParam.NormalMovment_Distance, NormalDistance);
            paramService.SetParam(CharacterParam.RunMovment_Distance, RunDistance);
        }

        public void OnPathComplete(Path p)
        {
            if (waitForPath)
            {
                Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
                if (!p.error)
                {
                    int pathLenght = p.vectorPath.Count;
                    bool isItPossible = paramService.GetValue(CharacterParam.RunMovment_Distance) > pathLenght;
                    bool isRunNeeded = paramService.GetValue(CharacterParam.NormalMovment_Distance) < pathLenght;
                    waitForPath = false;

                    if (!isItPossible || (isRunNeeded && !owner.GetAI().CouldRun()))
                    {
                        return;
                    }
                    path = p;   
                    //Reset the waypoint counter
                    currentWaypoint = 0;
                   
                    owner.GetAI().StartMoveOnPath(isRunNeeded);
                    if(isRunNeeded)
                    {
                        desiredSpeed = runSpeed;
                    }
                    else
                    {
                        desiredSpeed = commonSpeed;
                    }
                }
            }
        }


        public void StartPath(Grid.Cell targetCell, float overridedReachDistance = 0)
        {
            Vector3 target = GridController.GetV3FromCell(targetCell);
            if ((pathTarget - target).sqrMagnitude < 0.25f)
            {
                Debug.Log("No need of path");
                return;
            }
            if (!waitForPath)
            {
                pathTarget = target;
                waitForPath = true;
                pathComplete = false;
                seeker.StartPath(owner.myTransform.position, target, OnPathComplete);
               
                this.overridedReachDistance = overridedReachDistance;
            }
        }
      
        public void CancelNavigation()
        {
            path = null;
            pathComplete = false;
            waitForPath = false;
            overridedReachDistance = 0.0f;
        }
        protected void FixedUpdate()
        {
            if (path != null)
            {

                //Direction to the next waypoint
                Vector3 cellBasedPoint = GridController.NormalizeV3( path.vectorPath[currentWaypoint]);
                Vector3 dir = (cellBasedPoint - transform.position).normalized;
                bool isNeedParking  = currentWaypoint >= path.vectorPath.Count - parkingWaypointAmount;
                float reachDistance;
                if (currentWaypoint == path.vectorPath.Count - 1)
                {

                    if (overridedReachDistance == 0.0f)
                    {
                       reachDistance = REACH_DISTANCE;
                    }
                    else
                    {
                        reachDistance = overridedReachDistance;
                    }

                }
                else
                {
                    reachDistance = cuttingEdgeDistance;
                }

                if (isNeedParking)
                {
                    desiredSpeed = commonSpeed;
                }

                owner.MoveTo(dir, desiredSpeed);

                //Check if we are close enough to the next waypoint
                //If we are, proceed to follow the next waypoint
               
                if (IsReached(transform.position, cellBasedPoint, reachDistance))
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
                if (!waitForPath)
                {
                    owner.GetAI().NavigationComplited();
                }
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