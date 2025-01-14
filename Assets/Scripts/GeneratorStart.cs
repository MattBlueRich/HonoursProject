using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GeneratorStart : MonoBehaviour, IInteractable // Object is marked as interactable, and must use the interact function.
{
    public Canvas PuzzleCanvas;
    public GameBehaviour gameBehaviour; // Responsible for the puzzle canvas mechanics.
    bool isInteracting = false;
    bool canInteract = true;
    PlayerInteract player;

    private void Start()
    {
        PuzzleCanvas.gameObject.SetActive(false); // Hide canvas microgame by default.
    }

    public bool Enabled
    {
        get { return canInteract; }
    }

    // Called by PlayerInteract.cs when the player is attempting to interact with the object.
    public void Interact(PlayerInteract interactor)
    {
        if (canInteract)
        {
            // Assign the player object as the interactor ONCE.
            if (player == null)
            {
                player = interactor;
            }

            Debug.Log("Interacting with generator!"); 

            ToggleCanvas(); // Shows PuzzleCanvas when attempting to interact.
        }
    }
    private void Update()
    {
        // Hides PuzzleCanvas when any key is pressed interacting.
        if (Keyboard.current.anyKey.wasPressedThisFrame && isInteracting)
        {
            ToggleCanvas(); 
        }
    }

    public void ToggleCanvas()
    {
        isInteracting = !isInteracting; // Toggle boolean.

        if (isInteracting)
        {
            PuzzleCanvas.gameObject.SetActive(true); // Show canvas microgame.
            player.GetComponent<PlayerController>().canMove = false; // Disables player movement.
            player.GetComponent<PlayerController>().canLook = false; // Disables player aim movement.
            
            CursorManager.instance.SetCursorNormal(); // Switch cursor to normal cursor.
        }
        else
        {
            PuzzleCanvas.gameObject.SetActive(false); // Hides canvas microgame.
            player.GetComponent<PlayerController>().canMove = true; // Enables player movement.
            player.GetComponent<PlayerController>().canLook = true; // Enables player aim movement.

            // If the puzzle canvas game hasn't been solved and is therefore still interactable...
            if (!gameBehaviour.hasWon)
            {
                CursorManager.instance.SetCursorInteract(); // Switch cursor to interact cursor if generator hasn't been restored.
            }
        }
    }

    // When the generator is activated successfully, the player can no longer interact with it.
    public void DisableInteraction()
    {
        canInteract = false;
    }
}
