using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Code.Abstract.Enume;
using Code.Abstract.Interface;
using Code.Configs;

namespace Code.Devises
{
    public class DoorDriver : MonoBehaviour, IDevice
    {
        [SerializeField] private GameObject[] NextDevice;
        [SerializeField] private int ID;
        [SerializeField] private string name;
        [SerializeField] private bool isActiveDevise;
        [SerializeField] private Animator animatorDoor;
        [Inject] private DeviceConfig deviceConfig;
        private bool isCurrent;
        private bool isActiveDeviseForReboot;
        private List<IDevice> deviceslist = new List<IDevice>();
        void Awake()
        {
            for (int i = 0; NextDevice.Length > i; i++)
                deviceslist.Add(NextDevice[i].GetComponent<IDevice>());
            isActiveDeviseForReboot = isActiveDevise;
        }

        public int GetIDDevice()
        {
            return ID;
        }

        public string GetNameDevice()
        {
            return name;
        }

        public TypeDevise GetTypeDevise()
        {
            return TypeDevise.DoorDriver;
        }

        public bool GetActiv()
        {
            return isActiveDevise;
        }

        public void RebootDevice()
        {
             isActiveDevise = isActiveDeviseForReboot;
             animatorDoor.SetInteger("State", isActiveDevise ? 1 : -1);
             for (int i = 0; deviceslist.Count > i; i++)
                deviceslist[i].RebootDevice();
        }
        public List<IDevice> GetThisAndNextDevise()
        {
            List<IDevice> list = new List<IDevice>();
            list.Add(this);
            for (int i = 0; deviceslist.Count > i; i++)
            {
                list.AddRange(deviceslist[i].GetThisAndNextDevise());
            }

            return list;
        }

        public Dictionary<int, bool> GetStateThisAndNextDevise()
        {
            Dictionary<int, bool> list = new Dictionary<int, bool>();
            if (!list.ContainsKey(ID))
                list.Add(ID, isActiveDevise);
            Dictionary<int, bool> result = new Dictionary<int, bool>();
            for (int i = 0; deviceslist.Count > i; i++)
            {
                result = deviceslist[i].GetStateThisAndNextDevise();
                foreach (var element in result)
                {
                    if (!list.ContainsKey(element.Key))
                        list.Add(element.Key, element.Value);
                }
            }
            return list;
        }

        public int ChangeStateDevise(int id)
        {
            int result = 0;
            if (id == ID && isCurrent)
            {
                animatorDoor.StopRecording();
                animatorDoor.SetInteger("State", isActiveDevise ? 1 : -1);
                isActiveDevise = !isActiveDevise;
                result += deviceConfig.DoorDrivePowerConsumption;
            }
            else
                for (int i = 0; deviceslist.Count > i; i++)
                {
                    result += deviceslist[i].ChangeStateDevise(id);
                }
            return result;
        }
        public int CheckElectricalSystem(bool IsCurrent)
        {
            int result = 0;
            isCurrent = IsCurrent;
            for (int i = 0; deviceslist.Count > i; i++)
            {
                result += deviceslist[i].CheckElectricalSystem(IsCurrent);
            }
            return result;
        }
    }
}