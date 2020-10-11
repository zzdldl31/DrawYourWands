using UnityEngine;

public class LaserHitReceiver : MonoBehaviour
{
#if true
    private void Awake()
    {
        OnLaserKeep += (s, e) => print("성공");
    }
#endif

    public PointerEventHandler OnLaserKeep;
}