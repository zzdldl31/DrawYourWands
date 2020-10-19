using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RegenItem : MonoBehaviour
{
    //public Hand leftHand, rightHand;

    float destR, destH;
    float angle;
    int ccw;
    float t = 3, v, vh, r, h;

    // Start is called before the first frame update
    void Start()
    {
        destR = Random.Range(0.4f, 0.6f);
        destH = Random.Range(1.2f, 1.4f);
        ccw = Random.Range(0, 2) * 2 - 1;
        angle = Mathf.Atan2(transform.position.z, transform.position.x);
        v = (transform.position.magnitude - destR) / 3.0f;
        vh = (transform.position.y- destH) / 3.0f;
        h = transform.position.y;
        r = transform.position.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        
        t -= Time.deltaTime;
        if (t < 0) t = 0;
        else
        {
            r -= v * Time.deltaTime;
            h -= vh * Time.deltaTime;
        }
        angle += ccw * Time.deltaTime * Mathf.PI / 2.0f;

        transform.position = new Vector3(r * Mathf.Sin(angle), h, r * Mathf.Cos(angle));
        
    }

    public void Pickup()
    {
        var leftHand = Player.instance.hands[0];
        var rightHand = Player.instance.hands[1];

        //leftHand.DetachObject(gameObject);
        //rightHand.DetachObject(gameObject);
        PlayerData.inst.Restore(10);
        Destroy(gameObject);
    }

}
