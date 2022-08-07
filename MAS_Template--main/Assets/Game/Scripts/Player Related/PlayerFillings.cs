using DG.Tweening;
using StylizedWaterShader;
using UnityEngine;

public class PlayerFillings : MonoBehaviour
{
    public static PlayerFillings Instance;

    [SerializeField] private Transform waterObj;

    [SerializeField] private bool waterAdded;
    [SerializeField] private bool saltAdded;
    [SerializeField] private bool sugarAdded;
    [SerializeField] private bool yeastAdded;
    [SerializeField] private bool flourAdded;

    [SerializeField] private Transform doughMixer;
    [SerializeField] private GameObject dough;
    [SerializeField] private MeshRenderer waterRenderer;
    [SerializeField] private Color sugarColor;
    [SerializeField] private Color saltColor;
    [SerializeField] private Color flourColor;
    [SerializeField] private Color yeastColor;
    [SerializeField] private Color waterColor;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EventsManager.ONConvertedToDough += TweenDoughMixer;
    }

    public void FillWithWater()
    {
        waterObj.DOScale(Vector3.one, .35f).OnComplete(() =>
        {
            waterAdded = true;
            waterRenderer.material.DOColor(waterColor, .25f);
        });
        
    }
    public void AddSugar()
    {
        sugarAdded = true;
        waterRenderer.material.DOColor(sugarColor, .5f);
    }
    public void AddSalt()
    {
        saltAdded = true;
        waterRenderer.material.DOColor(saltColor, .5f);
    }
    public void AddYeast()
    {
        yeastAdded = true;
        waterRenderer.material.DOColor(yeastColor, .5f);
    }
    
    public void AddFlour()
    {
        flourAdded = true;
        waterRenderer.material.DOColor(flourColor, .5f);
    }



    public bool AllIngredientsAdded()
    {
        return waterAdded && (saltAdded || sugarAdded || yeastAdded || flourAdded);
    }
    

    #region  Event Callbacks

    private void TweenDoughMixer()
    {
        doughMixer.DOScaleY(0, .15f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            doughMixer.gameObject.SetActive(false);

            dough.SetActive(true);
        });
    }

    #endregion


    private void OnDestroy()
    {
        EventsManager.ONConvertedToDough -= TweenDoughMixer;
    }
}
