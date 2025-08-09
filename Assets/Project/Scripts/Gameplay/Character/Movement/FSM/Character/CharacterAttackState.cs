using UnityEngine;

public class CharacterAttackState : CharacterState
{

    public CharacterAttackState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
        
    }

    public override void Enter()
    {
        Debug.Log("Character attack");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("attack exit");
    }
}