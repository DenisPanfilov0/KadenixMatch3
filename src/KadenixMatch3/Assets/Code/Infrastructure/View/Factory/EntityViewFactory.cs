using System.Threading.Tasks;
using Code.Infrastructure.AssetManagement;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.View.Factory
{
  public class EntityViewFactory : IEntityViewFactory
  {
    private readonly IAssetProvider _assetProvider;
    private readonly IInstantiator _instantiator;
    private readonly Vector3 _farAway = new(-999, 999, 0);

    public EntityViewFactory(IAssetProvider assetProvider, IInstantiator instantiator)
    {
      _assetProvider = assetProvider;
      _instantiator = instantiator;
    }
    
    public EntityBehaviour CreateViewForEntity(GameEntity entity)
    {
      EntityBehaviour viewPrefab = _assetProvider.LoadAsset<EntityBehaviour>(entity.ViewPath);
      EntityBehaviour view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(
        viewPrefab,
        position: _farAway,
        Quaternion.identity,
        parentTransform: null);
      
      view.SetEntity(entity);

      return view;
    }

    public EntityBehaviour CreateViewForEntityFromPrefab(GameEntity entity)
    {
      EntityBehaviour view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(
        entity.ViewPrefab,
        position: _farAway,
        Quaternion.identity,
        parentTransform: null);
      
      view.SetEntity(entity);

      return view;
    }
    
    public async Task<EntityBehaviour> CreateViewForEntityFromAddressPrefab(GameEntity entity)
    {
      GameObject prefab = await _assetProvider.Load<GameObject>(entity.ViewAddress);
      EntityBehaviour view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(
        prefab,
        position: entity.WorldPosition,
        // Quaternion.identity,
        prefab.transform.rotation, // Сохраняем угол поворота префаба
        parentTransform: null);

      view.SetEntity(entity);
      entity.isAddViewProcess = false;

      return view;
    }
  }
}