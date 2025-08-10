using UnityEngine;

public class CharacterDeathState : CharacterState
{
    public CharacterDeathState(CharacterStateMachine characterStateMachine) : base(characterStateMachine) { }

    public override void Enter()
    {
        Debug.Log("Character entered Death");
    }

    public override void Update()
    {
        
    }
}