
using MoreMountains.Feedbacks;
using UnityEngine;

public class MMFManager : MonoBehaviour
{
    public MMF_Player playerGetHit;
    public MMF_Player fire;

    private void Awake()
    {
        Global.mmfManager = this;
    }
}