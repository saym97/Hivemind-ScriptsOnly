using System;
using UnityEngine;
using QFSW.QC;

public class CommandDebugMode : MonoBehaviour
{
    public static event Action<bool> OnDebugMode;

    [Command("debug-mode", description: "Enables all debugging aspects of the game." +
        " Individual debug aspects can be toggled on and off freely." +
        " This command enables or disables all debug commands, regardless of their current status.")]
    private static void DebugMode(bool mode) 
    {
        OnDebugMode?.Invoke(mode);
    }
}
