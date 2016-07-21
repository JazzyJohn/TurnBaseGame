using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AI;
using System;
using Render;
using InputLogic;
using PawnLogic;
namespace UI
{
    public class AgentUI : MonoBehaviour
    {
        public Text healthText;
        public ParamsService paramService;
        public PerceptionService perceptionService;
        public ArcRender rendererOfPerception;
        public float closeDistanceOfPerception = 0.3f;
        public AI.EventHandler eventHandler;
        public Image delayEventIndicator;
        public Image selectionIndicater;
        public Color selectedColor;
        Color notSelectedColor;
        Pawn owner;
        void Start()
        {
            notSelectedColor = selectionIndicater.color;
            owner = transform.root.GetComponent<Pawn>();
        }
        void Update()
        {
            if (paramService != null)
            {
                if (healthText != null)
                {
                    float health = paramService.GetValue(CharacterParam.Health);
                    float maxHealth = paramService.GetValue(CharacterParam.MaxHealth);
                    healthText.text = String.Format("{0} / {1}", health.ToString("0"), maxHealth.ToString("0"));
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
            if (eventHandler != null)
            {
                if(delayEventIndicator != null)
                {
                    if (eventHandler.IsHaveDelayedReaction())
                        delayEventIndicator.enabled = true;
                    else
                        delayEventIndicator.enabled = false;
                }
            }
            if(InputManager.GetSelectedPawn() == owner)
            {
                selectionIndicater.color = selectedColor;
            }
            else
            {
                selectionIndicater.color = notSelectedColor;
            }
        }
 
    }
}