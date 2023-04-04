using UnityEngine;
using Zenject;
using Code.Configs;

namespace Code.Installer
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private ScenesConfig _scenesConfig;
        [SerializeField] private DeviceConfig _deviceConfig;
        [SerializeField] private MaterialConfig _materialConfig;
        public override void InstallBindings()
        {
            Container.Bind<ScenesConfig>().FromInstance(_scenesConfig).AsSingle();
            Container.Bind<DeviceConfig>().FromInstance(_deviceConfig).AsSingle();
            Container.Bind<MaterialConfig>().FromInstance(_materialConfig).AsSingle();
        }
    }
}