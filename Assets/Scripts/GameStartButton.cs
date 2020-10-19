using DYW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Valve.VR.InteractionSystem.Sample
{
    public class GameStartButton : UIElement
    {
        protected SkeletonUIOptions ui;

        protected override void OnButtonClick()
        {
            GameManager.Instance.ChangeGameState(1);
        }
    }
}

