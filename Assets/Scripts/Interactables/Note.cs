using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI messageText;
    public bool showNote;
    [SerializeField]
    private Image noteImage;
    public static Note instance;
    private enum notes
    {
        DeadBodyNote
    }
    
    private Dictionary<string, string> noteMessages = new Dictionary<string,string>
    {
        {"DeadBodyNote","You’ve come this far, but know this — to end the ghost’s torment, you must find the bones. The fire’s hunger is unrelenting. Its rage is bound to the very remains that it could not consume. Only one place can tame this fury: the old furnace. That is where the fire was born, and that is where it will die.The bones will burn, the ghost will scream. The furnace will feed on them, just as the fire once fed on you. But be warned, you can’t unignite the past. Once the bones are placed inside, there will be no turning back.Find them. Burn them. The fire will not stop until it claims what is owed."},
    };


    private void Awake()
    {
        instance = this;
        if (instance != this || instance == null)
        {
            Destroy(instance);
        }
    }

    void Start()
    {
        StartCoroutine(assignText("DeadBodyNote"));
    }

    void Update()
    {
        if (showNote)
        {
            noteImage.transform.gameObject.SetActive(true);
        }
        else
        {
            noteImage.transform.gameObject.SetActive(false);
        }
    }

    private IEnumerator assignText(string interactableName)
    {
        if (this.gameObject.CompareTag("Note") && this.gameObject.name==interactableName)
        {
            if (noteMessages.TryGetValue(this.gameObject.name,out string message))
            {
                messageText.text = message;
            }
        }
        yield return new WaitForSeconds(1f);
    }
}
