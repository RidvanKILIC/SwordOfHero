using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playBodyAnim(List<GameObject> bodyParts,string animName,bool loop,float _timeScale=1f)
    {
        //Debug.Log("Called : " + animName);
        foreach(var item in bodyParts)
        {
            if (item.activeInHierarchy)
            {
                //item.GetComponent<SkeletonAnimation>().state.ClearTracks();
                //item.GetComponent<SkeletonAnimation>().skeleton.SetToSetupPose();
                item.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, animName, loop).TimeScale = _timeScale;
                //if(animName!= "Defaults/Default_Default_Win" && animName!= "Defaults/Default_Default_Idle")
                    item.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(1, "Defaults/Default_Default_Idle", true).TimeScale = _timeScale;
            }
                
        }

    }
    public void playAttackAnim(List<GameObject> bodyParts,GameObject weapon, string animName,float _timeScale=1f)
    {
        foreach (var item in bodyParts)
        {
            
            if (item.activeInHierarchy)
            {

                item.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, animName/*"Defaults/Default_Default_RPunch"*/, false).TimeScale = _timeScale;
                item.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(1, "Defaults/Default_Default_Idle"/*"Defaults/Default_Default_RPunch"*/, true).TimeScale = _timeScale;
            }
                
        }
        //weapon.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, animName, false).TimeScale = _timeScale;


    }
    public void playWinAnim(List<GameObject> bodyParts, string animName, float _timeScale = 1f)
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.playerWinSFX);
        foreach (var item in bodyParts)
        {

            if (item.activeInHierarchy)
            {
                item.GetComponent<SkeletonAnimation>().AnimationState.ClearTracks();
                item.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, animName/*"Defaults/Default_Default_RPunch"*/, false).TimeScale = _timeScale;
                //item.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(1, "Defaults/Default_Default_Idle"/*"Defaults/Default_Default_RPunch"*/, true).TimeScale = _timeScale;
            }

        }
        StartCoroutine(playIdleAnim(bodyParts, "Defaults/Default_Default_Idle", 1f));
    }
    IEnumerator playIdleAnim(List<GameObject> bodyParts, string animName, float _timeScale = 1f)
    {
        yield return new WaitForSeconds(1);
        foreach (var item in bodyParts)
        {

            if (item.activeInHierarchy)
            {
                item.GetComponent<SkeletonAnimation>().AnimationState.ClearTracks();
                item.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, animName/*"Defaults/Default_Default_RPunch"*/, true).TimeScale = _timeScale;
                //item.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(1, "Defaults/Default_Default_Idle"/*"Defaults/Default_Default_RPunch"*/, true).TimeScale = _timeScale;
            }

        }
    }

}
