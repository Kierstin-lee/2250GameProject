using UnityEngine;

public class CharacterSpawner_Level3 : MonoBehaviour
{
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;

    [SerializeField] private CameraFollowLevel3 level3Camera;

    void Start()
    {
        // Deactivate all fairy characters at the start
        if (fairyRedCharacter != null) fairyRedCharacter.SetActive(false);
        if (fairyGreenCharacter != null) fairyGreenCharacter.SetActive(false);
        if (fairyOrangeCharacter != null) fairyOrangeCharacter.SetActive(false);

        GameObject selectedObject = null;

        if (GameManager.instance == null)
        {
            // If GameManager is null, default to the red fairy and log a warning
            Debug.LogWarning("GameManager.instance is null. Defaulting to red fairy.");

            if (fairyRedCharacter != null)
            {
                fairyRedCharacter.SetActive(true); // Ensure the red fairy is active
                fairyRedCharacter.tag = "Player"; // Set the tag to Player

                if (level3Camera != null)
                {
                    level3Camera.SetTarget(fairyRedCharacter.transform); // Set the camera target to the red fairy
                }

                Debug.Log("Spawned default red fairy because GameManager was null.");
            }

            return;
        }

        // Spawn the selected fairy based on the GameManager's selectedFairy value
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

        if (selectedObject != null) // Ensure a fairy was selected and spawned
        {
            selectedObject.tag = "Player";

            if (level3Camera != null)
            {
                // Set the camera target to the selected fairy
                level3Camera.SetTarget(selectedObject.transform);
            }

            Debug.Log("Spawned fairy: " + selectedObject.name + " | tag set to Player");
        }
        else
        {
            // If no fairy was selected or spawned, log a warning
            Debug.LogWarning("No fairy was selected/spawned. Check your fairy references in the Inspector.");
        }
    }
}