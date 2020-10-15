using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandTrail : MonoBehaviour
{
    public GameObject tracer;
    public float interval;
    public SteamVR_Behaviour_Pose pose;
    public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (pose == null)
            pose = this.GetComponent<SteamVR_Behaviour_Pose>();
        if (pose == null)
            Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);

        if (interactWithUI == null)
            Debug.LogError("No ui interaction action has been set on this component.", this);
    }

    // Update is called once per frame
    void Update()
    {
        if (interactWithUI != null && interactWithUI.GetState(pose.inputSource))
        {
            time += Time.deltaTime;
            if (time > interval)
            {
                time -= interval;
                Instantiate(tracer, transform.position, Quaternion.identity, null);
            }
        }
        else time = 0;

    }
}
