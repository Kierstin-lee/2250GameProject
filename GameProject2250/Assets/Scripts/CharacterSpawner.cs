using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;
    
    [SerializeField] private CameraFollow_MainHub cameraFollow;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // deactivate 
        fairyRedCharacter.SetActive(false);
        fairyGreenCharacter.SetActive(false);
        fairyOrangeCharacter.SetActive(false);
        
        GameObject selectedObject = null;
        
        // activate selected one
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
            default: // red fairy as default
                fairyRedCharacter.SetActive(true);
                selectedObject = fairyRedCharacter;
                break;
        }
        
        // assign the camera target
        if (selectedObject != null && cameraFollow != null)
        {
            cameraFollow.target = selectedObject.transform;
        }
    }
}
