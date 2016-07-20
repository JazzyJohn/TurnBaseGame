using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AI;
using System;
using Render;
namespace UI
{
    public class AgentUI : MonoBehaviour
    {
        public Text healthText;
        public ParamsService paramService;
        public PerceptionService perceptionService;
        public ArcRender rendererOfPerception;
        public float closeDistanceOfPerception = 0.3f;
        // Update is called once per frame
        void Update()
        {
            if (paramService != null)
            {
                if (healthText != null)
                {
                    float health = paramService.GetValue(CharacterParam.Health);
                    float maxHealth = paramService.GetValue(CharacterParam.MaxHealth);
                    healthText.text = String.Format("{0} / {1}", health.ToString("0"), health.ToString("0"));
                }
            }
            if(perceptionService != null)
            {
                if(rendererOfPerception != null)
                {
                    PerceptionValue value = perceptionService.GetCurrentValue();
                    rendererOfPerception.angle_fov = value.FOV ;
                    rendererOfPerception.dist_max = value.maxDistance;
                    rendererOfPerception.dist_min = closeDistanceOfPerception;
                }
            }
        }
    }
}