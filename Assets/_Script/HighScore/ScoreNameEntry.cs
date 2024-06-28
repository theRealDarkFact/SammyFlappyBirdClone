using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows;

public class ScoreNameEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreValueText, NameValueText;
    [SerializeField] private GameObject HSTable;
    [SerializeField] Button HSTableSelectButton;
    string characterToAdd = string.Empty;
    string tmpString = string.Empty;

    private void Start()
    {
        NameValueText.text = string.Empty;
    }

    public void OnClicked(Button button) 
    {
        switch (button.name) 
        {
            case "A Button":
                characterToAdd = "A";
                break;
            case "B Button":
                characterToAdd = "B";
                break;
            case "C Button":
                characterToAdd = "C";
                break;
            case "D Button":
                characterToAdd = "D";
                break;
            case "E Button":
                characterToAdd = "E";
                break;
            case "F Button":
                characterToAdd = "F";
                break;
            case "G Button":
                characterToAdd = "G";
                break;
            case "H Button":
                characterToAdd = "H";
                break;
            case "I Button":
                characterToAdd = "I";
                break;
            case "J Button":
                characterToAdd = "J";
                break;
            case "K Button":
                characterToAdd = "K";
                break;
            case "L Button":
                characterToAdd = "L";
                break;
            case "M Button":
                characterToAdd = "M";
                break;
            case "N Button":
                characterToAdd = "N";
                break;
            case "O Button":
                characterToAdd = "O";
                break;
            case "P Button":
                characterToAdd = "P";
                break;
            case "Q Button":
                characterToAdd = "Q";
                break;
            case "R Button":
                characterToAdd = "R";
                break;
            case "S Button":
                characterToAdd = "S";
                break;
            case "T Button":
                characterToAdd = "T";
                break;
            case "U Button":
                characterToAdd = "U";
                break;
            case "V Button":
                characterToAdd = "V";
                break;
            case "W Button":
                characterToAdd = "W";
                break;
            case "X Button":
                characterToAdd = "X";
                break;
            case "Y Button":
                characterToAdd = "Y";
                break;
            case "Z Button":
                characterToAdd = "Z";
                break;
            case "Del Button":
                characterToAdd = "DEL";
                break;
        }

        if (characterToAdd != "DEL")
        {
            tmpString += characterToAdd;
        }
        else if (characterToAdd == "DEL" && tmpString.Length > 0)
        {
            if(tmpString.Length == 1) 
            {
                tmpString = string.Empty;
            }
            else 
            {
                tmpString = tmpString.Remove(tmpString.Length - 1);
            }
            NameValueText.text = tmpString;
            return;
        }
        if (tmpString.Length > 3) tmpString = string.Empty;
        NameValueText.text = tmpString;
        
    }

    public void SubmitScore() 
    {
        //Let's make sure no errored information gets into the integer we want to store.
        //since this is being converted from string we should be mindful.
        try
        {
            //temp variable result to parse the string into an integer
            int result = Int32.Parse(scoreValueText.text);
            //if all goes well save through the add high score entry
            HSTable.SetActive(true);
            HSTable.GetComponent<HighScoreTable>().AddHighscoreEntry(result, NameValueText.text);
            HSTable.GetComponent<HighScoreTable>().RefreshTable();
            HSTableSelectButton.Select();
            gameObject.SetActive(false);
        }
        catch (FormatException)
        {
            //if it fails debug log the issue.
            Console.WriteLine($"Unable to parse '{scoreValueText.text}'");
        }

    }
}
