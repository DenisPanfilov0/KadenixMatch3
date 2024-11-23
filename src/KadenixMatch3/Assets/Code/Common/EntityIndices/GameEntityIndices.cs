using Zenject;

namespace Code.Common.EntityIndices
{
  public class GameEntityIndices : IInitializable
  {
    private readonly GameContext _game;

    public const string StatusesOfType = "StatusesOfType"; 
    public const string StatChanges = "StatChanges"; 

    public GameEntityIndices(GameContext game)
    {
      _game = game;
    }
    
    public void Initialize()
    {
    }

  }
}