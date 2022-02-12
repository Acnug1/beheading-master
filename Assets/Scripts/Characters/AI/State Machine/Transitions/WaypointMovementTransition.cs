using UnityEngine;

public class WaypointMovementTransition : Transition
{
    private void Update()
    {
        if (Character.Spawner.IsGameStarted)
            NeedTransit = true;
    }
}
