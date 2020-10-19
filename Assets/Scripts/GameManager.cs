using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace DYW
{
    public class GameManager : MonoBehaviour
    {
        public WaveManager waveManager;
        public DayNightController dayNight;

        public PlayerData playerData;
        public FloatingCanvas playerUI;

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
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
#if !UNITY_EDITOR
            ChangeGameState(0);
#endif
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
            playerData.ResetForGame();
            gameStartButton.SetActive(false);
            playerUI.PrepareForGameStart();
            waveManager.StartWave();
            dayNight.Activate();
        }

        void GameOver()
        {
            gameStartButton.SetActive(true);
            waveManager.EndWave();
            playerUI.SetForGameOver();
            dayNight.ResetTime();
            Pulse(1f, 150, 100);
        }

        public void Pulse(float duration, float frequency, float amplitude)
        {
            hapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.LeftHand);
            hapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.RightHand);
        }
    }
}