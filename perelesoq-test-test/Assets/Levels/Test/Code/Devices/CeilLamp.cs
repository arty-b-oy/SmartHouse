using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Code.Abstract.Enume;
using Code.Abstract.Interface;
using Code.Configs;
using Code.Static;
namespace Code.Devises
{
    public class CeilLamp : MonoBehaviour, IDevice
    {
        [SerializeField] private GameObject[] NextDevice;
        [SerializeField] private int ID;
        [SerializeField] private string name;
        [SerializeField] private bool isActiveDevise;
        [SerializeField] int numberRoom;
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

        public string GetNameDevice()
        {
            return name;
        }

        public TypeDevise GetTypeDevise()
        {
            return TypeDevise.CeilLamp;
        }

        public bool GetActiv()
        {
            return isActiveDevise;
        }


        public void RebootDevice()
        {
            isActiveDevise = isActiveDeviseForReboot;
            InformationBetweenScenes.LightRoom[numberRoom - 1].SetActive(isActiveDevise);
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
            result += isCurrent ? deviceConfig.CeilLampPowerConsumption : 0;
            InformationBetweenScenes.LightRoom[numberRoom - 1].SetActive(isCurrent);
            isActiveDevise = isCurrent;
            for (int i = 0; deviceslist.Count > i; i++)
            {
                result += deviceslist[i].CheckElectricalSystem(isActiveDevise);
            }
            return result;
        }
    }
}