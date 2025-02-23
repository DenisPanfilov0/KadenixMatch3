//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherGoalAmount;

    public static Entitas.IMatcher<GameEntity> GoalAmount {
        get {
            if (_matcherGoalAmount == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GoalAmount);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGoalAmount = matcher;
            }

            return _matcherGoalAmount;
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

    public Code.Gameplay.Features.GoalsCounting.GoalAmount goalAmount { get { return (Code.Gameplay.Features.GoalsCounting.GoalAmount)GetComponent(GameComponentsLookup.GoalAmount); } }
    public int GoalAmount { get { return goalAmount.Value; } }
    public bool hasGoalAmount { get { return HasComponent(GameComponentsLookup.GoalAmount); } }

    public GameEntity AddGoalAmount(int newValue) {
        var index = GameComponentsLookup.GoalAmount;
        var component = (Code.Gameplay.Features.GoalsCounting.GoalAmount)CreateComponent(index, typeof(Code.Gameplay.Features.GoalsCounting.GoalAmount));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceGoalAmount(int newValue) {
        var index = GameComponentsLookup.GoalAmount;
        var component = (Code.Gameplay.Features.GoalsCounting.GoalAmount)CreateComponent(index, typeof(Code.Gameplay.Features.GoalsCounting.GoalAmount));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveGoalAmount() {
        RemoveComponent(GameComponentsLookup.GoalAmount);
        return this;
    }
}
