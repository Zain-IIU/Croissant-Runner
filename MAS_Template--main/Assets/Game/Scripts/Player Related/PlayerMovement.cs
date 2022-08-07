using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     [SerializeField] private float moveSpeed;

     
    [SerializeField] private float clampValue;

    [SerializeField] private float curSpeed;
    [SerializeField] private float horizontalSpeed;
    
    private float _yRot;

    [SerializeField] private bool hasReachedEnd;
    // Start is called before the first frame update
    void Start()
    {
        curSpeed = 0;
        EventsManager.ONGameStart += StartPlayer;
        EventsManager.ONCollidedWithObstacle += PlayerStepsBack;
        EventsManager.ONReachedEnd += StopPlayer;
        EventsManager.ONConvertedToDough += SlowPlayer;
        
    }

    #region Event Callbacks

    private void StartPlayer()
    {
        curSpeed = moveSpeed;
    }

    private void StopPlayer()
    {
        curSpeed = 0;
        horizontalSpeed = 0;
    }
    private void PlayerStepsBack()
    {
        curSpeed = 0;
        transform.DOMoveZ(transform.position.z - 3, .5f).OnComplete(() =>
        {
            curSpeed = moveSpeed;
        });

    }

    private void SlowPlayer()
    {
        curSpeed /= 4;
        StartCoroutine(nameof(NormalSpeed));
    }
    #endregion


    // Update is called once per frame
    void Update()
    { 
        Movement();
    }

    private void Movement()
    { 
        if(hasReachedEnd) return;
        
        transform.Translate(transform.forward * (curSpeed *Time.deltaTime));

        if (Input.GetMouseButtonDown(0)) return;
        
        
        if(Input.GetMouseButton(0))
        {
            var xMove = Input.GetAxis("Mouse X") * horizontalSpeed * Time.deltaTime;
            transform.Translate(transform.right * xMove);
        }
        ClampXAxis(clampValue);
    }

   
    private void ClampXAxis(float value)
    {
        var position = transform.position;
        position = new Vector3(Mathf.Clamp(position.x, -value, value),
            position.y, position.z);
        transform.position = position;
    }

    IEnumerator NormalSpeed()
    {
        yield return new WaitForSeconds(.5f);
        curSpeed = moveSpeed;
    }
    private void OnDestroy()
    {
        EventsManager.ONGameStart -= StartPlayer;
        EventsManager.ONCollidedWithObstacle -= PlayerStepsBack;
        EventsManager.ONReachedEnd -= StopPlayer;
        EventsManager.ONConvertedToDough -= SlowPlayer;
    }

   
}
