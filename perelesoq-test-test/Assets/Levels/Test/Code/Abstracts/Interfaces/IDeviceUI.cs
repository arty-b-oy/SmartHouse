using UnityEngine;
using Code.UI;

namespace Code.Abstract.Interface
{
    public interface IDeviceUI
    {
        void SetName(string name);
        void SetID(int id);
        int GetID();
        void SetDeviceList(DeviceList deviceList);
        void SetActivDevise(bool isActiv);
        void SetState(bool state);
    }
}