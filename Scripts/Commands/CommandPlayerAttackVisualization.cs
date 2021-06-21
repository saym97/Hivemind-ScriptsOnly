using QFSW.QC;
using UnityEngine;

public class CommandPlayerAttackVisualization : MonoBehaviour
{
    [Command("debug-player-attack", description:"Enables a preview mesh of the players current attacks")]
    private static void DebugAttack(bool mode)
    {
        var attack = FindObjectOfType<DebugPlayerAttackVisualization>();

        if(attack) 
        {
            attack.DebugMode(mode);
        }
    }
}
