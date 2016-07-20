using UnityEngine;
using System.Collections;
namespace AI.Conditions
{
    [CreateAssetMenu(fileName = "MaxHealthCondition", menuName = "AI/TargetCondition/MaxHealthCondition", order = 1)]
    public class MaxHealthCondition : TargetCondition
    {

        protected override bool _Check(Descriptor.ActorDescriptor descr)
        {
            HealthService service = descr.GetComponent<HealthService>();
            if (service == null)
                return false;
            return service.IsMaxHealth();
        }
    }
}