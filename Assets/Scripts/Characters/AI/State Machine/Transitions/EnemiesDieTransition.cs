using UnityEngine;

public class EnemiesDieTransition : Transition
{
    private void Update()
    {
        if (Character.Spawner.CountEnemies == 0)
            NeedTransit = true;
    }
}
