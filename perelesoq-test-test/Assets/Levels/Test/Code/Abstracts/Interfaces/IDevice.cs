using System.Collections.Generic;
using Code.Abstract.Enume;

namespace Code.Abstract.Interface
{
    public interface IDevice
    {
        void RebootDevice();
        int GetIDDevice();
        string GetNameDevice();
        TypeDevise GetTypeDevise();
        List<IDevice> GetThisAndNextDevise();
        Dictionary<int, bool> GetStateThisAndNextDevise();
        int ChangeStateDevise(int ID);
        int CheckElectricalSystem(bool isCurrent);
        bool GetActiv();
    }
}