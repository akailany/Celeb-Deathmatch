using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {
	public enum PlayerType
	{
		HUMAN, OPPNENT	
	};

	public static float MAX_HEALTH = 100f;

	public float healt = MAX_HEALTH;
	public string fighterName;
	public Fighter oponent;
	public bool enable;

	public PlayerType player;
	public FighterStates currentState = FighterStates.IDLE;

	protected Animator animator;
	private Rigidbody myBody;
	private AudioSource audioPlayer;

	
	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		audioPlayer = GetComponent<AudioSource> ();
	}

	public void UpdateHumanInput (){

        float hAxis = Input.GetAxis("Horizantal");

        if (hAxis > 0.1) {
			animator.SetBool ("WALK", true);
		} else {
			animator.SetBool ("WALK", false);
		}

		if (hAxis < -0.1) {
			animator.SetBool ("WALK_BACK", true);
            animator.SetBool("DEFEND", false);

        }
        else {
			animator.SetBool ("WALK_BACK", false);
			animator.SetBool ("DEFEND", false);
		}



        if (Input.GetButtonDown("JUMP")) {
			animator.SetTrigger("JUMP");
        } 

        if (Input.GetButtonDown("PUNCH")) {
			animator.SetTrigger("PUNCH");
		}

        if (Input.GetButtonDown("KICK")) {
            animator.SetTrigger("KICK");
        }

        if (Input.GetButtonDown("DUCK")) {
            animator.SetBool("DUCK", true);
        }

        if (Input.GetButtonUp("DUCK"))
        {
            animator.SetBool("DUCK", false);
        }

    }

    public void UpdateOponentInput () {

        float hAxis = Input.GetAxis("HorizantalD");

        if (hAxis > 0.1)
        {
            animator.SetBool("WALK", true);
        }
        else
        {
            animator.SetBool("WALK", false);
        }

        if (hAxis < -0.1)
        {
            animator.SetBool("WALK_BACK", true);
            animator.SetBool("DEFEND", false);

        }
        else
        {
            animator.SetBool("WALK_BACK", false);
            animator.SetBool("DEFEND", false);
        }



        if (Input.GetButtonDown("JUMPD"))
        {
            animator.SetTrigger("JUMP");
        }

        if (Input.GetButtonDown("PUNCHD"))
        {
            animator.SetTrigger("PUNCH");
        }

        if (Input.GetButtonDown("KICKD"))
        {
            animator.SetTrigger("KICK");
        }

        if (Input.GetButtonDown("DUCKD"))
        {
            animator.SetBool("DUCK", true);
        }

        if (Input.GetButtonUp("BNEW"))
        {
            Debug.Log("BBBBBBNNNNNNNEEEEEEWWWWWWW");
            animator.SetBool("DUCK", false);
        }


    }

    // Update is called once per frame
    void Update () {
		animator.SetFloat ("health", healtPercent);

		if (oponent != null) {
			animator.SetFloat ("oponent_health", oponent.healtPercent);
		} else {
			animator.SetFloat ("oponent_health", 1);
		}

        if (enable)
        {
            if (player == PlayerType.HUMAN)
            {
                UpdateHumanInput();
            }
            if (player == PlayerType.OPPNENT)
            {
                UpdateOponentInput();
            }


		if (healt <= 0 && currentState != FighterStates.DEAD) {
			animator.SetTrigger ("DEAD");
		}
	}



	//private float getDistanceToOponennt(){
	//	return Mathf.Abs(transform.position.x - oponent.transform.position.x);
	}

	public virtual void hurt(float damage){
		if (!invulnerable) {
			if (defending){
				damage *= 0.2f;
			}
			if (healt >= damage) {
				healt -= damage;
			} else {
				healt = 0;
			}

			if (healt > 0) {
				animator.SetTrigger ("TAKE_HIT");
			}
		}
	}



	public bool invulnerable {
		get {
			return currentState == FighterStates.TAKE_HIT 
				|| currentState == FighterStates.TAKE_HIT_DEFEND
					|| currentState == FighterStates.DEAD;
		}
	}

	public bool defending {
		get {
			return currentState == FighterStates.DEFEND 
				|| currentState == FighterStates.TAKE_HIT_DEFEND;
		}
	}

	public bool attacking {
		get {
			return currentState == FighterStates.ATTACK;
		}	
	}

	public float healtPercent {
		get {
			return healt / MAX_HEALTH;
		}	
	}

	public Rigidbody body {
		get {
			return this.myBody;
		}
	}
}
