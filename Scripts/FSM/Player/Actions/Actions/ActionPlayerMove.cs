using UnityEngine;

[CreateAssetMenu(fileName = "FSM_Action_Player_Move", menuName = "Game Data/Finite State Machine/Action/Action/Player Move")]

public class ActionPlayerMove : FSMAction
{
    public override void Act(FiniteStateMachine fsm)
    {
        var context = (PlayerContext)fsm.Context;

        Vector2 input = context.MovementInput;

        Vector3 movement = Vector3.zero;

        movement.x = (input.x + input.y);
        movement.z = ((movement.x / 2 - input.x) * 2);

        movement.Normalize();
        context.transform.parent.rotation = Quaternion.LookRotation(movement);

        context.CharacterController.SimpleMove(movement * context.MovementSpeed);
    }
}
