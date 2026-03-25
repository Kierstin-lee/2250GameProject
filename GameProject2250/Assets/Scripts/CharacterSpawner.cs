using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // deactivate 
        fairyRedCharacter.SetActive(false);
        fairyGreenCharacter.SetActive(false);
        fairyOrangeCharacter.SetActive(false);
        
        // activate selected one
        switch (GeneralGameManager.SelectedFairy)
        {
            case "FairyR":
                fairyRedCharacter.SetActive(true);
                break;
            case "FairyG":
                fairyGreenCharacter.SetActive(true);
                break;
            case "FairyO":
                fairyOrangeCharacter.SetActive(true);
                break;
            default:
                fairyRedCharacter.SetActive(true);  // red fairy as default
                break;
        }
    }
}
