using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;


[CreateAssetMenu(fileName = "CoinMagnetData", menuName = "PowerUp/CoinMagnetData")]
public class CoinMagnetData : PowerUpData
{
    public float[] durations = new float[5];
    public float[] radiusList = new float[5];

    private float pullingForce = 10.0f;

    public override void Activate(int currentLevel)
    {
        if (currentLevel <= 0 || currentLevel > Constants.MAX_POWERUP_LEVEL)
        {
            Debug.LogError("Invalid Level");
            return;
        }

        Debug.Log("Activeate");
        float duration = durations[currentLevel-1];
        float radius = radiusList[currentLevel-1];

        GameplayManager.Instance.RunCoroutine(DetectAndPullCoin(radius, duration));


    
    }
    IEnumerator DetectAndPullCoin(float radius, float duration)
    {
        Transform characterTransform = GameplayManager.Instance.player.transform;
        float timer = duration;
        while(timer > 0)
        {
            if (GameplayManager.Instance.IsPlaying())
            {


                Collider[] nearbyCoinObj = Physics.OverlapSphere(characterTransform.position, radius);
                foreach (Collider collider in nearbyCoinObj)
                {
                    if (collider.CompareTag("Coin"))
                    {
                        GameplayManager.Instance.mapController.coinSpawner.FlagPulling(collider.gameObject);
                    }
                }

                timer -= Time.deltaTime;
            }
            
            yield return null;

        }

        
    }
}


