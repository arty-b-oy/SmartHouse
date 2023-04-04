using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Config/MaterialConfig")]
    public class MaterialConfig : ScriptableObject
    {
        public Material onMaterial;
        public Material offMaterial;
    }
}