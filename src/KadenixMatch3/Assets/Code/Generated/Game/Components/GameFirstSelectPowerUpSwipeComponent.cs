//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherFirstSelectPowerUpSwipe;

    public static Entitas.IMatcher<GameEntity> FirstSelectPowerUpSwipe {
        get {
            if (_matcherFirstSelectPowerUpSwipe == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.FirstSelectPowerUpSwipe);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherFirstSelectPowerUpSwipe = matcher;
            }

            return _matcherFirstSelectPowerUpSwipe;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity firstSelectPowerUpSwipeEntity { get { return GetGroup(GameMatcher.FirstSelectPowerUpSwipe).GetSingleEntity(); } }

    public bool isFirstSelectPowerUpSwipe {
        get { return firstSelectPowerUpSwipeEntity != null; }
        set {
            var entity = firstSelectPowerUpSwipeEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isFirstSelectPowerUpSwipe = true;
                } else {
                    entity.Destroy();
                }
            }
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

    static readonly Code.Gameplay.Features.Input.FirstSelectPowerUpSwipe firstSelectPowerUpSwipeComponent = new Code.Gameplay.Features.Input.FirstSelectPowerUpSwipe();

    public bool isFirstSelectPowerUpSwipe {
        get { return HasComponent(GameComponentsLookup.FirstSelectPowerUpSwipe); }
        set {
            if (value != isFirstSelectPowerUpSwipe) {
                var index = GameComponentsLookup.FirstSelectPowerUpSwipe;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : firstSelectPowerUpSwipeComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}
