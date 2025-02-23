//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherPowerUpMagicalBallAndPowerUp;

    public static Entitas.IMatcher<GameEntity> PowerUpMagicalBallAndPowerUp {
        get {
            if (_matcherPowerUpMagicalBallAndPowerUp == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PowerUpMagicalBallAndPowerUp);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPowerUpMagicalBallAndPowerUp = matcher;
            }

            return _matcherPowerUpMagicalBallAndPowerUp;
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

    public Code.Gameplay.Features.ActiveInteractionFeature.PowerUpMagicalBallAndPowerUp powerUpMagicalBallAndPowerUp { get { return (Code.Gameplay.Features.ActiveInteractionFeature.PowerUpMagicalBallAndPowerUp)GetComponent(GameComponentsLookup.PowerUpMagicalBallAndPowerUp); } }
    public Code.Gameplay.Features.BoardBuildFeature.TileTypeId PowerUpMagicalBallAndPowerUp { get { return powerUpMagicalBallAndPowerUp.Value; } }
    public bool hasPowerUpMagicalBallAndPowerUp { get { return HasComponent(GameComponentsLookup.PowerUpMagicalBallAndPowerUp); } }

    public GameEntity AddPowerUpMagicalBallAndPowerUp(Code.Gameplay.Features.BoardBuildFeature.TileTypeId newValue) {
        var index = GameComponentsLookup.PowerUpMagicalBallAndPowerUp;
        var component = (Code.Gameplay.Features.ActiveInteractionFeature.PowerUpMagicalBallAndPowerUp)CreateComponent(index, typeof(Code.Gameplay.Features.ActiveInteractionFeature.PowerUpMagicalBallAndPowerUp));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplacePowerUpMagicalBallAndPowerUp(Code.Gameplay.Features.BoardBuildFeature.TileTypeId newValue) {
        var index = GameComponentsLookup.PowerUpMagicalBallAndPowerUp;
        var component = (Code.Gameplay.Features.ActiveInteractionFeature.PowerUpMagicalBallAndPowerUp)CreateComponent(index, typeof(Code.Gameplay.Features.ActiveInteractionFeature.PowerUpMagicalBallAndPowerUp));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemovePowerUpMagicalBallAndPowerUp() {
        RemoveComponent(GameComponentsLookup.PowerUpMagicalBallAndPowerUp);
        return this;
    }
}
