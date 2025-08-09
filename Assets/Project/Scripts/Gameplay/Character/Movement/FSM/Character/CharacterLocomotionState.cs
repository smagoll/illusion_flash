using UnityEngine;

public class CharacterLocomotionState : CharacterState
{

    public CharacterLocomotionState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
        
    }

    public override void Enter()
    {
        Debug.Log("Character locomotion");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("locomotion ended");
    }
}