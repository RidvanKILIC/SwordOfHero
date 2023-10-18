using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimationController : MonoBehaviour
{
    [SerializeField] List<GameObject> bodyPartsWithoutWeapons = new List<GameObject>();
    [SerializeField] List<GameObject> bodyParts = new List<GameObject>();
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayBodyAnim(string animName,bool loop,float _timeScale=1f)
    {
        foreach (var item in bodyParts)
        {
            if (item.activeInHierarchy)
            {
                if (item.GetComponent<Animator>() == null)
                {
                    if (item.GetComponent<SkeletonAnimation>().state != null)
                    {
                        if (!item.GetComponent<SkeletonAnimation>().state.Equals(animName)/* && !item.GetComponent<SkeletonAnimation>().state.Equals("Defaults/Default_Default_Hit")*/ 
                            && (!animName.Contains("_Hit")))
                        {
                            item.GetComponent<SkeletonAnimation>().state.ClearTracks();
                            item.GetComponent<SkeletonAnimation>().skeleton.SetToSetupPose();
                            item.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, animName, loop).TimeScale = _timeScale;
                            //item.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(1, "Defaults/Default_Default_Idle", true).TimeScale = _timeScale;
                        }
                    }
                    else
                    {
                        item.GetComponent<SkeletonAnimation>().state.ClearTracks();
                        item.GetComponent<SkeletonAnimation>().skeleton.SetToSetupPose();
                        item.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, animName, loop).TimeScale = _timeScale;
                        //item.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(1, "Defaults/Default_Default_Idle", true).TimeScale = _timeScale;
                    }
                }
                else
                {
                    Debug.Log("called");
                    //if(item.GetComponent<Animator>().StopP(animName))
                    item.GetComponent<Animator>().SetTrigger(animName);
                }
                    
            }
                

        }
    }
    public void PlayAttacAnim(float _timeScale=1)
    {
        foreach (var item in bodyParts)
        {
            if (item.activeInHierarchy)
            {
                
                if (item.GetComponent<Animator>()==null && weapon.GetComponent<Animator>() == null)
                {
                    if (!item.GetComponent<SkeletonAnimation>().state.Equals(weapon.name + "_Attack"))
                    {
                        item.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, weapon.name + "_Attack", false).TimeScale = _timeScale;
                        weapon.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, weapon.name + "_Attack", false).TimeScale = _timeScale;
                        item.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(1, "Defaults/Default_Default_Idle", true).TimeScale = _timeScale;
                        weapon.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(1, "Defaults/Default_Default_Idle", true).TimeScale = _timeScale;
                    }
                }
                else
                {
                    Debug.Log("called");
                    item.GetComponent<Animator>().SetTrigger(weapon.name + "_Attack");
                    weapon.GetComponent<Animator>().SetTrigger(weapon.name + "_Attack");
                }
            }
                

        }
        //weapon.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0,weapon.name+"_Attack", false).TimeScale=_timeScale;
    }
}
