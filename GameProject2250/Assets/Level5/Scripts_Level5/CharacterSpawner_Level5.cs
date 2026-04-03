using UnityEngine;

public class CharacterSpawner_Level5 : MonoBehaviour
{
    [Header("Fairy Options")]
    [SerializeField] private GameObject fairyRedCharacter; // Reference to the red fairy character prefab
    [SerializeField] private GameObject fairyGreenCharacter; // Reference to the green fairy character prefab
    [SerializeField] private GameObject fairyOrangeCharacter; // Reference to the orange fairy character prefab

    [Header("Level 5 Camera")]
    [SerializeField] private CameraMovementLevel5 level5Camera;

    void Start()
    {
        // Deactivate all fairy characters at the start
        if (fairyRedCharacter != null) fairyRedCharacter.SetActive(false);
        if (fairyGreenCharacter != null) fairyGreenCharacter.SetActive(false);
        if (fairyOrangeCharacter != null) fairyOrangeCharacter.SetActive(false);

        GameObject selectedObject = null;

        if (GameManager.instance == null)
        {
            // If GameManager is not found, default to red fairy and log a warning
            Debug.LogWarning("GameManager.instance is null. Defaulting to red fairy.");

            if (fairyRedCharacter != null)
            {
                // Activate the red fairy character and set it as the selected object
                fairyRedCharacter.SetActive(true);
                selectedObject = fairyRedCharacter;
            }
        }
        else
        {
            // Determine which fairy to spawn based on the selected fairy in the GameManager
            switch (GameManager.instance.selectedFairy)
            {
                case "FairyR":
                    if (fairyRedCharacter != null)
                    {
                        fairyRedCharacter.SetActive(true);
                        selectedObject = fairyRedCharacter;
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
        }

        if (selectedObject != null)
        {
            // Set the tag of the selected object to "Player" so that it can be recognized by other scripts
            selectedObject.tag = "Player";

            if (level5Camera != null)
            {
                // Set the camera's target to the selected fairy character so that it follows the player
                level5Camera.SetTarget(selectedObject.transform);
            }

            Debug.Log("Level 5 spawned fairy: " + selectedObject.name);
        }
        else
        {
            // If no fairy was successfully spawned, log a warning to help with debugging
            Debug.LogWarning("Level 5 could not spawn a fairy. Check Inspector references.");
        }
    }
}