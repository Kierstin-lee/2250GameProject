using UnityEngine;

public class CharacterSpawner_level6 : MonoBehaviour
{
    //Note: I kept this code the same as level 4 cuz im scared that if it's not consistent it will break 
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;

    [SerializeField] private CameraFollow_MainHub cameraFollow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
