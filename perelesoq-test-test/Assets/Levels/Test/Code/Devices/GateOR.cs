using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Code.Abstract.Enume;
using Code.Abstract.Interface;
using Code.Configs;

namespace Code.Devises
{
    public class GateOR : MonoBehaviour, IDevice
    {
        [SerializeField] private GameObject[] NextDevice;
        [SerializeField] private Switch LeftInputСontacts;
        [SerializeField] private Switch RightInputСontacts;
        [SerializeField] private int ID;
        [SerializeField] private string name;
        [SerializeField] private MeshRenderer leftMeshRenderer;
        [SerializeField] private MeshRenderer rightMeshRenderer;
        [SerializeField] private MeshRenderer outputMeshRenderer;
        [SerializeField] private bool isActiveDevise;
        [Inject] private MaterialConfig materialConfig;
        private bool isActiveDeviseForReboot;
        private bool checkElectricalSystemThisFrame;
        private MeshRenderer leftMeshRendererForReboot;
        private MeshRenderer rightMeshRendererForReboot;
        private MeshRenderer outputMeshRendererForReboot;
        private List<IDevice> deviceslist = new List<IDevice>();
        void Awake()
        {
            for (int i = 0; NextDevice.Length > i; i++)
                deviceslist.Add(NextDevice[i].GetComponent<IDevice>());
            isActiveDevise = (leftMeshRenderer.material == materialConfig.onMaterial ||
                             rightMeshRenderer.material == materialConfig.onMaterial) ? true : false;
            isActiveDeviseForReboot = isActiveDevise;
            leftMeshRendererForReboot = leftMeshRenderer;
            rightMeshRendererForReboot = rightMeshRenderer;
            outputMeshRendererForReboot = outputMeshRenderer;
        }
        private void Update()
        {
            checkElectricalSystemThisFrame = false;
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
            return TypeDevise.GateOR;
        }

        public bool GetActiv()
        {
            return isActiveDevise;
        }

        public void RebootDevice()
        {
            isActiveDevise = isActiveDeviseForReboot;
            leftMeshRenderer = leftMeshRendererForReboot;
            rightMeshRenderer = rightMeshRendererForReboot;
            outputMeshRenderer = outputMeshRendererForReboot;
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
            if (checkElectricalSystemThisFrame) return 0;
            checkElectricalSystemThisFrame = true;
            int result = 0;
            leftMeshRenderer.material = LeftInputСontacts.GetActiv() ? materialConfig.onMaterial : materialConfig.offMaterial;
            rightMeshRenderer.material = RightInputСontacts.GetActiv() ? materialConfig.onMaterial : materialConfig.offMaterial;
            isActiveDevise = (LeftInputСontacts.GetActiv() || RightInputСontacts.GetActiv()) ? true : false;
            outputMeshRenderer.materials[outputMeshRenderer.materials.Length - 1] = isActiveDevise ? materialConfig.onMaterial : materialConfig.offMaterial;
            for (int i = 0; deviceslist.Count > i; i++)
            {
                result += deviceslist[i].CheckElectricalSystem(isActiveDevise);
            }
            return result;
        }
    }
}