using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace DYW
{
    public class GameManager : MonoBehaviour
    {

        public GameObject Player => GameObject.FindGameObjectWithTag("Player");
        public WaveSpawner spawner;
        public FloatingCanvas player;
        public GameObject gameStartButton;
        public int gameState = 0; // 0 = menu, 1 = gameplay 

        public SteamVR_Action_Vibration hapticAction;


        private static GameManager instance = null;
        public static GameManager Instance
        {
            get
            {
                if (null == instance)
                {
                    return null;
                }
                return instance;
            }
        }

        void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            ChangeGameState(0);
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void Pulse(float duration, float frequency, float amplitude)
        {
            hapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.LeftHand);
            hapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.RightHand);
        }

        public void ChangeGameState(int state)
        {
            gameState = state;
            if (gameState == 0)
                GameOver();
            else if(gameState == 1)
                GameStart();
        }

        void GameStart()
        {
            gameStartButton.SetActive(false);
            spawner.ResetWave();
            player.healthbar.GainHealth(player.healthbar.maximumHealth);
            player.SetTextCenter("");
            player.MoveScoreBoard(428);
            player.killCount = 0;
            player.healthbar.gameObject.SetActive(true);
        }

        void GameOver()
        {
            gameStartButton.SetActive(true);
            foreach (Transform obj in spawner.transform)
            {
                Destroy(obj.gameObject);
            }
            player.SetTextCenter("Game Over");
            player.MoveScoreBoard(-150);
            player.healthbar.gameObject.SetActive(false);


            Pulse(1f, 150, 100);
        }

    }
}