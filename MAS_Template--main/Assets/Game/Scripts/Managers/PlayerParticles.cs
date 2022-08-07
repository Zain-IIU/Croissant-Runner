using System;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] private GameObject[] ingredientVfx;

    [SerializeField] private GameObject obstacleHitVfx;

    [SerializeField] private GameObject fillingsVfx;

    private void Start()
    {
        EventsManager.ONCollidedWithObstacle += PlayObstacleVFX;
    }

    public void PlayIngredientVFX() => ingredientVfx[Random.Range(0,ingredientVfx.Length)].SetActive(true);
    public void PlayObstacleVFX() => obstacleHitVfx.SetActive(true);
    public void PlayFillingsVFX() => fillingsVfx.SetActive(true);

    private void OnDestroy()
    {
        EventsManager.ONCollidedWithObstacle -= PlayObstacleVFX;
    }
}
