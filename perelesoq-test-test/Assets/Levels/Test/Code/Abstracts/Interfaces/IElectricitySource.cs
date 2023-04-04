using System;
namespace Code.Abstract.Interface
{
    public interface IElectricitySource
    {
        void FixInformation();
        string NextCamera();
        TimeSpan GetTimeSpan();
        int GetCurrent();
        float GetTotal();
    }
}