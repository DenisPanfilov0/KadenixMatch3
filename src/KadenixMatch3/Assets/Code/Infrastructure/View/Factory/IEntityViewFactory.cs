using System.Threading.Tasks;

namespace Code.Infrastructure.View.Factory
{
  public interface IEntityViewFactory
  {
    EntityBehaviour CreateViewForEntity(GameEntity entity);
    EntityBehaviour CreateViewForEntityFromPrefab(GameEntity entity);
    Task<EntityBehaviour> CreateViewForEntityFromAddressPrefab(GameEntity entity);
  }
}