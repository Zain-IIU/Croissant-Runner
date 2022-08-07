using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class Mixer : MonoBehaviour
{
    
    [SerializeField] private GameObject mixingVFX;
    

    [SerializeField] private float endPos;
    [SerializeField] private float delay;
    private bool _startRotating;
    
    private void Update()
    {
        if(!_startRotating) return;
        var eulerAngles = transform.localEulerAngles;
        Quaternion newRotation = quaternion.Euler(0, eulerAngles.y+40f, 0);
        transform.localRotation=Quaternion.Lerp(transform.localRotation,newRotation,Time.deltaTime*30f);
    }

    public void CreateDoughFromMixture()
    {
        //if(!_fillings.AllIngredientsAdded()) return;
        EventsManager.ConvertedToDough();
        mixingVFX.SetActive(true);
        TweenHolder();
    }
    private void TweenHolder()
    {
        _startRotating = true;
        transform.DOLocalMoveY(endPos, delay);
    }
}
