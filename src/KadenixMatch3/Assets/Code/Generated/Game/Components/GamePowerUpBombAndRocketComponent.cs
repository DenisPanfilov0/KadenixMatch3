//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherPowerUpBombAndRocket;

    public static Entitas.IMatcher<GameEntity> PowerUpBombAndRocket {
        get {
            if (_matcherPowerUpBombAndRocket == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PowerUpBombAndRocket);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPowerUpBombAndRocket = matcher;
            }

            return _matcherPowerUpBombAndRocket;
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

    static readonly Code.Gameplay.Features.ActiveInteractionFeature.PowerUpBombAndRocket powerUpBombAndRocketComponent = new Code.Gameplay.Features.ActiveInteractionFeature.PowerUpBombAndRocket();

    public bool isPowerUpBombAndRocket {
        get { return HasComponent(GameComponentsLookup.PowerUpBombAndRocket); }
        set {
            if (value != isPowerUpBombAndRocket) {
                var index = GameComponentsLookup.PowerUpBombAndRocket;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : powerUpBombAndRocketComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}
