using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Code.Abstract.Enume;
using Code.Abstract.Interface;
using Code.Configs;

namespace Code.Devises
{

    public class Lamp : MonoBehaviour, IDevice
    {
        [SerializeField] private GameObject[] NextDevice;
        [SerializeField] private int ID;
        [SerializeField] private string name;
        [SerializeField] private GameObject[] lights;
        [SerializeField] private bool isActiveDevise;
        [Inject] private DeviceConfig deviceConfig;
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

        public TypeDevise GetTypeDevise()
        {
            return TypeDevise.Lamp;
        }

        public bool GetActiv()
        {
            return isActiveDevise;
        }

        public string GetNameDevice()
        {
            return name;
        }

        public void RebootDevice()
        {
            isActiveDevise = isActiveDeviseForReboot;
            for (int i = 0; lights.Length > i; i++)
                lights[i].SetActive(isActiveDevise);
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
            for (int i = 0; deviceslist.Count > i; i++)
            {
                result += deviceslist[i].ChangeStateDevise(id);
            }
            return result;
        }

        public int CheckElectricalSystem(bool isCurrent)
        {
            int result = 0;
            print(deviceConfig.LampPowerConsumption);
            isActiveDevise = isCurrent;
            for (int i = 0; lights.Length > i; i++)
                lights[i].SetActive(isCurrent);
            result += isCurrent ? deviceConfig.LampPowerConsumption : 0;
            for (int i = 0; deviceslist.Count > i; i++)
                result += deviceslist[i].CheckElectricalSystem(isActiveDevise);
            return result;
        }
    }
}