using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    [Header("UI")]
    public Button[] Squares;
    public Slider progressionBar;

    [Header("Progress Value Settings")]
    public float progressionDecreaseSpeed = 1.0f;
    public float increaseValue = 0.15f;
    public float decreaseValue = 0.07f;

    [Header("Events")]
    public UnityEvent onPuzzleSolved;
    
    private float progressionValueNewTarget;

    private int lastOddNo;
    int randomOddSquareNo = 1;

    private List<Color> colors;
    Color oddColour;
    Color evenColour;

    bool hasWon = false;
    bool changeProgressValue = false;

    // Start is called before the first frame update
    void Start()
    {
        colors = new List<Color>() { Color.red,  Color.green, Color.blue, Color.white, Color.yellow, Color.magenta };
        progressionValueNewTarget = progressionBar.value;
        
        LoadSquarePuzzle();
    }

    // Update is called once per frame
    void Update()
    {
        // Decrease slider value to zero if we are not actively changing the slider value, or if the slider value is not equal to one.
        if(progressionBar.value > 0 && !changeProgressValue && !hasWon)
        {
            progressionBar.value -= Time.deltaTime * progressionDecreaseSpeed / 10;
        }

        // If we are trying to change the value of the slider...
        if (changeProgressValue)
        {
            // If the slider value (rounded to 2dp) is not in an approximate range with the progressionValueNewTarget (slider value +/- new value, rounded to 2dp)...
            if(!Mathf.Approximately(Mathf.Round(progressionBar.value * 100f) / 100f, progressionValueNewTarget))
            {
                // Add / subtract by Time.deltaTime until slider value is approximately similar to target value.
                if (progressionBar.value < progressionValueNewTarget)
                {
                    progressionBar.value += Time.deltaTime * 2f;
                }
                else if (progressionBar.value > progressionValueNewTarget)
                {
                    progressionBar.value -= Time.deltaTime * 2f;
                }
            }
            else
            {
                // Stop updating this if-statement if target value and slider value are approximately similar.
                changeProgressValue = false;
                progressionValueNewTarget = progressionBar.value;
            }
        }

        // If the slider value is equal to one...
        if(progressionBar.value == 1 & !hasWon)
        {
            hasWon = true;
            Debug.Log("Solved!");
            onPuzzleSolved.Invoke();
        }
    }
    public void LoadSquarePuzzle()
    {     
        // Pick random even colour.
        evenColour = colors[Random.Range(0, colors.Count)];
        // Pick random odd colour.
        oddColour = colors[Random.Range(0, colors.Count)];

        // Keep picking a random odd colour until it is definitely different to the even colour.
        while (oddColour == evenColour)
        {
            oddColour = colors[Random.Range(0, colors.Count)];
        }      
        
        // Keep picking a random odd square number until it is definitely different to the last odd square number.
        while(randomOddSquareNo == lastOddNo)
        {
            randomOddSquareNo = Random.Range(0, Squares.Length);
        }

        lastOddNo = randomOddSquareNo; // Save odd number.

        Debug.Log(randomOddSquareNo);

        // Updates colours for odd square, and even squares.
        for(int i = 0; i < Squares.Length; i++)
        {
            if(i == randomOddSquareNo)
            {
                Squares[i].GetComponent<Image>().color = oddColour;
            }
            else
            {
                Squares[i].GetComponent<Image>().color = evenColour;
            }
        }
    }

    // This function is called on button press, returning the value of the button, to check if the correct odd button has been pressed,
    public void OnButtonPress(int buttonNo)
    {
        if(buttonNo == randomOddSquareNo)
        {
            Debug.Log("Correct!");
            ChangeProgressionValue("add", increaseValue);
        }
        else
        {
            Debug.Log("Incorrect!");
            ChangeProgressionValue("subtract", decreaseValue);
        }

        LoadSquarePuzzle();
    }

    // This function creates a target value for the slider, which it must increase/decrease over time to reach.
    public void ChangeProgressionValue(string operation, float value)
    {
        if(operation == "add")
        {
            progressionValueNewTarget = Mathf.Round((progressionBar.value + value) * 100f) / 100f;    
        }
        else if(operation == "subtract")
        {
            progressionValueNewTarget = Mathf.Round((progressionBar.value - value) * 100f) / 100f;
        }
        else
        {
            return; // Invalid operation.
        }

        changeProgressValue = true; // Enable if-statements in Update() for changing the slider value to target value.
    }



}
