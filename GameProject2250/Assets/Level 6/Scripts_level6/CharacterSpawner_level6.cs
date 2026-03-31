using UnityEngine;

//Note: I kept this code the same as level 4 cuz im scared that if it's not consistent it will break 
public class CharacterSpawner_level6 : MonoBehaviour
{
    //Fields for each of the fairy options 
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;

    [SerializeField] private CameraFollow_MainHub cameraFollow;

    void Start()
    {
        //set all the fairies as inactive for now
        fairyRedCharacter.SetActive(false);
        fairyGreenCharacter.SetActive(false);
        fairyOrangeCharacter.SetActive(false);

        //Create a reference to store the fairy selected by the fairy
        GameObject selectedObject = null;

        switch (GameManager.instance.selectedFairy)
        {
            //if the player selects the red fairy
            case "FairyR":
                fairyRedCharacter.SetActive(true);
                selectedObject = fairyRedCharacter;
                break;
            //if the player selects the green fairy
            case "FairyG":
                fairyGreenCharacter.SetActive(true);
                selectedObject = fairyGreenCharacter;
                break;
            //if the player selects the orange fairy
            case "FairyO":
                fairyOrangeCharacter.SetActive(true);
                selectedObject = fairyOrangeCharacter;
                break;
            //use the red fairy as the default fairy (testing purposes)
            default:
                fairyRedCharacter.SetActive(true);
                selectedObject = fairyRedCharacter;
                break;
        }

        //make sure the camera is set to follow the selected fairy
        if (selectedObject != null && cameraFollow != null)
        {
            cameraFollow.target = selectedObject.transform;
        }
    }
}
