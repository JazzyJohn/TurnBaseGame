using UnityEngine;
using System.Collections;
using InputLogic;
using System.Collections.Generic;
using PawnLogic;
namespace Grid
{
    [System.Serializable]
    public class Cell
    {
        public bool selected = false;
        public int x;
        public int z;

        List<Pawn> ocupants = new List<Pawn>();
        List<GameObject> gObjects = new List<GameObject>();
        public void Leave(Pawn pawn)
        {
            ocupants.Remove(pawn);
        }

        public void Enter(Pawn pawn)
        {
            ocupants.Add(pawn);
        }
        public void Enter(GameObject go)
        {
            gObjects.Add(go);
        }
        public override string ToString()
        {
            return x + " " + z;
        }
        public List<GameObject> GetObjects()
        {
            return gObjects;
        }
    }

    public class GridController : MonoBehaviour
    {
        
        [System.Serializable]
        public class Tower
        {
            [SerializeField]
            Cell[] cells;
            public Tower()
            {
                cells = new Cell[1];
                cells[0] = new Cell();
            }
            public Cell this[int key]
            {
                get
                {
                    if (cells.Length <= key)
                        return cells[cells.Length];
                    if (key < 0)
                        return cells[0];
                    return cells[key];
                }
                set
                {
                    cells[key] = value;
                }
            }
        }
        [System.Serializable]
        public class Column
        {
            [SerializeField]
            Tower[] towers;

            public Column(int p)
            {
                towers = new Tower[p];
            }
            public Column()
            {

            }
            public Tower this[int key]
            {
                get
                {
                    if (key < 0|| towers.Length <= key)
                        return null;
                    return towers[key];
                }
                set
                {
                    towers[key] = value;
                }
            }
        }
        static GridController s_instance;

        public Column[] columns;

        public Column this[int key]
        {
            get
            {
                return columns[key];
            }
            set
            {
                columns[key] = value;
            }
        }
        
        public bool IsExist(int x, int y, int z)
        {
            if(x<0 || columns.Length <= x)
            {
                return false;
            }
            if(columns[x][y] != null)
            {
                return true;
            }
            return false;
        }

        public float cellSize = 1.0f;

        public int gridSize =100;

        Cell selectedCell;

        Vector3 startPosition;
        // Use this for initialization
        void Awake()
        {
            s_instance = this;
            startPosition = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
        }

        public Vector3 GetStartPosition()
        {
            return startPosition;
        }
        public void Update()
        {
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             RaycastHit hit;
             if (Physics.Raycast(ray, out hit))
             {
                 int column,row;
                 CalcualteRowAndCollumnFromV3(hit.point, out column, out row);
                 int h = InputManager.GetCamLvl();
                 
                 if(IsExist(column, row, h))
                 {

                     if (selectedCell!= null)
                        selectedCell.selected = false;
                     selectedCell = columns[column][row][h];
                     selectedCell.selected = true;
                 }
             }
  
        }

        public static void CalcualteRowAndCollumnFromV3(Vector3 position, out int column, out int row)
        {

            Vector3 localPosition = position - s_instance.startPosition ;
            column = (int)Mathf.Floor(localPosition.x);
            row = (int)Mathf.Floor(localPosition.z);
        }

        public static void CalcualteAllFromV3(Vector3 position, out int column, out int row, out int h)
        {

            Vector3 localPosition = position - s_instance.startPosition;
            column = (int)Mathf.Floor(localPosition.x);
            row = (int)Mathf.Floor(localPosition.z);
            h = (int)Mathf.Floor(localPosition.y);

        }

        public static GridController GetInstance()
        {
            return s_instance;
        }

        public void GenerateGrid()
        {
            columns = new Column[gridSize];
            for (int i = 0; i < gridSize; ++i)
            {
                columns[i] = new Column(gridSize);
                for (int j = 0; j < gridSize; ++j)
                {
                    columns[i][j] = new Tower();
                    Cell newcell = columns[i][j][0];
                    newcell.x = i;
                    newcell.z = j;
                }
            }
        }


        public static Cell GetCellFromCoord(Vector3 pos)
        {
            return s_instance._GetCellFromCoord(pos);
        }

        private Cell _GetCellFromCoord(Vector3 pos)
        {
            int col =0,row=0,h = 0;
            CalcualteAllFromV3(pos, out col, out row, out h);
            if (IsExist(col, row, h))
            {
                return columns[col][row][h];
            }
            return null;
        }

        public static Cell GetSelectedCell()
        {
            return s_instance.selectedCell;
        }

        public static Vector3 GetV3FromCell(Cell targetCell)
        {
            return s_instance._GetV3FromCell(targetCell);
        }

        private Vector3 _GetV3FromCell(Cell targetCell)
        {

           return startPosition + new Vector3(targetCell.x + cellSize / 2, 0, targetCell.z + cellSize / 2);
        }

        public static Vector3 NormalizeV3(Vector3 pos)
        {
            return s_instance._NormalizeV3(pos);
        }
        private Vector3 _NormalizeV3(Vector3 pos)
        {

            return new Vector3(Mathf.Floor(pos.x) + cellSize / 2, pos.y, Mathf.Floor(pos.z) + cellSize / 2);
        }

    }
}
