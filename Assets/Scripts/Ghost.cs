using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Ghost : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    public Transform player;
    public float health = 50f;
    private AudioSource ghostAudioSource;
    [SerializeField]
    private AudioClip[] audioClips;
    private Animator ghostAnimator;
    public bool isHurt;
    [SerializeField]
    private bool isScreamAnimationFinished;
    public float minDistance;
    public float attackDistance;
    public bool attack;
    public bool ghostDied;
    public static Ghost instance;
    private enum Animations
    {
        scream,
        idle,
        run,
        attack,
        walk
    }

    private void Start()
    {
        instance = this;
        if (instance!=this)
        {
            Destroy(this);
        }
        minDistance = 2f;
        agent = GetComponent<NavMeshAgent>();
        ghostAnimator = GetComponent<Animator>();
        ghostAudioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (player==null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        PlayAnimations();
        

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if ((isHurt && isScreamAnimationFinished) || (SceneManager.GetActiveScene().buildIndex==3 && distanceToPlayer<=attackDistance))
        {
            attack = true;
            agent.SetDestination(player.transform.position);
            FacePlayer();
            
        }
        else if (SceneManager.GetActiveScene().buildIndex==3 && distanceToPlayer <= attackDistance)
        {
            attackDistance = Vector3.Distance(transform.position,player.transform.position);
            if (attackDistance<=12f)
            {
                agent.SetDestination(player.transform.position);
                FacePlayer();
            }
        }


        if (agent.remainingDistance <= agent.stoppingDistance && agent.hasPath)
        {
            GameHandler.instance.playerDied = true;
        }

        if (agent.velocity.magnitude<0.5f)
        {
            FacePlayer();

        }
    }


    #region TakeDamage()
    public void TakeDamage(float amount)
    {

        health -= amount;
        if (health <= 0f)
        {
            Die();
        }

    }
    #endregion


    public void Die()
    {
        Destroy(gameObject);
    }

    #region soundfuncions
    public void PlayIdleSound()
    {
        ghostAudioSource.clip = audioClips[0];
        ghostAudioSource.Play();
    }

    public void PlayScreamSound()
    {
        if (isHurt)
        {
            ghostAudioSource.clip = audioClips[1];
            ghostAudioSource.Play();
        }

    }
    #endregion

    #region animations

   private void PlayAnimations()
   {
        if ((isHurt || attackDistance<=12f)||attack)
        {
            ghostAnimator.SetBool(nameof(Animations.scream),true);
            ghostAnimator.SetBool(nameof(Animations.idle), false);
            ghostAnimator.SetBool(nameof(Animations.run), false);
            ghostAnimator.SetBool(nameof(Animations.attack), false);
            ghostAnimator.SetBool(nameof(Animations.walk), false);

            if (isScreamAnimationFinished)
            {
                ghostAnimator.SetBool(nameof(Animations.run), true);
                ghostAnimator.SetBool(nameof(Animations.scream), false);
                ghostAnimator.SetBool(nameof(Animations.idle), false);
                ghostAnimator.SetBool(nameof(Animations.attack), false);
                ghostAnimator.SetBool(nameof(Animations.walk), false);

                if (agent.remainingDistance<=agent.stoppingDistance)
                {
                    ghostAnimator.SetBool(nameof(Animations.attack), true);
                    ghostAnimator.SetBool(nameof(Animations.scream), false);
                    ghostAnimator.SetBool(nameof(Animations.idle), false);
                    ghostAnimator.SetBool(nameof(Animations.run), false);
                    ghostAnimator.SetBool(nameof(Animations.walk), false);



                }
                else
                {
                    ghostAnimator.SetBool(nameof(Animations.run), true);
                    ghostAnimator.SetBool(nameof(Animations.scream),false);
                    ghostAnimator.SetBool(nameof(Animations.idle), false);
                    ghostAnimator.SetBool(nameof(Animations.attack), false);
                    ghostAnimator.SetBool(nameof(Animations.walk), false);

                }
            }
            else
            {
                ghostAnimator.SetBool(nameof(Animations.scream), true);
                ghostAnimator.SetBool(nameof(Animations.idle), false);
                ghostAnimator.SetBool(nameof(Animations.run), false);
                ghostAnimator.SetBool(nameof(Animations.attack), false);
                ghostAnimator.SetBool(nameof(Animations.walk), false);

            }
        }


        if (ghostDied)
        {
            ghostAnimator.SetBool(nameof(Animations.scream), false);
            ghostAnimator.SetBool(nameof(Animations.idle), false);
            ghostAnimator.SetBool(nameof(Animations.run), false);
            ghostAnimator.SetBool(nameof(Animations.attack), false);
            ghostAnimator.SetBool(nameof(Animations.walk), false);

        }

    }

    #endregion

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void OnScreamAnimationEnd()
    {
        isScreamAnimationFinished = true;
    }

    
}
