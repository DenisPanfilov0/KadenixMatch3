using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.AssetManagement
{
  public interface IAssetProvider
  {
    GameObject LoadAsset(string path);
    T LoadAsset<T>(string path) where T : Component;
    void Initialize();
    Task<T> Load<T>(AssetReference assetReference) where T : class;
    Task<T> Load<T>(string address) where T : class;
    Task<GameObject> Instantiate(string address);
    Task<GameObject> Instantiate(string address, Vector3 at);
    Task<GameObject> Instantiate(string address, Transform under);
    void CleanUp();
  }
}