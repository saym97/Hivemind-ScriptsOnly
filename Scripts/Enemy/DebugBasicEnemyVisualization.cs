using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DebugBasicEnemyVisualization : MonoBehaviour
{
    [SerializeField] private bool LiveView;
    public bool _Debug;
    public WayPoints PatrolWayPoints;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (LiveView) DrawWayPoints();
    }

    private void OnDrawGizmosSelected()
    {
        if (!LiveView) DrawWayPoints();
    }

    void DrawWayPoints()
    {
        if (_Debug && PatrolWayPoints.Waypoints.Length > 0)
        {
            GUIStyle Style = new GUIStyle();
            Style.fontSize = 20;
            Style.fontStyle = FontStyle.Bold;
            Style.normal.textColor = Color.green;
            Gizmos.color = Color.yellow;
            
            int count = 0;
            foreach (Vector3 waypoint in PatrolWayPoints.Waypoints)
            {
                Gizmos.DrawWireSphere(waypoint,1);
                
                Handles.Label(waypoint,count.ToString(),Style);
                
                count++;
            }

            count = 0;
            Gizmos.color = Color.white;

        }
    }
#endif
    
}
