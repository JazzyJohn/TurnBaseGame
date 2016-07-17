using UnityEngine;
using System.Collections;
using PawnLogic;
using Grid;
using AI;
using Pathfinding;
using System.Collections.Generic;
namespace InputLogic
{
    public class OrderControl : MonoBehaviour
    {
        Pawn selectedPawn;
        Vector3 pathTarget;
        Seeker seeker;
        bool newSelection = false;
        Path path;
        LineRenderer pathRenderer;
        int nextAction = -1;
        // Use this for initialization
        void Awake()
        {
            seeker = GetComponent<Seeker>();
            pathRenderer = GetComponent<LineRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateSelectionPath();
          
        }
        
        void UpdateSelectionPath()
        {
            if(selectedPawn != null)
            {
                Vector3 target = GridController.GetV3FromCell(GridController.GetSelectedCell());
                if ((pathTarget - target).sqrMagnitude < 0.25f && !newSelection)
                {
                    return;
                }
             
                newSelection = false;
                pathTarget = target;
               
                seeker.StartPath(GridController.GetV3FromCell(selectedPawn.GetPosition()), target, OnPathComplete);
            
                
            }
        }

        public void OnPathComplete(Path p)
        {
           
            if (!p.error)
            {
                path = p;
                //Reset the waypoint counter
                   
               
                UpdateDrawPath();
            }
          
        }
        void UpdateDrawPath()
        {
            if(path == null)
            {
                return;
            }
            pathRenderer.enabled = true;

            pathRenderer.SetVertexCount(path.vectorPath.Count);
            List<Vector3> positions = new List<Vector3>();
            foreach(Vector3 pos in path.vectorPath)
            {
                positions.Add(GridController.NormalizeV3(pos));
            }
            pathRenderer.SetPositions(positions.ToArray());
           
        }
        public void Click()
        {
            Cell selectedCell = GridController.GetSelectedCell();
            if (nextAction!=-1)
            {

                List<GameObject> gObjects = selectedCell.GetObjects();
                GameObject target = null;
                if (selectedCell.GetObjects().Count > 0)
                {
                    target = selectedCell.GetObjects()[0];
                }
                
                if (selectedCell.GetOccupants().Count > 0)
                {
                    target = selectedCell.GetOccupants()[0].gameObject;
                }
                ActionManager.DoAction(nextAction, target, selectedPawn);
                nextAction = -1;
            }
            else
            {
                if (selectedPawn == null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit[] hits = Physics.RaycastAll(ray);
                    foreach (RaycastHit hit in hits)
                    {
                        Pawn pawn = hit.transform.GetComponent<Pawn>();
                        if (pawn != null && pawn.IsControllable())
                        {
                            selectedPawn = pawn;
                            newSelection = true;
                            Debug.Log("Select" + selectedPawn);
                            break;
                        }
                    }
                }
                else
                {
                    Debug.Log("Goto" + selectedCell);
                    BaseAI baseAI = selectedPawn.GetAI();
                    if (selectedCell != selectedPawn.GetPosition() && baseAI.CanMove())
                    {
                        baseAI.MoveTo(selectedCell);
                    }
                }
            }
          
        }
        public void DoAction(int i)
        {
            nextAction = i;
         
        }
    }
}
