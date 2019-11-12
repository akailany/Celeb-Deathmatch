using UnityEngine;
using System.Collections;

public class BattleController : MonoBehaviour {
	public int roundTime = 60;
	private float lastTimeUpdate = 0;
	private bool battleStarted;
	private bool battleEnded;

	public Fighter player1;
    public Fighter player2;
    public AudioSource musicPlayer;
    public AudioClip backgroundmusic;



	// Use this for initialization
	void Start () {
	}

    private void expireTime()
    {
        if (player1.healtPercent > player2.healtPercent){
            player2.healt = 0;
        }
        else {
            player1.healt = 0;
        }
    }


    // Update is called once per frame
    void Update () {
		if (!battleStarted) {
			battleStarted = true;

			player1.enable = true;
            player2.enable = true;

            //GameUtils.PlaySound(backgroundmusic, musicPlayer);

		}

		if (battleStarted && !battleEnded) {
			if (roundTime > 0 && Time.time - lastTimeUpdate > 1) {
				roundTime--;
				lastTimeUpdate = Time.time;
				
                if (roundTime == 0){
                    expireTime();
                }
            }

			if (player1.healtPercent <= 0) {
				battleEnded = true;
			}
            if (player2.healtPercent <= 0) {
                battleEnded = true;
            }

        }
	}
}
