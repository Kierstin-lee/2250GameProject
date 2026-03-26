using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;

    [SerializeField] private CameraFollow_MainHub cameraFollow;

    void Start()
    {
        fairyRedCharacter.SetActive(false);
        fairyGreenCharacter.SetActive(false);
        fairyOrangeCharacter.SetActive(false);

        GameObject selectedObject = null;

        switch (GameManager.instance.selectedFairy)
        {
            case "FairyR":
                fairyRedCharacter.SetActive(true);
                selectedObject = fairyRedCharacter;
                break;
            case "FairyG":
                fairyGreenCharacter.SetActive(true);
                selectedObject = fairyGreenCharacter;
                break;
            case "FairyO":
                fairyOrangeCharacter.SetActive(true);
                selectedObject = fairyOrangeCharacter;
                break;
            default:
                fairyRedCharacter.SetActive(true);
                selectedObject = fairyRedCharacter;
                break;
        }

        if (selectedObject != null && cameraFollow != null)
        {
            cameraFollow.target = selectedObject.transform;
        }
    }
}