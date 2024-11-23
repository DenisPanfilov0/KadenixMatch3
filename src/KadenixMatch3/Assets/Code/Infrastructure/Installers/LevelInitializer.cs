using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class LevelInitializer : MonoBehaviour, IInitializable
  {
    public Camera MainCamera;
    public Transform StartPoint;

    [Inject]
    private void Construct()
    {
    }
    
    public void Initialize()
    {
    }
  }
}