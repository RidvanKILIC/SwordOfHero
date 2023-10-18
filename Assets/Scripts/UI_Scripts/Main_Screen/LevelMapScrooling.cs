using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelMapScrooling:MonoBehaviour
{
    public RectTransform maskTransform;

    public ScrollRect mScrollRect;
    RectTransform mScrollTransform;
    private RectTransform mContent;
    //[SerializeField] public RectTransform target;
    [SerializeField] bool followingTarget = false;
    [SerializeField] float Smoothing;
    Vector2 newNormalizedPosition;
    float limit;
    [SerializeField] Animator _screenShaker;
    //Vector2 newNormalizedPosition = new Vector2();
    private void Start()
    {
        //mScrollRect = GetComponent<ScrollRect>();
       
    }
    public void init()
    {
        mScrollTransform = mScrollRect.transform as RectTransform;
        mContent = mScrollRect.content;
        Reset();
    }
    public void CenterOnItem(RectTransform target)
    {
        //Debug.Log("called");
        // Item is here
        var itemCenterPositionInScroll = GetWorldPointInWidget(mScrollTransform, GetWidgetWorldPoint(target));
        // But must be here
        var targetPositionInScroll = GetWorldPointInWidget(mScrollTransform, GetWidgetWorldPoint(maskTransform));
        // So it has to move this distance
        var difference = targetPositionInScroll - itemCenterPositionInScroll;
        difference.z = 0f;

        //clear axis data that is not enabled in the scrollrect
        if (!mScrollRect.horizontal)
        {
            difference.x = 0f;
        }
        if (!mScrollRect.vertical)
        {
            difference.y = 0f;
        }

        var normalizedDifference = new Vector2(
            difference.x / (mContent.rect.size.x - mScrollTransform.rect.size.x),
            difference.y / (mContent.rect.size.y - mScrollTransform.rect.size.y));

        newNormalizedPosition = mScrollRect.normalizedPosition - normalizedDifference;
        if (mScrollRect.movementType != ScrollRect.MovementType.Unrestricted)
        {
            newNormalizedPosition.x = Mathf.Clamp01(newNormalizedPosition.x);
            newNormalizedPosition.y = Mathf.Clamp01(newNormalizedPosition.y);
        }
        followingTarget = true;
        //mScrollRect.normalizedPosition = newNormalizedPosition;
    }
    void Update()
    {
        if (followingTarget)
        {
            limit = newNormalizedPosition.y;
            //Debug.Log("RectNorm: " + mScrollRect.normalizedPosition + " targetNorm: " + newNormalizedPosition);
            mScrollRect.normalizedPosition = Vector2.Lerp(mScrollRect.normalizedPosition, newNormalizedPosition, Time.deltaTime*Smoothing);
            if(mScrollRect.normalizedPosition.y>=(newNormalizedPosition.y-0.01f))
            {
                followingTarget = false;
            }
        }
        else
        {
            if (mScrollRect.normalizedPosition.y > newNormalizedPosition.y)
            {
                if(mScrollRect.normalizedPosition.y > (newNormalizedPosition.y+0.01f))
                    cameraShake();

                mScrollRect.normalizedPosition = newNormalizedPosition;
            }
        }

    }
    private void Reset()
    {
        if (maskTransform == null)
        {
            var mask = GetComponentInChildren<Mask>(true);
            if (mask)
            {
                maskTransform = mask.rectTransform;
            }
            if (maskTransform == null)
            {
                var mask2D = GetComponentInChildren<RectMask2D>(true);
                if (mask2D)
                {
                    maskTransform = mask2D.rectTransform;
                }
            }
        }
    }
    private Vector3 GetWidgetWorldPoint(RectTransform target)
    {
        //pivot position + item size has to be included
        var pivotOffset = new Vector3(
            (0.5f - target.pivot.x) * target.rect.size.x,
            (0.5f - target.pivot.y) * target.rect.size.y,
            0f);
        var localPosition = target.localPosition + pivotOffset;
        return target.parent.TransformPoint(localPosition);
    }
    private Vector3 GetWorldPointInWidget(RectTransform target, Vector3 worldPoint)
    {
        return target.InverseTransformPoint(worldPoint);
    }
    void cameraShake()
    {

        _screenShaker.SetTrigger("openAnim");

    }
}