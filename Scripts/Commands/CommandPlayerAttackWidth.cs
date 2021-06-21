using QFSW.QC;
using UnityEngine;

public class CommandBasicAttackWidth : MonoBehaviour
{
    [Command("player-attack-width", description: "Get or set the player's basic attack width")]
    private static float AttackWidth
    {
        
        get => FindObjectOfType<PlayerContext>().BasicAttackData.Width;
        set
        {
            FindObjectOfType<PlayerContext>().BasicAttackData.Width = value;
            var debug = FindObjectOfType<DebugPlayerAttackVisualization>();

            if (debug.Debug) // destroying the mesh and recreating it
            {
                debug.DebugMode(false);
                debug.DebugMode(true);
            }
        }
    }
}