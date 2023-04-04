using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Code.Abstract.Enume;
using Code.Abstract.Interface;
using Code.Devises;
using Code.Static;

namespace Code.UI
{
    public class DeviceList : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        [SerializeField] private GameObject cameraUI;
        [SerializeField] private GameObject blockDoorUI;
        [SerializeField] private GameObject gateANDUI;
        [SerializeField] private GameObject gateORUI;
        [SerializeField] private GameObject lampUI;
        [SerializeField] private GameObject switcherUI;
        [SerializeField] private TextMeshProUGUI textTime;
        [SerializeField] private TextMeshProUGUI textCurrent;
        [SerializeField] private TextMeshProUGUI textTotal;
        private CameraUI cameraUIScript;
        private PowerSource powerSource;
        private List<IDeviceUI> deviceUIList = new List<IDeviceUI>();

        void Start()
        {
            powerSource = InformationBetweenScenes.powerSource;
            LoadUI();
            RefreshUI();
        }
        private void Update()
        {
            TimeSpan time = powerSource.GetTimeSpan();
            textTime.text = $"{time.Days}d {time.Hours}h {time.Minutes}m {time.Seconds}s";
            textTotal.text = $"TOTAL: {Math.Round(powerSource.GetTotal(), 2)}W";
            textCurrent.text = $"CURRENT: {powerSource.GetCurrent()}W";
        }
        private void LoadUI()
        {
            List<IDevice> deviceslist = new List<IDevice>();
            deviceslist = powerSource.GetThisAndNextDevise();
            GameObject prefab;

            prefab = Instantiate(cameraUI, content.transform);
            cameraUIScript = prefab.GetComponent<CameraUI>();
            cameraUIScript.SetAction(NextCamera);

            for (int i = 0; deviceslist.Count > i; i++)
            {
                switch (deviceslist[i].GetTypeDevise())
                {
                    case TypeDevise.Switcher:
                        prefab = Instantiate(switcherUI, content.transform);
                        deviceUIList.Add(prefab.GetComponent<IDeviceUI>());
                        deviceUIList[deviceUIList.Count - 1].SetName(deviceslist[i].GetNameDevice());
                        deviceUIList[deviceUIList.Count - 1].SetID(deviceslist[i].GetIDDevice());
                        deviceUIList[deviceUIList.Count - 1].SetDeviceList(this);
                        break;
                    case TypeDevise.Lamp:
                        prefab = Instantiate(lampUI, content.transform);
                        deviceUIList.Add(prefab.GetComponent<IDeviceUI>());
                        deviceUIList[deviceUIList.Count - 1].SetName(deviceslist[i].GetNameDevice());
                        deviceUIList[deviceUIList.Count - 1].SetID(deviceslist[i].GetIDDevice());
                        deviceUIList[deviceUIList.Count - 1].SetDeviceList(this);
                        break;
                    case TypeDevise.GateOR:
                        prefab = Instantiate(gateORUI, content.transform);
                        deviceUIList.Add(prefab.GetComponent<IDeviceUI>());
                        deviceUIList[deviceUIList.Count - 1].SetName(deviceslist[i].GetNameDevice());
                        deviceUIList[deviceUIList.Count - 1].SetID(deviceslist[i].GetIDDevice());
                        deviceUIList[deviceUIList.Count - 1].SetDeviceList(this);
                        break;
                    case TypeDevise.GateAND:
                        prefab = Instantiate(gateANDUI, content.transform);
                        deviceUIList.Add(prefab.GetComponent<IDeviceUI>());
                        deviceUIList[deviceUIList.Count - 1].SetName(deviceslist[i].GetNameDevice());
                        deviceUIList[deviceUIList.Count - 1].SetID(deviceslist[i].GetIDDevice());
                        deviceUIList[deviceUIList.Count - 1].SetDeviceList(this);
                        break;
                    case TypeDevise.DoorDriver:
                        prefab = Instantiate(blockDoorUI, content.transform);
                        deviceUIList.Add(prefab.GetComponent<IDeviceUI>());
                        deviceUIList[deviceUIList.Count - 1].SetName(deviceslist[i].GetNameDevice());
                        deviceUIList[deviceUIList.Count - 1].SetID(deviceslist[i].GetIDDevice());
                        deviceUIList[deviceUIList.Count - 1].SetDeviceList(this);
                        break;
                }
            }

        }
        private void NextCamera()
        {
            cameraUIScript.SetName(powerSource.NextCamera());
        }
        public void ChangeStateDevise(int ID)
        {
            powerSource.ChangeStateDevise(ID);
            RefreshUI();
        }
        private void RefreshUI()
        {
            Dictionary<int, bool> list = new Dictionary<int, bool>();
            list = powerSource.GetStateThisAndNextDevise();
            foreach (var element in list)
            {
                for (int i = 0; deviceUIList.Count > i; i++)
                {
                    if (deviceUIList[i].GetID() == element.Key)
                    {
                        deviceUIList[i].SetState(element.Value);
                        break;
                    }
                }

            }


        }

        public void RebootDevice()
        {

        }
    }
}