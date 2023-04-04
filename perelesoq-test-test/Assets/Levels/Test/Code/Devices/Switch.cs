using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Code.Abstract.Enume;
using Code.Abstract.Interface;
using Code.Configs;

namespace Code.Devises
{
    public class Switch : MonoBehaviour, IDevice
    {
        [SerializeField] private GameObject[] NextDevice;
        [SerializeField] private int ID;
        [SerializeField] private string name;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private bool isActiveDevise;
        [SerializeField] private bool isCurrent;
        [Inject] private MaterialConfig materialConfig;
        private List<IDevice> deviceslist = new List<IDevice>();
        private bool isActiveDeviseForReboot;
        private MeshRenderer meshRendererForReboot;
        void Awake()
        {
            for (int i = 0; NextDevice.Length > i; i++)
                deviceslist.Add(NextDevice[i].GetComponent<IDevice>());
            isActiveDeviseForReboot = isActiveDevise;
            isCurrent = isActiveDevise;
            meshRendererForReboot = meshRenderer;

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
            return TypeDevise.Switcher;
        }

        public bool GetActiv()
        {
            return isActiveDevise && isCurrent;
        }

        public void RebootDevice()
        {
            isActiveDevise = isActiveDeviseForReboot;
            isCurrent = isActiveDevise;
            meshRenderer = meshRendererForReboot;
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
            {
                list.Add(ID, isActiveDevise);
            }
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
            if (id == ID)
            {
                isActiveDevise = !isActiveDevise;
                meshRenderer.material = isActiveDevise ? materialConfig.onMaterial : materialConfig.offMaterial;
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
                result += deviceslist[i].CheckElectricalSystem(isActiveDevise && IsCurrent);
            }
            return result;
        }
    }
}