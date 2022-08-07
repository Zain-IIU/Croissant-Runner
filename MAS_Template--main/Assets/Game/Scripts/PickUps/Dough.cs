using System;
using System.Collections;
using DG.Tweening;
using MegaFiers;
using UnityEngine;

public class Dough : MonoBehaviour
{
    #region  Variables

    [SerializeField] private DoughState curState;
    
    [SerializeField] private bool isPartOfDoughs;
    [SerializeField] private   Transform targetToFollow;
    
    [SerializeField] private  float followSpeed;
    
    [SerializeField] private Vector3 offset;

    [SerializeField] private Animation doughAnimation;

    [SerializeField] private MegaStretch plainDoughModifier;
   
    [SerializeField] private Transform rolledDough;
    [SerializeField] private Transform doughTriangle;
    [SerializeField] private Transform croissant;
    
    [SerializeField] private MeshRenderer croissantModel;

     private Material croissantMat;
    [SerializeField] private Color color; 
    private static int _doughID;
    private int id;
    #endregion

    private void Start()
    {
        croissantMat = croissantModel.material;
    }

    #region Dough Part

  
    
    public void UpdateState(DoughState newState)
    {
        curState = newState;
        StateCondition();
    }

    public DoughState GETCurState() => curState;

    public bool IsPartOf()
    {
        return isPartOfDoughs;
    } 

    public void DoughRemoved(){
        isPartOfDoughs = false;
    } 

    public void MakePartOfDough()
    { 
        isPartOfDoughs = true;
       
    } 

    public void PlayDoughAnimation()
    {
        switch (curState)
        {
            case DoughState.InComplete:
                break;
            case DoughState.SimpleDough:
                doughAnimation.Play();
                break;
            case DoughState.RolledDough:
                rolledDough.DOScaleX(2, .05f).OnComplete(() =>
                {
                    rolledDough.DOScaleX(1, .05f);
                });
                break;
            case DoughState.DoughTriangle:
                doughTriangle.DOScaleX(2, .05f).OnComplete(() =>
                {
                    doughTriangle.DOScaleX(1, .05f);
                });
                break;
            case DoughState.UnBakedCroissant:
                croissant.DOScaleX(2, .05f).OnComplete(() =>
                {
                    croissant.DOScaleX(1, .05f);
                });
                break;
            case DoughState.BakedCroissant:
                croissant.DOScaleX(2, .05f).OnComplete(() =>
                {
                    croissant.DOScaleX(1, .05f);
                });
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }



    private void StateCondition()
    {
        switch (curState)
        {
            case DoughState.SimpleDough:
                break;
            case DoughState.RolledDough:
                RollingDough();
                break;
            case DoughState.DoughTriangle:
                DoughTriangles();
                break;
            case DoughState.UnBakedCroissant:
                Croissant();
                break;
            case DoughState.InComplete:
                break;
            case DoughState.BakedCroissant:
                BakeCroissant();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void DoughTriangles()
    {
        rolledDough.DOScaleX(0, .3f).OnComplete(() =>
        {
            rolledDough.gameObject.SetActive(false);
            doughTriangle.DOScale(Vector3.one, .25f);
            plainDoughModifier.gameObject.SetActive(false);
        });
    }

    private void RollingDough()
    {
        DOTween.To(() => plainDoughModifier.amount, x => plainDoughModifier.amount = x, .5f, .5f);
    }

    private void Croissant()
    {
        doughTriangle.DOScaleX(0, .3f).OnComplete(() =>
        {
            doughTriangle.gameObject.SetActive(false);
            croissant.DOScale(Vector3.one, .25f);
            plainDoughModifier.gameObject.SetActive(false);
            rolledDough.gameObject.SetActive(false);
        });
    }

    private void BakeCroissant()
    {
        croissantMat.color = color;
    }
    
    #endregion
    

    #region Follow Part

  

    // Update is called once per frame
    void Update()
    {
        if(targetToFollow)
            FollowTarget(targetToFollow);
        _doughID++;
        id = _doughID;
    }

    private void FollowTarget(Transform target)
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, followSpeed * Time.deltaTime);
    }


    public void UpdateTarget(Transform newTarget)
    {
        targetToFollow = newTarget;
    }

    public void UpdateSpeedAndOffset()
    {
        followSpeed = 30f;
        offset.z = 1;
        StartCoroutine(nameof(ResetValues));
    }

    IEnumerator  ResetValues()
    {
        yield return new WaitForSeconds(.5f);
        followSpeed = 15f;
        offset.z = 2;
    }
    #endregion

    public int GetID() => id;
}
