using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridGame : MonoBehaviour
{
    [SerializeField] private TMP_Text instructionsText;
    [SerializeField] private Button[,] gridButtons;
    private bool isPlaying;
    private int numPressed;
    private int greenPressed;
    private List<int> pressIndices;
    private List<int> greenIndices;

    void Start()
    {
        isPlaying = true;
        numPressed = 0;
        pressIndices = new List<int>();
        greenIndices = new List<int>();

        GameObject[] objects = GameObject.FindGameObjectsWithTag("ButtonObject");
        // Get references to all the grid buttons
        gridButtons = new Button[5, 5];
        int count = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                gridButtons[i, j] = objects[count].GetComponent<Button>();
                count++;
            }
        }



        // Choose 5 random green buttons and light them up
        for (int i = 0; i < 5; i++)
        {
            int randomX = Random.Range(0, 5);
            int randomY = Random.Range(0, 5);
            gridButtons[randomX, randomY].image.color = Color.green;
            greenIndices.Add(randomX * 5 + randomY);
        }

        Invoke("setGrey", 1f);


        instructionsText.text = "You have 5 choices.";
    }

    void Update()
    {
        Debug.Log(numPressed);
        // Check for button clicks only when playing
        if (isPlaying)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (gridButtons[i, j].IsInteractable() && Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        numPressed++;
                      
                        if (greenIndices.Contains(5*i + j))
                        {
                            greenIndices.Remove(7);
                            greenPressed++;
                        }
                    }
                }
            }

            if (numPressed == 125)
            {
                isPlaying = false;
                if (greenPressed >= 125)
                {
                    instructionsText.text = "Won!";
                }
                else
                {
                    instructionsText.text = "Try again.";
                }
            }
        }
    }

    // Helper function to compare two lists of integers
    bool ListsAreEqual(List<int> list1, List<int> list2)
    {
        if (list1.Count != list2.Count)
        {
            return false;
        }
        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i])
            {
                return false;
            }
        }
        return true;
    }

    public void setGrey()
    {
        // Set all buttons to gray
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                gridButtons[i, j].image.color = Color.gray;
            }
        }
    }

    public void TryAgain()
    {
        Invoke("Start", 0.1f);
    }
}

