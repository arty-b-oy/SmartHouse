using System.Collections.Generic;
using UnityEngine;
using Code.Abstract.Enume;
using Code.Abstract.Interface;
namespace Code.Devises
{
    public class HomeCamera : MonoBehaviour, IDevice
    {
        [SerializeField] private int ID;
        [SerializeField] private string name;
        [SerializeField] private Camera camera;
        void Awake()
        {

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
            return TypeDevise.HomeCamera;
        }

        public void RebootDevice()
        {
        }

        public List<IDevice> GetThisAndNextDevise()
        {
            List<IDevice> list = new List<IDevice>();
            return list;
        }
        public Dictionary<int, bool> GetStateThisAndNextDevise()
        {
            Dictionary<int, bool> list = new Dictionary<int, bool>();
            return list;
        }

        public RenderTexture GetRenderTextureCamera()
        {
            return camera.targetTexture;
        }
        public void SetRenderTextureCamera(RenderTexture renderTexture)
        {
            camera.targetTexture = renderTexture;
        }

        public int ChangeStateDevise(int id)
        {
            return 0;
        }

        public int CheckElectricalSystem(bool isCurrent)
        {
            return 0;
        }

        public bool GetActiv()
        {
            return true;
        }
    }
}