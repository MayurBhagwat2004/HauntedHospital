
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : MonoBehaviour
{
    [SerializeField]
    public bool shouldPlay;
    [SerializeField]
    private ParticleSystem dollEffect;
    public static Doll instance { get; private set; }
    private Animator dollAnimator;
    [SerializeField]
    private AudioClip audioClip;
    private bool isPlaying;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (instance!=this)
        {
            Destroy(this);
        }
        instance = this;
        shouldPlay = false;
        dollAnimator = GameObject.FindWithTag("Doll").GetComponent<Animator>();
    }



    void Update()
    {
        if (shouldPlay && !isPlaying)
        {
            StartCoroutine(PlayScareLaugh());
        }
    }

    private IEnumerator PlayScareLaugh()
    {
        isPlaying = true;
        //SoundHandler.instance.PlayScarySound(audioClip);
        dollEffect.Play();
        dollAnimator.Play("dollAnim");
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
        shouldPlay = false;

    }
}
