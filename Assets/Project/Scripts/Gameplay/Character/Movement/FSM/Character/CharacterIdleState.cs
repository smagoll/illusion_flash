using UnityEngine;

public class CharacterIdleState : CharacterState
{
    public CharacterIdleState(CharacterStateMachine characterStateMachine) : base(characterStateMachine) { }

    public override void Enter()
    {
        Debug.Log("Character entered Idle");
    }

    public override void Update()
    {
        
    }
}