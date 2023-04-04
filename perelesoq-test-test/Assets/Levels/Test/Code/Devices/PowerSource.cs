using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Code.Abstract.Enume;
using Code.Abstract.Interface;
using Code.Static;
namespace Code.Devises
{
    public class PowerSource : MonoBehaviour, IDevice, IElectricitySource
    {
        [SerializeField] private GameObject[] NextDevice;
        [SerializeField] private HomeCamera[] cameras;
        [SerializeField] private int ID;
        [SerializeField] private string name;
        [SerializeField] private TextMeshPro panel;
        private TimeSpan time;
        private DateTime startingPointTime;
        private float TOTAL;
        private int CURRENT;
        private List<IDevice> deviceslist = new List<IDevice>();
        private const int secondInHours = 3600;
        void Start()
        {
            TOTAL = 0;
            CURRENT = 0;
            InformationBetweenScenes.powerSource = this;
            for (int i = 0; NextDevice.Length > i; i++)
                deviceslist.Add(NextDevice[i].GetComponent<IDevice>());
            CheckElectricalSystem(true);
            startingPointTime = DateTime.Now;

        }
        private void Update()
        {
            FixInformation();

        }
        public void FixInformation()
        {
            time = DateTime.Now.Subtract(startingPointTime);
            TOTAL += CURRENT * Time.deltaTime / secondInHours;
            panel.text = $"TIME: {time.Hours}h {time.Minutes}m {time.Seconds}s{'\n'}TOTAL: {Math.Round(TOTAL, 2)}W{'\n'}CURRENT: {CURRENT}w";
        }

        public void RebootDevice()
        {
            TOTAL = 0;
            CURRENT = 0;
            for (int i = 0; deviceslist.Count > i; i++)
                deviceslist[i].RebootDevice();
            CheckElectricalSystem(true);
            startingPointTime = DateTime.Now;
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
            return TypeDevise.PowerSourc;
        }

        public bool GetActiv()
        {
            return true;
        }

        public List<IDevice> GetThisAndNextDevise()
        {
            List<IDevice> list = new List<IDevice>();
            for (int i = 0; deviceslist.Count > i; i++)
            {
                list.AddRange(deviceslist[i].GetThisAndNextDevise());
            }
            return list.Distinct().ToList();
        }
        public Dictionary<int, bool> GetStateThisAndNextDevise()
        {
            Dictionary<int, bool> list = new Dictionary<int, bool>();
            Dictionary<int, bool> result = new Dictionary<int, bool>();
            for (int i = 0; deviceslist.Count > i; i++)
            {
                result = deviceslist[i].GetStateThisAndNextDevise();
                foreach (var element in result)
                {
                    if (!list.ContainsKey(element.Key))
                        list.Add(element.Key, element.Value);
                }
            }
            return list;
        }
        public string NextCamera()
        {
            RenderTexture renderTexture;
            int index = 0;
            for (int i = 0; cameras.Length > i; i++)
            {
                renderTexture = cameras[i].GetRenderTextureCamera();
                if (renderTexture != null)
                {
                    index = i;
                    cameras[i].SetRenderTextureCamera(null);
                    if (index + 1 >= cameras.Length)
                    {
                        cameras[0].SetRenderTextureCamera(renderTexture);
                        index = 0;
                    }
                    else
                    {
                        index++;
                        cameras[index].SetRenderTextureCamera(renderTexture);
                    }
                    break;
                }
            }
            return cameras[index].GetNameDevice();
        }

        public int ChangeStateDevise(int id)
        {
            int result = 0;
            for (int i = 0; deviceslist.Count > i; i++)
            {
                result += deviceslist[i].ChangeStateDevise(id);
            }
            CheckElectricalSystem(true);
            TOTAL += result;
            return 0;
        }

        public int CheckElectricalSystem(bool isCurrent)
        {
            int result = 0;
            for (int i = 0; deviceslist.Count > i; i++)
            {
                result += deviceslist[i].CheckElectricalSystem(true);
            }
            print(result);
            CURRENT = result;
            return 0;
        }

        public TimeSpan GetTimeSpan()
        {
            return time;
        }

        public int GetCurrent()
        {
            return CURRENT;
        }

        public float GetTotal()
        {
            return TOTAL;
        }
    }
}