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
        float timer = duration;
        while (timer > 0)
        {
            if (GameplayManager.Instance.IsPlaying())
            {
                timer -= Time.deltaTime;
            }
            
            yield return null;
        }
        GameplayManager.Instance.ToggleInvisibleState(false);
    }
}
