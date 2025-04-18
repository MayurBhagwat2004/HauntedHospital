using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutScene : MonoBehaviour
{
    public PlayableDirector cutscene;
    public bool cutScenePlayed;

    void Start()
    {
        cutscene = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (this.gameObject.name== "ExitDoorCutScene")
        {
            if (cutscene.state==PlayState.Paused)
            {
                Debug.Log("paused");

                StartCoroutine(GameHandler.instance.SendToNextLevel());
            }
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !cutScenePlayed)
        {
            TimeLineManager.instance.PlayCutScenes(cutscene);
            cutScenePlayed = true;
            
        }

    }
}
