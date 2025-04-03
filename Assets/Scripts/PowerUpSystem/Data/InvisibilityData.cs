using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[CreateAssetMenu(fileName= "InvisibilityData", menuName = "PowerUp/InvisibilityData")]
public class InvisibilityData : PowerUpData
{
    public float[] durations = new float[5];

    public override void Activate(int currentLevel)
    {
        if (currentLevel <= 0|| currentLevel > Constants.MAX_POWERUP_LEVEL)
        {
            Debug.LogError("Invalid level for Invisibility");
            return;
        }
        float duration = durations[currentLevel - 1];
        GameplayManager.Instance.RunCoroutine(ActivateInvisible(duration));

        
    }

    IEnumerator ActivateInvisible(float duration)
    {
        GameplayManager.Instance.ToggleInvisibleState(true);
        yield return new WaitForSeconds(duration);
        GameplayManager.Instance.ToggleInvisibleState(false);
    }
}
