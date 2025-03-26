using System.Collections;
using UnityEngine;

public class PlayerMovemenet : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform attackCentre;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float checkDistance = 1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer playerSprite;

    private float horizontalInput;
    private Vector2 moveVector;
    private bool isGrounded;
    private bool isAttacking;
    private const float attackDuration = 0.750f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("speed", Mathf.Abs(horizontalInput));

        //Rotate when moving left
        if (horizontalInput < -0.1f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (horizontalInput > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        //Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForceY(jumpSpeed,ForceMode2D.Impulse);
        }

        //Attack
        if (!isAttacking && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }

        moveVector = new Vector2(horizontalInput * horizontalSpeed, rb.linearVelocityY);
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkDistance, groundLayer);

        rb.linearVelocity = moveVector;
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("melee");
        isAttacking = true;

        Collider2D other = Physics2D.OverlapCircle(attackCentre.position, attackRadius, enemyLayer);
        if (other != null)
        {
            other.GetComponent<IDamagable>()?.TakeDamage();
        }

        //Make sure player cant attac while attack anim is already playing
        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
    }

}
