using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Code.Abstract.Interface;

namespace Code.UI
{
    public class LampUI : MonoBehaviour, IDeviceUI
    {
        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private TextMeshProUGUI textState;
        private int ID;
        private DeviceList deviceList;
        private const string on = "on";
        private const string off = "off";
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
            textState.text = state ? on : off;
        }
    }
}