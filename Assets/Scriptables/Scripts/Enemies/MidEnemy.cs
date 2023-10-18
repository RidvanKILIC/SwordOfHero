using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MidEnemy", menuName = ("Enemy/Mid Enemy"))]
public class MidEnemy : Enemy
{
    public override void Attack(float _damageAmount)
    {
        base.Attack(_damageAmount);
        var hit = this.target.GetComponent<IDamagable>();
        if (hit != null)
        {
            hit.IDamage(this.attack);
            //Vector3 pos = new Vector3(target.transform.position.x + (Random.Range(-0.5f, 0.5f)), target.transform.position.y + (Random.Range(-0.5f, 0.5f)), target.transform.position.z);
            //GameObject fx = Instantiate(Attackfx, pos, Quaternion.identity);
            //fx.transform.SetParent(target.transform.parent.transform);
            //fx.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            //Destroy(fx, 1f);
        }

    }
    public override void enterArena(GameObject obj, Transform pos)
    {
        base.enterArena(obj, pos);
        SoundManager.SInstance.playSfx(stepsSFX,true);
       // GameObject.FindGameObjectWithTag("Managements").GetComponent<MiniGame_Manager>().currentPlayer.GetComponent<ScreenShaker>().shake(2,0.01f);
        LeanTween.move(obj, pos.position, arenaEnterDureation).setEase(LeanTweenType.linear).setOnComplete(() => SoundManager.SInstance.playSfx(null, false));
        //
    }
}
