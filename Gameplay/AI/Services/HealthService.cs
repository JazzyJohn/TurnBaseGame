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

            paramService.SetParam(CharacterParam.Health, maxHealth);
            paramService.SetParam(CharacterParam.MaxHealth, maxHealth);
        }
        

        // Update is called once per frame
        void Update()
        {
            if(paramService.GetValue(CharacterParam.Health)<=0.0f)
            {
                owner.StartDeath();
            }
        }
    }
}
