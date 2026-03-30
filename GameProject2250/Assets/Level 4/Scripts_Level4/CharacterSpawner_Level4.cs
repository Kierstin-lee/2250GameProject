using UnityEngine;

public class CharacterSpawner_Level4 : MonoBehaviour
{
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;

    void Start()
    {
        fairyRedCharacter.SetActive(false);
        fairyGreenCharacter.SetActive(false);
        fairyOrangeCharacter.SetActive(false);

        switch (GameManager.instance.selectedFairy)
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
                fairyRedCharacter.SetActive(true);
                break;
        }
    }
}