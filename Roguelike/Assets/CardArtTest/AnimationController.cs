using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator main;
    public Animator legs;

    public float speed = 5f;

    private Vector2 moveInput;
    private Vector2 shootInput;

    private int moveFacing = 1;

    private bool isMoving;
    private bool isShooting;

    // Stores last valid shoot direction
    private Vector2 lockedShootDirection = Vector2.right;

    void Update()
    {
        HandleMovementInput();
        HandleShootingInput();
        ApplyMovement();
        HandleFacing();

        if (!isMoving && isShooting)
        {
            legs.SetBool("Shooting", true);
        }
        else
        {
            legs.SetBool("Shooting", false);
        }
    }

    void HandleMovementInput()
    {
        moveInput = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) moveInput.y += 1;
        if (Input.GetKey(KeyCode.S)) moveInput.y -= 1;

        if (Input.GetKey(KeyCode.D))
        {
            moveInput.x += 1;
            moveFacing = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveInput.x -= 1;
            moveFacing = -1;
        }

        moveInput.Normalize();

        bool movingNow = moveInput.magnitude > 0;
        if (movingNow != isMoving)
        {
            isMoving = movingNow;
            legs.SetBool("Moving", isMoving);
        }
    }

    void HandleShootingInput()
    {
        shootInput = Vector2.zero;

        if (Input.GetKey(KeyCode.RightArrow)) shootInput.x = 1;
        else if (Input.GetKey(KeyCode.LeftArrow)) shootInput.x = -1;

        if (Input.GetKey(KeyCode.UpArrow)) shootInput.y = 1;
        else if (Input.GetKey(KeyCode.DownArrow)) shootInput.y = -1;

        bool shootingNow = shootInput != Vector2.zero;

        if (shootingNow != isShooting)
        {
            isShooting = shootingNow;
            main.SetBool("Shooting", isShooting);
        }

        if (isShooting)
        {
            // PRIORITIZE vertical if pressed
            if (shootInput.y != 0)
            {
                lockedShootDirection = new Vector2(0, shootInput.y);
            }
            else if (shootInput.x != 0)
            {
                lockedShootDirection = new Vector2(shootInput.x, 0);
            }

            main.SetFloat("ShootX", lockedShootDirection.x);
            main.SetFloat("ShootY", lockedShootDirection.y);
        }
    }

    void ApplyMovement()
    {
        transform.position += (Vector3)(moveInput * speed * Time.deltaTime);
    }

    void HandleFacing()
    {
        legs.transform.localScale = new Vector3(moveFacing, 1, 1);

        if (!isShooting)
        {
            main.transform.localScale = new Vector3(moveFacing, 1, 1);
        }
        else
        {
            if (lockedShootDirection.x != 0)
            {
                main.transform.localScale =
                    new Vector3(Mathf.Sign(lockedShootDirection.x), 1, 1);
            }
        }
    }
}