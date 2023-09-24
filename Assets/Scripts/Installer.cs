using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Installer : MonoInstaller
{
    [SerializeField] private GameObject _playingField;
    [SerializeField] private Transform _pos;
    public override void InstallBindings()
    {
        Container.Bind<ResourceManager>().AsSingle();
        Container.Bind<ScoreManager>().AsSingle();
        var playingFieldTransform = Container.InstantiatePrefabForComponent<SpawnObjects>(_playingField, _pos.position, Quaternion.identity, null);
        Container.Bind<SpawnObjects>().FromInstance(playingFieldTransform).AsSingle();
        Debug.Log("»нстал€ци€ закончилась!");
    }

}