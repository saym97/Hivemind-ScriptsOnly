using QFSW.QC;
using UnityEngine;

public class CommandPlayerAttackRange : MonoBehaviour
{
    [Command("player-attack-range", description: "Get or set the player's basic attack range")]
    private static float AttackRange
    {

        get => FindObjectOfType<PlayerContext>().BasicAttackData.Range;
        set
        {
            FindObjectOfType<PlayerContext>().BasicAttackData.Range = value;
            var debug = FindObjectOfType<DebugPlayerAttackVisualization>();

            if (debug.Debug) // destroying the mesh and recreating it
            {
                debug.DebugMode(false);
                debug.DebugMode(true);
            }
        }
    }
}
