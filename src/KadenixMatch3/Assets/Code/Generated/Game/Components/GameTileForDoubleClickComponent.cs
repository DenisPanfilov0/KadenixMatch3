//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherTileForDoubleClick;

    public static Entitas.IMatcher<GameEntity> TileForDoubleClick {
        get {
            if (_matcherTileForDoubleClick == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TileForDoubleClick);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTileForDoubleClick = matcher;
            }

            return _matcherTileForDoubleClick;
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

    public GameEntity tileForDoubleClickEntity { get { return GetGroup(GameMatcher.TileForDoubleClick).GetSingleEntity(); } }

    public bool isTileForDoubleClick {
        get { return tileForDoubleClickEntity != null; }
        set {
            var entity = tileForDoubleClickEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isTileForDoubleClick = true;
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

    static readonly Code.Gameplay.Features.Input.TileForDoubleClick tileForDoubleClickComponent = new Code.Gameplay.Features.Input.TileForDoubleClick();

    public bool isTileForDoubleClick {
        get { return HasComponent(GameComponentsLookup.TileForDoubleClick); }
        set {
            if (value != isTileForDoubleClick) {
                var index = GameComponentsLookup.TileForDoubleClick;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : tileForDoubleClickComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}
