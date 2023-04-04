using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Code.Abstract.Interface;

namespace Code.UI
{
    public class SwitchUI : MonoBehaviour, IDeviceUI
    {
        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private TextMeshProUGUI textState;
        [SerializeField] private Toggle toggle;
        private int ID;
        private DeviceList deviceList;
        private const string on = "on";
        private const string off = "off";
        private void Start()
        {
            toggle.onValueChanged.AddListener(delegate
            {
                ChangeSwitcher(toggle);
            });
        }
        private void ChangeSwitcher(Toggle toggle)
        {
            deviceList.ChangeStateDevise(ID);
        }
        public void SetName(string name) => textName.text = name;

        public void SetID(int id) => ID = id;
        public int GetID()
        {
            return ID;
        }
        public void SetDeviceList(DeviceList _deviceList) => deviceList = _deviceList;
        public void SetActivDevise(bool isActiv)
        {

        }

        public void SetState(bool state)
        {
            toggle.isOn = state;
            textState.text = state ? on : off;
        }
    }
}