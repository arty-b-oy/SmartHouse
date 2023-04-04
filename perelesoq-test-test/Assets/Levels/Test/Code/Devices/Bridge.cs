using System.Collections.Generic;
using UnityEngine;
using Code.Abstract.Enume;
using Code.Abstract.Interface;
namespace Code.Devises
{
    public class Bridge : MonoBehaviour, IDevice
    {
        [SerializeField] private GameObject[] NextDevice;
        [SerializeField] private int ID;
        [SerializeField] private string name;
        private List<IDevice> deviceslist = new List<IDevice>();

        void Awake()
        {
            for (int i = 0; NextDevice.Length > i; i++)
                deviceslist.Add(NextDevice[i].GetComponent<IDevice>());
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
            return TypeDevise.Bridge;
        }

        public bool GetActiv()
        {
            return true;
        }

        public void RebootDevice()
        {
            for (int i = 0; deviceslist.Count > i; i++)
                deviceslist[i].RebootDevice();
        }

        public List<IDevice> GetThisAndNextDevise()
        {
            List<IDevice> list = new List<IDevice>();
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
                list.Add(ID, true);
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
            for (int i = 0; deviceslist.Count > i; i++)
            {
                result += deviceslist[i].CheckElectricalSystem(true && isCurrent);
            }
            return result;
        }
    }
}