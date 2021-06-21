using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "RoomEvents" , menuName = "Game Data/Room Events" )]
public class RoomEvents : ScriptableObject
{
   public UnityAction RoomEnter;
   public UnityAction RoomExit;

   public void TriggerRoomEnterEvent() => RoomEnter.Invoke();
   public void TriggerRoomExitEvent() => RoomExit.Invoke();
}
