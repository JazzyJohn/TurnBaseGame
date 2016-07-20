using UnityEngine;
using System.Collections;
using PawnLogic;
namespace AI
{
    public class HealthService : MonoBehaviour
    {
        ParamsService paramService;
        Pawn owner;
        public float maxHealth;
        void Start()
        {
            paramService = GetComponent<ParamsService>();
            owner = GetComponent<Pawn>();
            if (paramService == null || owner == null)
            {
                enabled = false;
                return;
            }

            paramService.SetValue(CharacterParam.Health, maxHealth);
            paramService.SetValue(CharacterParam.MaxHealth, maxHealth);
        }
        
        void ForceUpdateParam()
        { 
            if(paramService.GetValue(CharacterParam.Health)<=0.0f)
            {
                owner.StartDeath();
            }
            if(paramService.GetValue(CharacterParam.Health) > paramService.GetValue(CharacterParam.MaxHealth))
            {
                paramService.SetValue(CharacterParam.Health, paramService.GetValue(CharacterParam.MaxHealth));
            }

        }
        public bool IsMaxHealth()
        {
            return paramService.GetValue(CharacterParam.Health) < paramService.GetValue(CharacterParam.MaxHealth);
        }
        // Update is called once per frame
        void Update()
        {
            ForceUpdateParam();
        }
    }
}
