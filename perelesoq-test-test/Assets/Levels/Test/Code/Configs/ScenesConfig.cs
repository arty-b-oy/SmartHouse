using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Config/ScenesConfig")]
    public class ScenesConfig : ScriptableObject
    {
        public string[] nameScenes;
    }
}