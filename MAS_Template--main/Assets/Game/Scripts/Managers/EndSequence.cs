using DG.Tweening;
using UnityEngine;


public class EndSequence : MonoBehaviour
{
    [SerializeField] private GameObject coinStacker;
    [SerializeField] private Transform stackPoint;

    [SerializeField] private GameObject confettiVFX;
    [SerializeField] private GameObject happyVFX;
    [SerializeField] private Transform chef;
    [SerializeField] private Animator receptionist;
    [SerializeField] private int itemsToSpawn;
    [SerializeField] private Animator[] npcAnimators;
    
    [SerializeField] private float stackPointSpeed;
    private float curSpeed;
    private static readonly int Win = Animator.StringToHash("Win");
    private static readonly int Happy = Animator.StringToHash("Happy");

    private void Start()
    {
        SpawnCoinStack();
        curSpeed = 0;
        EventsManager.ONCollisionNextLevel += ReTransformChef;
        EventsManager.ONGameWin += StopStackPoint;
        EventsManager.ONDoughPutToCounter += HappyReceptionist;
        EventsManager.ONGameWin += EnableWinConfetti;
        EventsManager.ONDoughPutToTable += EnableHappyVFX;
    }

    private void Update()
    {
       stackPoint.Translate(Vector3.up* (curSpeed*Time.deltaTime));
    }

    private void HappyReceptionist()
    {
        receptionist.SetTrigger(Win);
    }
    private void SpawnCoinStack()
    {
        confettiVFX.SetActive(false);
        float yPos = 0;
        for (var i = 0; i < itemsToSpawn; i++)
        {
            GameObject seat = Instantiate(coinStacker, stackPoint, true);
            seat.transform.DOLocalMove(new Vector3(0, yPos, 0), 0);
            yPos += 0.27f;
        }
    }

    private void EnableWinConfetti()
    {
        confettiVFX.SetActive(true);
    }

    private void EnableHappyVFX()
    {
        happyVFX.SetActive(true);
        foreach (var npc in npcAnimators)
        {
            npc.SetTrigger(Happy);
        }
    }

    private void ReTransformChef()
    {
        chef.transform.parent = stackPoint;
        chef.DOLocalMove(new Vector3(0, 1.33f, 0), 0);
        chef.DOLocalRotate(new Vector3(0, 180, 0), 0).OnComplete(StartStackPoint);
    }

  
    
    private void StopStackPoint()
    {
        curSpeed = 0;
    }

    private void StartStackPoint()
    {
        curSpeed = stackPointSpeed;
    }

    private void OnDestroy()
    {
        EventsManager.ONCollisionNextLevel -= ReTransformChef;
        EventsManager.ONGameWin -= StopStackPoint;
        EventsManager.ONDoughPutToCounter -= HappyReceptionist;
        EventsManager.ONGameWin -= EnableWinConfetti;
        EventsManager.ONDoughPutToTable -= EnableHappyVFX;
    }
}
