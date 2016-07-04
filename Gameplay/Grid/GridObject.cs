using UnityEngine;
using System.Collections;

namespace Grid
{
    public class GridObject : MonoBehaviour
    {

        void Start()
        {
            GridController.GetCellFromCoord(transform.position).Enter(gameObject);
        }

    }

}