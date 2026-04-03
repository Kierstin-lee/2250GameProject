using UnityEngine;

public class CharacterSpawner_Level1 : MonoBehaviour
{
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;

    void Start()
    {
        // Turn all fairies off first
        if (fairyRedCharacter != null) fairyRedCharacter.SetActive(false);
        if (fairyGreenCharacter != null) fairyGreenCharacter.SetActive(false);
        if (fairyOrangeCharacter != null) fairyOrangeCharacter.SetActive(false);

        GameObject selectedObject = null;

        // Fairy spawner logic
        if (GameManager.instance == null)
        {
            Debug.LogWarning("GameManager.instance is null. Defaulting to red fairy.");

            if (fairyRedCharacter != null)
            {
                // If GameManager is missing, default to red fairy
                fairyRedCharacter.SetActive(true); // Ensure the red fairy is active
                fairyRedCharacter.tag = "Player"; // Set the tag to Player for the red fairy
                Debug.Log("Spawned default red fairy because GameManager was null.");
            }

            return;
        }

        switch (GameManager.instance.selectedFairy) // Check the selected fairy from GameManager
        {
            case "FairyR":
                if (fairyRedCharacter != null) // Check if the red fairy GameObject is assigned
                {
                    fairyRedCharacter.SetActive(true); // Activate the red fairy GameObject
                    selectedObject = fairyRedCharacter; // Store the reference to the selected red fairy for later use
                }
                break;

            case "FairyG":
                if (fairyGreenCharacter != null)
                {
                    fairyGreenCharacter.SetActive(true);
                    selectedObject = fairyGreenCharacter;
                }
                break;

            case "FairyO":
                if (fairyOrangeCharacter != null)
                {
                    fairyOrangeCharacter.SetActive(true);
                    selectedObject = fairyOrangeCharacter;
                }
                break;

            default:
                if (fairyRedCharacter != null)
                {
                    fairyRedCharacter.SetActive(true);
                    selectedObject = fairyRedCharacter;
                }
                break;
        }

        if (selectedObject != null)
        {
            selectedObject.tag = "Player";
            Debug.Log("Level 1 spawned fairy: " + selectedObject.name + " | tag set to Player"); // Log the name of the spawned fairy for debugging
        }
        else
        {
            Debug.LogWarning("No fairy was selected/spawned for Level 1."); // Log a warning if no fairy was spawned
        }
    }
}