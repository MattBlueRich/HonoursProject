using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/* 3rd Person Controller - Unity's New Input System by One Wheel Studio:
 * https://www.youtube.com/watch?v=WIl6ysorTE0
 */

public class PlayerController : MonoBehaviour
{
    // Input Fields
    private PlayerCharacterIA playerIA; // PlayerCharacterIA is an automated C# script form of the Input Action Component.
    private InputAction move;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool canLook = true;

    // Movement Fields
    private Rigidbody rb;
    private float movementForce = 1.0f;

    [Header("Movement Properties")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float maxSpeed = 5.0f;
    [SerializeField] private float gravityForce = 2.0f;

    private Vector3 forceDirection = Vector3.zero; // The forceDirection is the sum of all player character movement.

    [Header("Assign Camera")]
    [SerializeField] private Camera playerCam;

    [Header("Assign Ground")]
    [SerializeField] private LayerMask groundMask;

    [Header("Cursor To Camera Properties")]
    public GameObject cursorObj;
    public float cameraCursorMaxDistance = 5.0f;

    [HideInInspector] public bool disableAllMovement = false;

    private void Awake()
    {
        cursorObj.transform.position = transform.position;
        rb = GetComponent<Rigidbody>();
        playerIA = new PlayerCharacterIA();

        disableAllMovement = false;
    }

    // These region functions only enable the use of Input Actions, when the player character is also enabled.

    #region Input Action Setup
    private void OnEnable()
    {
        playerIA.Player.Jump.started += DoJump;
        move = playerIA.Player.Move;
        playerIA.Player.Enable();
    }
    private void OnDisable()
    {
        playerIA.Player.Jump.started -= DoJump;
        move = playerIA.Player.Move;
        playerIA.Player.Disable();
    }
    #endregion

    private void FixedUpdate()
    {
        if (!disableAllMovement)
        {
            // Moving

            if (canMove)
            {
                forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCam) * movementForce; // Add force in x-direction, in relation to the horizontal view of the camera.
                forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCam) * movementForce; // Add force in z-direction, in relation to the forward view of the camera.

                rb.AddForce(forceDirection, ForceMode.Impulse); // Applies player character movement.

                forceDirection = Vector3.zero; // Disables fall-off acceleration.

                // Moving - Velocity Capping

                Vector3 horizontalVelocity = rb.velocity;
                horizontalVelocity.y = 0f;
                if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
                {
                    rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
                }

                // Jumping = Reduce "floatiness" from falling

                if (rb.velocity.y < 0f) // Is the player character currently falling?
                {
                    rb.velocity -= Vector3.down * gravityForce * Physics.gravity.y * Time.fixedDeltaTime; // Increase acceleration as the player character falls.
                }
            }

            if (canLook)
            {
                LookAt();
            }
        }
    }

    // These region functions move the player character in the desired direction, in relation to the camera angle (great for Isometric games!).

    #region Fix Movement To Camera Angle
    private Vector3 GetCameraRight(Camera playerCam)
    {
        Vector3 right = playerCam.transform.right;
        right.y = 0;

        return right.normalized; // Returns normalized right direction of camera, without length.
    }

    private Vector3 GetCameraForward(Camera playerCam)
    {
        Vector3 forward = playerCam.transform.forward;
        forward.y = 0;

        return forward.normalized; // Returns normalized forward direction of camera, without length.
    }
    #endregion

    private void DoJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            forceDirection += Vector3.up * jumpForce; // Adds an upward force.
        }
    }

    private void LookAt()
    {
        var (success, position) = GetMousePosition(); // Retrieves both success mouse hit location information.

        if (success)
        {
            Vector3 mouseDir = position - transform.position; // Finds direction between the player and the mouse location.

            mouseDir.y = 0f; // Set height of y-direction as constant.

            Quaternion targetDir = Quaternion.LookRotation(mouseDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetDir, 5f * Time.deltaTime); // Makes the player character look in the desired direction of the cursor.
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        Ray ray = playerCam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundMask))
        {
            RestrictCursorMovement(hitInfo.point); // Affect camera track by mouse position.

            // Success: The raycast from the camera to the cursor has hit an object, and therefore it can now use this hit point position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // Failure: the raycast from the camera to the cursor did not hit an object, and therefore it cannot return a hit point location.
            return (success: false, position: Vector3.zero);
        }
    }

    private bool IsGrounded()
    {
        // Casts ray below the player character.
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.down * 1.25f));
        
        // Return true if the ray hits an object of any layer beneath the player.
        if(Physics.Raycast(ray, out RaycastHit hit, 1.25f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void RestrictCursorMovement(Vector3 hitPoint)
    {
        var currentPos = hitPoint; // Cursor position.
        var newPos = transform.position + Vector3.ClampMagnitude(currentPos - transform.position, cameraCursorMaxDistance);
        cursorObj.transform.position = newPos;   
    }
}
