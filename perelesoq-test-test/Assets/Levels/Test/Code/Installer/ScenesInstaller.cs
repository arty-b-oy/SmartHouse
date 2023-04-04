using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Code.Configs;

namespace Code.Installer
{
    public class ScenesInstaller : MonoInstaller
    {
        [Inject] private ScenesConfig scenesConfig;
        public override void InstallBindings()
        {
            for (int i = 0; scenesConfig.nameScenes.Length > i; i++)
                SceneManager.LoadScene(scenesConfig.nameScenes[i], LoadSceneMode.Additive);
        }
    }
}