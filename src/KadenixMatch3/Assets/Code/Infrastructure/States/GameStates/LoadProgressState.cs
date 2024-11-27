using System.Collections.Generic;
using System.IO;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.StaticData;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Serialization;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.Data;
using Code.Progress.Provider;
using Code.Progress.SaveLoad;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadProgressState : IState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly IStaticDataService _staticDataService;
    private readonly IProgressProvider _progressProvider;
    private readonly IAssetProvider _assetProvider;
    private readonly ISaveLoadService _saveLoadService;

    public LoadProgressState(
      IGameStateMachine stateMachine,
      ISaveLoadService saveLoadService,
      IStaticDataService staticDataService,
      IProgressProvider progressProvider, 
      IAssetProvider assetProvider)
    {
      _saveLoadService = saveLoadService;
      _stateMachine = stateMachine;
      _staticDataService = staticDataService;
      _progressProvider = progressProvider;
      _assetProvider = assetProvider;
    }

    public void Enter()
    {
      InitializeProgress();
      
      _stateMachine.Enter<LoadingHomeScreenState>();
    }

    private void InitializeProgress()
    {
      // if (_saveLoadService.HasSavedProgress)
      //   _saveLoadService.LoadProgress();
      // else
        CreateNewProgress();
        LoadLevels();
    }

    private void CreateNewProgress()
    {
      _saveLoadService.CreateProgress();
    }
    
    private void LoadLevels()
    {
      string levelsFilePath = Path.Combine("Assets/Resources/Gameplay/", "Levels.json");
      string json = File.ReadAllText(levelsFilePath);

      _progressProvider.ProgressData.ProgressModel = new ProgressModel();
      
      LevelsData levels = JsonSerialization.FromJson<LevelsData>(json);

      foreach (var level in levels.Levels)
      {
        _progressProvider.ProgressData.ProgressModel.Levels.Add(level);
      }
    }

    public void Exit()
    {
    }
  }
}