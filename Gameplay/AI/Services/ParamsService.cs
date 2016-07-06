using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI
{
    public enum CharacterParam
    {
        Health,
        MaxHealth,
        Defence,
        NormalMovment_Distance,
        RunMovment_Distance
    }
    public class ParamsService : MonoBehaviour
    {
        Dictionary<CharacterParam, float> all_params = new Dictionary<CharacterParam, float>();

        public float GetValue(CharacterParam param)
        {
            if(all_params.ContainsKey(param))
            {
                return all_params[param];
            }
            return 0.0f;
        }
        public void SetParam(CharacterParam param, float value)
        {
            all_params[param] = value;
        }
    }
}