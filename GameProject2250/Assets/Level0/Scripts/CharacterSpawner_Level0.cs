using UnityEngine;

public class CharacterSpawner_Level0 : MonoBehaviour
{
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;

    [SerializeField] private CameraMovement_Level0 cameraFollow;
    [SerializeField] private Transform playerSpawn;

    void Start()
    {
        fairyRedCharacter.SetActive(false);
        fairyGreenCharacter.SetActive(false);
        fairyOrangeCharacter.SetActive(false);

        GameObject selectedObject = null;

        switch (GameManager.instance.selectedFairy)
        {
            case "FairyR":
                selectedObject = fairyRedCharacter;
                break;
            case "FairyG":
                selectedObject = fairyGreenCharacter;
                break;
            case "FairyO":
                selectedObject = fairyOrangeCharacter;
                break;
            default:
                selectedObject = fairyRedCharacter;
                break;
        }

        if (selectedObject != null)
        {
            selectedObject.SetActive(true);

            if (playerSpawn != null)
            {
                selectedObject.transform.position = playerSpawn.position;
            }

            if (cameraFollow != null)
            {
                cameraFollow.target = selectedObject.transform;
            }
        }
    }
}