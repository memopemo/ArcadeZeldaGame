using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Camera camera;
    public float JUMP_FORCE;
    public const float SPEED = 12;
    bool hasWallHopped = false;
    Animator animator;
    Vector2 facingDirection;
    public float swingTimer; //time when enemies can be hit by attack.
    float dodgeCooldown; //time before dodging again
    float dodgeFrames; //invincibility when dodging (shorter than cooldown)
    [SerializeField] GameObject dashParticles;
    [SerializeField] GameObject dieParticle;
    AudioSource audioSource;
    [SerializeField] AudioClip dash;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = FindFirstObjectByType<Camera>();
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        audioSource = GetComponent<AudioSource>();

        rb.constraints = RigidbodyConstraints.FreezeAll; //disallow player to move until timer starts
        Invoke(nameof(UnlockPlayer), 2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //make it so that direction aligns to camera direction
        Vector2 playerPos = new(transform.position.x, transform.position.z);
        Vector2 cameraPos = new(camera.transform.position.x, camera.transform.position.z);
        Vector2 forwardDirectionRelativeToCamera = (playerPos - cameraPos).normalized;
        Vector2 sidewaysDirectionRelativeToCamera = new(forwardDirectionRelativeToCamera.y, -forwardDirectionRelativeToCamera.x);
        Vector2 moveDirection = forwardDirectionRelativeToCamera * Input.GetAxis("Vertical") + sidewaysDirectionRelativeToCamera * Input.GetAxis("Horizontal");

        moveDirection *= dodgeCooldown <= 0 ? 14 : SPEED;

        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.y);

        // update direction
        if (moveDirection != Vector2.zero)
        {
            facingDirection = new Vector2(rb.velocity.x, rb.velocity.z);
        }
    }
    void Update()
    {
        //collisions (a mess, as always)
        Physics2D.queriesStartInColliders = false; //do not detect ourself
        bool IsTouchingFloor = Physics.BoxCast(transform.position, new Vector3(0.4f, 0.4f, 0.4f), Vector3.down, Quaternion.identity, 1f);
        if (Input.GetKeyDown(KeyCode.Space) && IsTouchingFloor)
        {
            rb.velocity += Vector3.up * JUMP_FORCE;
        }
        bool IsTouchingWall = Physics.CheckBox(transform.position + Vector3.up * 0.1f, new Vector3(0.6f, 0.5f, 0.6f));

        //do a little hop if we bump into a wall once
        if (!hasWallHopped && IsTouchingWall && !IsTouchingFloor)
        {
            rb.velocity += Vector3.up * JUMP_FORCE / 2;
            hasWallHopped = true;
        }
        if (IsTouchingFloor)
        {
            hasWallHopped = false;
        }


        //update facing direction
        transform.eulerAngles = new Vector3(0, Vector2.SignedAngle(facingDirection, Vector2.right), 0);

        //buttons
        //attacking
        if (Input.GetButtonDown("Fire1"))
        {
            animator.Play("slash", -1, 0);
            transform.GetChild(0).localEulerAngles = new Vector3(90 + Random.Range(-45, 45), 0, 0); //for some flair
            swingTimer = 0.3f;
            audioSource.Play();
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            GetComponentInChildren<BoxCollider>().enabled = false;

        }

        //can only hit enemies if we are swinging
        GetComponentInChildren<BoxCollider>().enabled = swingTimer > 0;

        //dodging 
        if (Input.GetButtonDown("Fire2") && dodgeCooldown <= 0)
        {
            rb.velocity += Vector3.up * JUMP_FORCE / 2;
            rb.velocity = new Vector3(facingDirection.x, 0, facingDirection.y) * 2;
            Instantiate(dashParticles, transform.position, transform.rotation);
            dodgeCooldown = 0.5f;
            dodgeFrames = 0.25f;
            AudioSource.PlayClipAtPoint(dash, transform.position, 1);
        }

        //timers
        DecrementTimer(ref swingTimer);
        DecrementTimer(ref dodgeCooldown);
        DecrementTimer(ref dodgeFrames);

        //Failsafe death
        if (transform.position.y < -20)
        {
            Die();
        }

    }
    void DecrementTimer(ref float timerSecs)
    {
        if (timerSecs > 0)
        {
            timerSecs -= Time.deltaTime;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (dodgeFrames <= 0 && other.gameObject.TryGetComponent(out Enemy _))
        {
            Die();
        }
    }

    // Yes I know the camera unintentionally spins dizzyingly after dying, but i'll keep it in because its funny
    public void Die()
    {
        //do this when we die so the top score displays our current top score instead of the previous top score.
        if (ScoreGame.score > ScoreGame.top)
        {
            ScoreGame.top = ScoreGame.score;
        }

        //Bugfix: dying and winning at the same time allows the player to continue
        // this prevents the game from displaying the win screen if we die.
        FindFirstObjectByType<CompletionChecker>().completed = true;

        Instantiate(dieParticle, transform.position, transform.rotation);
        FindFirstObjectByType<GameOverScreen>(FindObjectsInactive.Include).gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None; //allow player to select restart.
        if (FindFirstObjectByType<Music>())
        {
            Destroy(FindFirstObjectByType<Music>().gameObject);
        }
        FindFirstObjectByType<TimerUI>().CancelInvoke(); //stop timer ticking
        Destroy(gameObject);
    }

    public void UnlockPlayer()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
