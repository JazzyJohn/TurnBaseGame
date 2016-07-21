using UnityEngine;
using System.Collections;
namespace AI
{
    public class BaseAI : MonoBehaviour
    {

    
        public virtual bool CanMove()
        {
            return false;
        }

        public virtual void MoveTo(Grid.Cell selectedCell, float overridedReachDistance = 0, bool freeAction = false)
        {
         
        }

        
    }
}
