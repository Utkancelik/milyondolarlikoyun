using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberGame : MonoBehaviour
{
    public TMP_Text[] numberDisplay;
    public TMP_InputField[] answerFields;
    public TMP_Text resultDisplay;

    private int[] numbers;

    void Start()
    {
        GenerateNumbers();
    }

    private void Update()
    {
        bool isFull = true;
        for (int i = 0; i < 5; i++)
        {
            if(numberDisplay[i].ToString().Length == 0)
            {
                isFull = false;
                break;
            }
        }
        if (isFull)
        {

            CheckAnswers();
        }
    }

    public void CheckAnswers()
    {
        // Get the user's answers
        int[] answers = new int[5];
        for (int i = 0; i < 5; i++)
        {
            int.TryParse(answerFields[i].text, out answers[i]);
        }

        // Check the answers
        bool allCorrect = true;
        for (int i = 0; i < 5; i++)
        {
            if (answers[i] != numbers[i])
            {
                allCorrect = false;
                break;
            }
        }

        // Display the result
        if (allCorrect)
        {
            resultDisplay.text = "You win!";
        }
        else
        {
            resultDisplay.text = "Try again!";
        }
    }

    public void TryAgain()
    {
        for (int i = 0; i < 5; i++)
        {
            answerFields[i].text = "";
        }
        GenerateNumbers();
    }

    public void GenerateNumbers()
    {
        // Generate 5 random numbers between 1 and 10
        numbers = new int[5];
        for (int i = 0; i < 5; i++)
        {
            numbers[i] = Random.Range(1, 11);
        }

        // Display the numbers on the screen
        for (int i = 0; i < 5; i++)
        {
            numberDisplay[i].transform.parent.gameObject.SetActive(true);
            numberDisplay[i].text = numbers[i].ToString();
        }

        Invoke("DisableNumbers", 0.7f);
    }

    private void DisableNumbers()
    {
        // Not Display the numbers on the screen
        for (int i = 0; i < 5; i++)
        {
            numberDisplay[i].transform.parent.gameObject.SetActive(false);
            
        }
    }
}
