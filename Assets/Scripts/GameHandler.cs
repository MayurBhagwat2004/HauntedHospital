using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{

    #region Note related variables
    [SerializeField]
    private TextMeshProUGUI actionText;
    [SerializeField]
    private TextMeshProUGUI messageText;

    #endregion

    public AudioSource audioSource;

    [SerializeField]
    private bool didPlayerInteracted;
    public TextMeshProUGUI deadActionText;
    public TextMeshProUGUI deadMessageText;
    public static GameHandler instance { get; private set; }
    private string tagName;
    [SerializeField]
    private List<AudioClip> audioClipList;
    private int currentLevel;
    public bool keyAcquired;
    public bool nearExitDoor;
    public bool playerDied;
    private bool lighterAquired;
    [SerializeField]
    private GameObject deadPanel;

    
    private enum Tags
    {
        Note,
        Doll,
        Door,
        weapon,
        Lighter,
        FuelTank,
        GraveYardBody,
        Key
    }

    public PlayableDirector wonCutScene;
    public GameObject won;

    private void Awake()
    {
        instance = this;
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
    }


    void Start()
    {

        if (instance != this)
        {
            Destroy(this);

        }
        instance = this;
        if (audioClipList.Count > 0 && SoundHandler.instance!=null)
        {
            if (currentLevel == 1 || currentLevel==3)
            {
                SoundHandler.instance.UpdateMusic(audioClipList[0]);
            }

        }
        instance = this;

        if (audioSource!=null)
        {
            audioSource.clip = audioClipList[0];
            audioSource.Play();
        }
    }

    private void Update()
    {
        IsTagValid();
    }

    #region SendToNextLevel()
    public IEnumerator SendToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Debug.Log("Sending to GraveYard");
        yield return new WaitForSeconds(1f);
    }
    #endregion


    #region AssignActionText(bool isTagValid)
    private void IsTagValid()
    {

        if (tagName == nameof(Tags.Doll) && !didPlayerInteracted)
        {
            actionText.gameObject.SetActive(true);
            actionText.text = "Interact [E]";
        }
        else if (tagName == nameof(Tags.Door) && !didPlayerInteracted)
        {
            actionText.gameObject.SetActive(true);
            actionText.text = "Open Door [E]";
            door.instance.openDoor = false;
        }

        else if (tagName == nameof(Tags.Note) && didPlayerInteracted)
        {
            Note.instance.showNote = true;
            actionText.gameObject.SetActive(true);
            actionText.text = "Close the Note [E]";
        }
        else if (tagName == nameof(Tags.Note) && !didPlayerInteracted)
        {
            Note.instance.showNote = false;
            actionText.gameObject.SetActive(true);
            actionText.text = "Read the Note [E]";
        }
        else if (tagName == nameof(Tags.weapon) && !didPlayerInteracted)
        {
            actionText.gameObject.SetActive(true);
            messageText.gameObject.SetActive(true);
            messageText.text = "Gun";
            actionText.text = "Pick Up Weapon";
        }

        else if (playerDied)
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().enabled = false;
            deadPanel.gameObject.SetActive(true);
            deadActionText.text = "Press E to Restart the game";
            deadMessageText.text = "You Died";
            deadActionText.fontSize = 58;
            deadMessageText.fontSize = 58;
        }
        else if (!keyAcquired && tagName=="ExitDoor")
        {
            actionText.gameObject.SetActive(true);
            messageText.gameObject.SetActive(true);
            actionText.text = "[E]";
            messageText.text = "Find The Exit Door Key";
        }
        else if (tagName==nameof(Tags.Lighter))
        {
            actionText.gameObject.SetActive(true);
            messageText.gameObject.SetActive(true);
            actionText.text = "[E]";
            messageText.text = "Grab The Lighter";
        }
        else if (tagName==nameof(Tags.FuelTank))
        {
            actionText.gameObject.SetActive(true);
            messageText.gameObject.SetActive(true);
            actionText.text = "[E]";
            messageText.text = "Grab The FuelTank";
        }
        else if (tagName==nameof(Tags.GraveYardBody) && !Lighter.instance.weaponAcquired && !FuelTank.instance.fuelTankAcquired)
        {
            actionText.gameObject.SetActive(true);
            messageText.gameObject.SetActive(true);
            messageText.text = "Find The Instruments To Burn The Body";
        }
        else if (tagName==nameof(Tags.GraveYardBody) && Lighter.instance.weaponAcquired)
        {
            actionText.gameObject.SetActive(true);
            messageText.gameObject.SetActive(true);
            actionText.text = "[E]";
            messageText.text = "Burn The Body";

        }
        else if (tagName==nameof(Tags.GraveYardBody) && FuelTank.instance.fuelTankAcquired && !FuelTank.instance.fuelTankPoured)
        {
            actionText.gameObject.SetActive(true);
            messageText.gameObject.SetActive(true);
            actionText.text = "[E]";
            messageText.text = "Pour Gas On The Body";

        }
        else if (tagName==nameof(Tags.Key))
        {
            actionText.gameObject.SetActive(true);
            messageText.gameObject.SetActive(true);
            actionText.text = "[E]";
            messageText.text = "Grab The Key";
        }
        else
        {
            if (Note.instance!=null)
            {
            Note.instance.showNote = false;

            }
            actionText.gameObject.SetActive(false);
            messageText.gameObject.SetActive(false);
            actionText.text = "";
            messageText.text = "";
            deadPanel.gameObject.SetActive(false);
        }
       
    }

    public void displayText(string actionTexts,string messageTexts)
    {
        actionText.gameObject.SetActive(true);
        messageText.gameObject.SetActive(true);
        actionText.text = actionText.text;
        messageText.text = messageText.text;
    }

    #endregion

    #region Getters
    public bool GetDidPlayerInteracted()
    {
        return didPlayerInteracted;
    } 
    #endregion

    #region setters
    public void SetTagName(string name) 
    {
        tagName = name;
    }

    public void setDidPlayerInteracted(bool value)
    {
        didPlayerInteracted = value;
    }



    #endregion

  








}
