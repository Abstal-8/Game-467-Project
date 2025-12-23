using UnityEngine;
using TMPro;

public class NumAdjuster : MonoBehaviour
{
    private TextMeshProUGUI number;

    public void Increase(GameObject parent) {
        // Pull references from the parent object
        TextMeshProUGUI userInput = parent.GetComponentInChildren<TextMeshProUGUI>();
        int number;

        if (userInput != null)
        {
            // Try to parse the text into a number
            if (int.TryParse(userInput.text, out number))
            {
                number++; // Transform the number
            }
            else
            {
                Debug.LogError("Invalid input! Not a number.");
                number = 0; // default value if parsing fails
            }

            userInput.text = number.ToString();
        }
        else {
            Debug.LogError("No TextMeshProUGUI found in parent!");
        }
    }

    public void Decrease(GameObject parent) {
        // Pull references from the parent object
        TextMeshProUGUI userInput = parent.GetComponentInChildren<TextMeshProUGUI>();
        int number;

        if (userInput)
        {
            // Try to parse the text into a number
            if (int.TryParse(userInput.text, out number))
            {
                number--; // Transform the number
            }
            else
            {
                Debug.LogError("Invalid input! Not a number.");
                number = 0; // default value if parsing fails
            }

            userInput.text = number.ToString();
        }
        else {
            Debug.LogError("No TextMeshProUGUI found in parent!");
            return;
        }
    }
    public int GetNum(GameObject parent) {
        // Pull references from the parent object
        TextMeshProUGUI userInput = parent.GetComponentInChildren<TextMeshProUGUI>();
        int number;

        if (userInput)
        {
            // Try to parse the text into a number
            if (int.TryParse(userInput.text, out number))
            {
                return number;
            }
            else
            {
                Debug.LogError("Invalid input! Not a number.");
                number = 0; // default value if parsing fails
                return number;
            }
        }
        else {
            Debug.LogError("No TextMeshProUGUI found in parent!");
            return 0;
        }
    }
}