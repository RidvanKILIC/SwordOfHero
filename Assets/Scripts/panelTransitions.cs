using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RK.Animations.panelTransitions
{
    public class panelTransitions : MonoBehaviour
    {
        public static void moveObj(GameObject obj, Vector3 position, float duration,float delay)
        {
            if (!obj.activeInHierarchy)
                obj.transform.parent.gameObject.SetActive(true);

            LeanTween.move(obj, position, duration).setEase(LeanTweenType.easeOutQuint).setDelay(delay);
        }
        public static void moveObjAndClosePanel(GameObject obj, Vector3 position, float duration, GameObject currentPanel, GameObject nextPanel)
        {
            LeanTween.move(obj, position, duration).setEase(LeanTweenType.easeInOutSine).setOnComplete(() => {
                currentPanel.transform.parent.gameObject.SetActive(false);
               
            });
            moveObj(nextPanel, Vector3.zero, 1f,0.5f);
        }
        public static void changeAlphaAndPanels(UnityEngine.UI.Image img, float targetAlpha, float duration, LeanTweenType tweenType, GameObject currentPanel, GameObject nextPanel)
        {
            Color col = img.color;
            Color fadeCol = new Color(0, 0, 0, targetAlpha);
            LeanTween.value(img.gameObject, col.a, fadeCol.a, duration).setEase(tweenType).setOnUpdate((float value) => { fadeCol.a = value; img.color = fadeCol; }).setOnComplete(() => { currentPanel.SetActive(false); changeAlphaAndNextPanel(img, duration, tweenType, nextPanel); });
        }
        public static void changeAlphaAndNextPanel(UnityEngine.UI.Image img, float duration, LeanTweenType tweenType, GameObject nextPanel)
        {
            Color col = img.color;
            Color fadeCol = new Color(0, 0, 0, 0);
            nextPanel.SetActive(true);
            LeanTween.value(img.gameObject, col.a, fadeCol.a, duration).setEase(tweenType).setOnUpdate((float value) => { fadeCol.a = value; img.color = fadeCol; }).setOnComplete(() => {  img.gameObject.SetActive(false); });
        }
    }

}
