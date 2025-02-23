//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherAddViewProcess;

    public static Entitas.IMatcher<GameEntity> AddViewProcess {
        get {
            if (_matcherAddViewProcess == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AddViewProcess);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAddViewProcess = matcher;
            }

            return _matcherAddViewProcess;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Code.Common.AddViewProcess addViewProcessComponent = new Code.Common.AddViewProcess();

    public bool isAddViewProcess {
        get { return HasComponent(GameComponentsLookup.AddViewProcess); }
        set {
            if (value != isAddViewProcess) {
                var index = GameComponentsLookup.AddViewProcess;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : addViewProcessComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}
