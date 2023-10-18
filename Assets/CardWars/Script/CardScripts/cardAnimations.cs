using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;
namespace cardAnims
{
    public class cardAnimations : MonoBehaviour
    {
        [SerializeField] float bounceAnimDuration= 0.6f;
        [SerializeField] static GameObject fx;
        [SerializeField] static GameObject trj;
        static GameObject currentFx;
        static Transform currentObject;
        static Vector3 currentPos;
        GameManager _gameManager;
        static Transform target;
        // Start is called before the first frame update
        void Start()
        {
            _gameManager = GameObject.FindObjectOfType<GameManager>();
        }
        // Update is called once per frame
        void Update()
        {

        }
        public static void deckBounceAnim()
        {
            //currentObject.DOLocalMove(Vector3.zero, bounceAnimDuration).SetEase(Ease.InBounce);
        }
        public static void cardkMovePosAnim(Transform obj, float duration,bool playPlacedSound,AudioClip movementClip)
        {
            SoundManager.SInstance.playSfx(movementClip);
            currentObject = obj;
            LeanTween.moveLocal(obj.gameObject, Vector3.zero, duration).setEase(LeanTweenType.easeInSine)
                     .setOnComplete(playPlacedSound ? () => SoundManager.SInstance.playSfx(cardSounds.SInstance.cardPlacedSFX) : null);
        }
        public static void cardkMovePosAnimElastic(Transform obj, float duration, bool playPlacedSound, AudioClip movementClip)
        {
            SoundManager.SInstance.playSfx(movementClip);
            currentObject = obj;
            LeanTween.moveLocal(obj.gameObject, Vector3.zero, duration).setEase(LeanTweenType.easeInOutCubic)
                     .setOnComplete(playPlacedSound ? () => SoundManager.SInstance.playSfx(cardSounds.SInstance.cardPlacedSFX) : null);
        }
        public static void cardkMoveReturnPosAnim(Transform obj, float duration, bool playPlacedSound)
        {
            if (playPlacedSound)
            {
                SoundManager.SInstance.playSfx(cardSounds.SInstance.cardMoveToHandSFX);
            }
                

            currentObject = obj;
            LeanTween.moveLocal(obj.gameObject, Vector3.zero, duration).setEase(LeanTweenType.easeOutSine)
                     .setOnComplete(playPlacedSound ? () => SoundManager.SInstance.playSfx(cardSounds.SInstance.cardPlacedSFX) : null);
        }
        public static void cardScaleUpAnim(Transform obj, float duration)
        {
            LeanTween.scale(obj.gameObject, Vector3.one, duration).setEase(LeanTweenType.linear);
        }
        public static void cardRotateAnim(Transform obj, float duration,Vector3 rotation)
        {
            LeanTween.rotate(obj.gameObject, Vector3.zero, duration).setEase(LeanTweenType.easeInOutSine);
        }
        public static void changeCardSizeAnim(RectTransform obj, Vector2 estimatedSize, float duration)
        {
            LeanTween.size(obj, estimatedSize, duration).setEase(LeanTweenType.easeInCirc);
        }
        public static void fadeCardAnim(UnityEngine.UI.Image obj, float fadeAmount, float duration)
        {
            Color col = obj.color;
            Color fadeCol = new Color(1, 1, 1,fadeAmount);
            LeanTween.value(obj.gameObject, col.a, fadeCol.a, duration).setEase(LeanTweenType.easeInCirc).setOnUpdate((float value) => { fadeCol.a = value ; obj.color = fadeCol; });
        }
        public static void fadeAndDestroy(UnityEngine.UI.Image obj, float duration, AudioClip _clip=null)
        {
            LTSeq sequenceA = LeanTween.sequence();

            Color col = obj.color;
            Color fadeCol = new Color(1, 1, 1, 0f);
            LeanTween.value(obj.gameObject, col.a, 0f, duration).setEase(LeanTweenType.easeInCirc).setOnUpdate((float value) => { fadeCol.a = value; obj.color = fadeCol; })
                     .setOnComplete(() => {playSoundSfx(_clip); Destroy(obj.transform.parent.gameObject); });
            
        }
        static void playSoundSfx(AudioClip _clip)
        {
            if (_clip != null)
                SoundManager.SInstance.playSfx(_clip);
        }
        public static void shakeAnim(Transform obj,float shakeAmount ,float duration)
        {
            LeanTween.scale(obj.gameObject, Vector3.one * (1f+shakeAmount), (duration / 2)).setEase(LeanTweenType.easeOutBounce)
                .setOnComplete(() => fadeCardAnim(obj.GetChild(1).GetComponent<UnityEngine.UI.Image>(),0f,2f));
        }
        public static void shakeAttaackAnim(Transform obj, float shakeAmount, float duration)
        {
            Vector3 orjPos = obj.position;
            LeanTween.move(obj.gameObject, new Vector3(obj.position.x, obj.position.y+shakeAmount, obj.position.z), duration / 2).setEase(LeanTweenType.punch)
                .setOnComplete(() => LeanTween.move(obj.gameObject, orjPos, duration / 2).setEase(LeanTweenType.easeInOutBounce));
        }
        public static void scaleUpAnim(Transform obj, float duration)
        {
            LeanTween.scale(obj.gameObject, new Vector3(1.1f, 1.1f, 1.1f), duration).setEase(LeanTweenType.easeOutSine)
                     .setOnComplete(() => LeanTween.scale(obj.gameObject, new Vector3(1.1f, 1.1f, 1.1f), duration).setEase(LeanTweenType.easeInSine));
        }
        public static void changeScaleAnim(Transform obj,Vector3 estimatedScale,float duration)
        {
            LeanTween.scale(obj.gameObject, estimatedScale, duration).setEase(LeanTweenType.linear);
            //obj.DOScale(estimatedScale, duration).SetEase(Ease.InOutSine);
        }
        public static void createTrj(Vector3 pos)
        {

            currentFx = Instantiate(trj, pos, Quaternion.identity);
            currentFx.transform.SetParent(GameObject.Find("Canvas").transform);
            currentFx.transform.position = pos;
            currentFx.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            foreach (Transform child in currentFx.transform)
            {
                child.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }

        }
        public static void chargeTrj()
        {
            LeanTween.scale(currentFx.gameObject, new Vector3(currentFx.transform.localScale.x + 0.2f, currentFx.transform.localScale.y + 0.2f, currentFx.transform.localScale.z + 0.2f), 0.3f).setEase(LeanTweenType.easeOutSine)
                     .setOnComplete(()=> LeanTween.scale(currentFx.gameObject, new Vector3(currentFx.transform.localScale.x + 0.1f, currentFx.transform.localScale.y + 0.1f, currentFx.transform.localScale.z + 0.1f), 0.5f).setEase(LeanTweenType.easeInSine));
            foreach (Transform child in currentFx.transform)
            {
                LeanTween.scale(child.gameObject, new Vector3(child.transform.localScale.x + 0.2f, child.transform.localScale.y + 0.2f, child.transform.localScale.z + 0.2f), 0.3f).setEase(LeanTweenType.easeOutSine)
                         .setOnComplete(() => LeanTween.scale(child.gameObject, new Vector3(child.transform.localScale.x + 0.1f, child.transform.localScale.y + 0.1f, child.transform.localScale.z + 0.1f), 0.5f).setEase(LeanTweenType.easeInSine));
            }

        } 
        public static void attackTrjAnim(Vector3 pos,Vector3 des, float duration,AudioClip _trjClip, AudioClip _fxClip)
        {
            SoundManager.SInstance.playSfx(_trjClip);
            currentPos = des;
            LeanTween.moveLocal(currentFx, des, duration).setEase(LeanTweenType.easeInElastic).setOnComplete(() => playFx(_fxClip));
            Destroy(currentFx.gameObject, duration+0.1f);
        }
        public static void SpecialTrjAnim(Vector3 pos, Vector3 des, float duration,AudioClip _trjClip, AudioClip _fxClip)
        {
            SoundManager.SInstance.playSfx(_trjClip);
            currentPos = des;
            LeanTween.moveLocal(currentFx, des, duration).setEase(LeanTweenType.easeInOutCirc).setOnComplete(() => playFx(_fxClip));
            Destroy(currentFx.gameObject, duration + 0.1f);
        }
        public static void playFx(AudioClip _audio)
        {
            if(fx != null)
            {
                SoundManager.SInstance.playSfx(_audio);
                //Debug.Log("fx playing");
                GameObject _fx = Instantiate(fx.gameObject, currentPos, Quaternion.identity);
                _fx.transform.SetParent(target);
                _fx.transform.position = target.transform.position;
                _fx.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                foreach (Transform child in _fx.transform)
                {
                    child.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                }
                Destroy(_fx,2f);
                fx = null;
                target = null;
            }

        }
        public static void setTRjfx(GameObject _fx)
        {
            trj = null;
            trj = _fx;
        }
        public static void setfx(GameObject _fx)
        {
            fx = null;
            fx = _fx;

        }
        public static void setTarget(Transform _target)
        {
            target = _target;
        }
        void destroyCurrentObj()
        {
            Destroy(currentObject.gameObject);
        }
    }
}

