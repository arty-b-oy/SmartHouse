using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Code.Abstract.Interface;

namespace Code.UI
{
    public class CameraUI : MonoBehaviour, IDeviceUI
    {

        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private Button button;
        private int ID;
        private DeviceList deviceList;
        public void SetName(string name) => textName.text = name;

        public void SetID(int id) => ID = id;

        public void SetAction(UnityAction action) => button.onClick.AddListener(action);

        public void SetDeviceList(DeviceList _deviceList) => deviceList = _deviceList;
        public void SetActivDevise(bool isActiv)
        {

        }

        public int GetID()
        {
            return ID;
        }
        public void SetState(bool state)
        {

        }
    }
}