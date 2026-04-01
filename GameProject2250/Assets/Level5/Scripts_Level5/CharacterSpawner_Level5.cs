using UnityEngine;

public class CharacterSpawner_Level5 : MonoBehaviour
{
    [Header("Fairy Options")]
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;

    [Header("Level 5 Camera")]
    [SerializeField] private CameraMovementLevel5 level5Camera;

    void Start()
    {
        if (fairyRedCharacter != null) fairyRedCharacter.SetActive(false);
        if (fairyGreenCharacter != null) fairyGreenCharacter.SetActive(false);
        if (fairyOrangeCharacter != null) fairyOrangeCharacter.SetActive(false);

        GameObject selectedObject = null;

        if (GameManager.instance == null)
        {
            Debug.LogWarning("GameManager.instance is null. Defaulting to red fairy.");

            if (fairyRedCharacter != null)
            {
                fairyRedCharacter.SetActive(true);
                selectedObject = fairyRedCharacter;
            }
        }
        else
        {
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
            selectedObject.tag = "Player";

            if (level5Camera != null)
            {
                level5Camera.SetTarget(selectedObject.transform);
            }

            Debug.Log("Level 5 spawned fairy: " + selectedObject.name);
        }
        else
        {
            Debug.LogWarning("Level 5 could not spawn a fairy. Check Inspector references.");
        }
    }
}