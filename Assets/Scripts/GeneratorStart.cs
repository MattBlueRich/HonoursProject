using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GeneratorStart : MonoBehaviour, IInteractable // Object is marked as interactable, and must use the interact function.
{

    public Canvas PuzzleCanvas;
    bool isInteracting = false;
    bool canInteract = true;
    PlayerInteract player;

    private void Start()
    {
        PuzzleCanvas.gameObject.SetActive(false); // Hide canvas microgame by default.
    }

    // Called by PlayerInteract.cs when the player is attempting to interact with the object.
    public void Interact(PlayerInteract interactor)
    {
        if (canInteract)
        {
            if (player == null)
            {
                player = interactor;
            }

            Debug.Log("Interacting with generator!");

            ToggleCanvas();
        }
    }
    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame && isInteracting)
        {
            ToggleCanvas();
        }
    }

    public void ToggleCanvas()
    {
        isInteracting = !isInteracting;

        if (isInteracting)
        {
            PuzzleCanvas.gameObject.SetActive(true); // Show canvas microgame.
            player.GetComponent<PlayerController>().canMove = false; // Disables player movement.
        }
        else
        {
            PuzzleCanvas.gameObject.SetActive(false); // Show canvas microgame.
            player.GetComponent<PlayerController>().canMove = true; // Disables player movement.
        }
    }

    public void DisableInteraction()
    {
        canInteract = false;
    }
}
