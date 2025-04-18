using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimeLineManager : MonoBehaviour
{

    [SerializeField]
    private PlayableDirector director;
    private Player player;
    public bool isPlaying;
    public static TimeLineManager instance;
    void Start()
    {
        instance = this;
        director = director.GetComponent<PlayableDirector>();
        if (director!=null)
        {
            director.Play();
        }
        isPlaying = false;
        player = GetComponent<Player>();    
    }

    void Update()
    {
        if(player!=null)
        {
            player.enabled = isPlaying;

        }
        if (director!=null)
        {
            if (director.state==PlayState.Paused)
            {
                isPlaying = true;         
            }
            else
            {
                isPlaying=false;
            }

        }
        
    }

    public void PlayCutScenes(PlayableDirector cutScene)
    {
        director = cutScene.GetComponent<PlayableDirector>();
        director.Play();
    }
}
