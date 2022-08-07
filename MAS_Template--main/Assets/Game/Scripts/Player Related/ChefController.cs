using DG.Tweening;
using UnityEngine;

public class ChefController : MonoBehaviour
{
    private Animator chefAnim; 
    private FloatingJoystick joystick;
    private float xVal;
    private float yVal;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float speed;
    private static readonly int XMove = Animator.StringToHash("xMove");
    private static readonly int YMove = Animator.StringToHash("yMove");
    private Vector3 _input;
    private Rigidbody rb;
    private bool startMoving;
    private static readonly int Win = Animator.StringToHash("Win");


    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<FloatingJoystick>();
        rb = GetComponent<Rigidbody>();
        chefAnim = GetComponent<Animator>();
        EventsManager.ONReachedEnd += StartMovement;
        EventsManager.ONCollisionNextLevel += NextLevelAction;
    }


    #region Extra Stuff

    void Update()
    {
        if (!startMoving) return;
       
        GatherInput();
        Look();
        Animate();
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {
        _input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;
        var rot= Quaternion.LookRotation(_input, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
    }
    private void Animate()
    {
        chefAnim.SetFloat(XMove,joystick.Horizontal);
        chefAnim.SetFloat(YMove,joystick.Vertical);
    }

    private void Move()
    {
        var transform1 = transform;
        rb.MovePosition(transform1.position + transform1.forward * _input.normalized.magnitude * speed * Time.deltaTime);
    }

   
    private void StartMovement()
    {
        startMoving = true;
    }




    #endregion
    // Update is called once per frame
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Multiplier multiplier))
        {
            if (multiplier.value == UIStuff.Instance.GETCurCoinsCount())
            {
                chefAnim.SetBool(Win,true);
                EventsManager.GameWin();
            }
        }
    }

    private void NextLevelAction()
    {
        enabled = false;
        chefAnim.SetFloat(XMove,0);
        chefAnim.SetFloat(YMove,0);
        transform.DOLocalRotate(new Vector3(0, 180, 0), 0).OnComplete(() =>
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        });
        startMoving = false;
    }
    private void OnDestroy()
    {
        EventsManager.ONReachedEnd -= StartMovement;
        EventsManager.ONCollisionNextLevel -= NextLevelAction;
    }
}
