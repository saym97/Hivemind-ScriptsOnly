using UnityEngine;

[CreateAssetMenu(fileName = "FSM_Action_BasicEnemy_Move",
    menuName = "Game Data/Finite State Machine/Action/Action/Basic Enemy Move")]
public class ActionBasicEnemyMove : FSMAction
{
   // private Vector3[] m_WayPoints;
    public WayPoints WayPoints;
    public override void Act(FiniteStateMachine fsm)
    {
        Enemy EnemyContext = (Enemy) fsm.Context;
        //if (m_WayPoints.Length == 0) m_WayPoints = EnemyContext.PatrolWayPoints.Waypoints;
        GoToNextPoint(EnemyContext);
    }

    void GoToNextPoint(Enemy Enemy)
    {
        if (WayPoints.Waypoints.Length == 0) return;
        if (Enemy.Agent.remainingDistance < 5f ||  !Enemy.Agent.hasPath)
            Enemy.Agent.SetDestination(WayPoints.Waypoints[Random.Range(0, WayPoints.Waypoints.Length)]);
    }
}