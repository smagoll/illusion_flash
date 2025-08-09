using UnityEngine;

public class CharacterStunState : CharacterState
{

    public CharacterStunState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
        
    }

    public override void Enter()
    {
        Debug.Log("Character stunned");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("Stun ended");
    }
}