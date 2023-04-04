using UnityEngine;
using TMPro;
using Code.Abstract.Interface;

namespace Code.UI
{
    public class GateANDUI : MonoBehaviour, IDeviceUI
    {
        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private TextMeshProUGUI textState;
        private const string opened = "opened";
        private const string closed = "closed";
        private int ID;
        private DeviceList deviceList;

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
        public void SetState(bool state) => textState.text = state ? opened : closed;
    }
}