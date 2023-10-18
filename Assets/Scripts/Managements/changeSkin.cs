using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
namespace RK.Skeleton
{
    public class changeSkin : MonoBehaviour
    {
        SkeletonAnimation _skeletonAnimation;
        // Start is called before the first frame update
        void Start()
        {
            //_skeletonAnimation=GetComponent<SkeletonAnimation>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public static void changeSkeletonSkin(SkeletonAnimation _skeleton, string skinName)
        {
            //Debug.Log("Skeleton skin name: "+_skeleton.Skeleton.Skin.Name+" sended name: "+skinName);
            _skeleton.Skeleton.SetSkin(skinName);
            _skeleton.Skeleton.SetSlotsToSetupPose();
            _skeleton.LateUpdate();
        }
    }
}

