using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Code.Abstract.Interface;

namespace Code.UI
{
    public class DoorDriverUI : MonoBehaviour, IDeviceUI
    {
        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private TextMeshProUGUI textState;
        [SerializeField] private TextMeshProUGUI textButtonState;
        [SerializeField] private Button button;
        private const string opened = "opened";
        private const string closed = "closed";
        private const string open = "OPEN";
        private const string close = "CLOSE";
        private int ID;
        private DeviceList deviceList;

        private void Awake()
        {
            button.onClick.AddListener(ChangeState);
        }
        private void ChangeState()
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
            textState.text = state ? closed : opened;
            textButtonState.text = state ? open : close;
        }
    }
}