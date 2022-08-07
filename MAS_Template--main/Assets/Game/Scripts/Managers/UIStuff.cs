using DG.Tweening;
using TMPro;
using UnityEngine;


public class UIStuff : MonoBehaviour
{
    public static UIStuff Instance;
    
    
    private void Awake()
    {
        Instance = this;
    }
    
    
    [SerializeField] private RectTransform mainPanel;
    [SerializeField] private RectTransform winPanel;
    [SerializeField] private RectTransform losePanel;

    [SerializeField] private TextMeshProUGUI coinsCount;

    private int curCoinsCount;
    
    [SerializeField] private RectTransform coinsBar;
    // Start is called before the first frame update
    void Start()
    {
        curCoinsCount = 0;
        coinsCount.text = "";
        EventsManager.ONGameStart += HideMainPanel;
        EventsManager.ONReachedEnd += GiveSomeCoins;
        EventsManager.ONCoinsPicked += IncrementCoinsCount;
        EventsManager.ONGameWin += EnableWinPanel;
        EventsManager.ONGameLose += EnableLosePanel;
    }


    private void GiveSomeCoins()
    {
        curCoinsCount = 10;
        coinsCount.text = curCoinsCount.ToString();
    }
    private void HideMainPanel()
    {
        mainPanel.DOScale(Vector2.zero, .25f);
    }

    private void IncrementCoinsCount()
    {
        curCoinsCount += 10;
        coinsCount.text = curCoinsCount.ToString();
    }

    private void EnableWinPanel()
    {
        winPanel.DOScale(Vector2.one, .25f);
    }
    private void EnableLosePanel()
    {
        losePanel.DOScale(Vector2.one, .25f);
    }
    
    
    public int GETCurCoinsCount() => curCoinsCount;

    private void OnDestroy()
    {
        EventsManager.ONGameStart -= HideMainPanel;
        EventsManager.ONCoinsPicked -= IncrementCoinsCount;
        EventsManager.ONGameWin -= EnableWinPanel;
        EventsManager.ONGameLose -= EnableLosePanel;
    }
}
