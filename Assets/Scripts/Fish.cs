using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float speed;
    public bool catchable = false;
    public bool isHooked = false;

    public Animator animator;
    public SmallSplash splash;

    protected void Update()
    {
        FishMovement();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "WallBack")
        {
            if (this == Player.Instance.TargetFish)
                Player.Instance.TargetFish = null;

            FishManager.Instance.RemoveFish(this);
            CatchLabelManager.Instance.RemoveCatchLabel(this);
            ObjectPoolManager.Instance.Despawn(this.gameObject);
        }
        else if (other.gameObject.name == "WallCatchable")
        {
            if (GetComponent<Shark>() == null)
            {
                catchable = true;
                CatchLabelManager.Instance.AddCatchLable(this);
            }
        }
    }

    public void Init()
    {
        catchable = false;
        isHooked = false;
        UpdateAnimatorState(false, false);
    }

    public void FishMovement()
    {
        if (!isHooked)
            transform.Translate(new Vector3(0, 0, -speed) * Time.deltaTime);
    }

    public void PlayFishSplachEffect()
    {
        splash.PlaySplaceEffect();
    }

    public bool IsSplashEffectPlaying()
    {
        return splash.jetEffect.isPlaying;
    }

    public void UpdateAnimatorState(bool isHook, bool isCatch)
    {
        animator.SetBool("isHook", isHook);
        animator.SetBool("isCatch", isCatch);
    }

    public bool IsPlayingCatchAnim()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        Debug.Log($"IsName: {info.IsName("Catch")}");

        if (info.normalizedTime > 0.95f && info.IsName("Catch"))
            return true;
        else 
            return false;
    }
}
