using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Config/DeviceConfig")]
    public class DeviceConfig : ScriptableObject
    {
        public int CeilLampPowerConsumption;
        public int LampPowerConsumption;
        public int DoorDrivePowerConsumption;
    }
}