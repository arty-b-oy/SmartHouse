using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.Static;
namespace Code.Devises
{
    public class LightInRoom : MonoBehaviour
    {
        [SerializeField] private GameObject LightInARoom;
        [SerializeField] private GameObject LightInBRoom;
        void Awake()
        {
            InformationBetweenScenes.LightRoom.Add(LightInARoom);
            InformationBetweenScenes.LightRoom.Add(LightInBRoom);
        }

    }
}