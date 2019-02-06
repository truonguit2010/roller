using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnDieEvent : UnityEvent {}

[DisallowMultipleComponent]
public class Character : MonoBehaviour {

    private readonly string TAG = typeof(Character).Name;

    private bool jump = false;

    [SerializeField]
    private float moveForce = 200f;
    [SerializeField]
    private float maxSpeed = 1f;
    [SerializeField]
    private float jumpForce = 300f;
    [SerializeField]
    private Transform firstGroundCheck;
    [SerializeField]
    private Transform secondGroundCheck;

    private Animator animator;
    private Rigidbody2D rigidbody2D;


    [Header("Call when character die.")]
    [SerializeField]
    private OnDieEvent onDieEvent;

    private Transform myTransform;

    // Use this for initialization
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        myTransform = this.transform;

        animator.Play("RunAnim");
    }

    void FixedUpdate()
    {
        bool grounded = Physics2D.Linecast(firstGroundCheck.position, secondGroundCheck.position);
        if (jump && grounded)
        {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    public void doJump() {
        jump = true;
    }

    public void startGame() {
        animator.Play("RunAnim");
    }

    public void endGame() {
        animator.Play("Die");
    }

    public void pauseGame() {
        animator.Play("StandAnim");
    }

    public void resumeGame() {
        animator.Play("RunAnim");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
#if DEBUG
            Debug.LogFormat("{0} - OnTriggerEnter2D with a obstacle.", TAG);
#endif
            onDieEvent.Invoke();
        }
    }
}
