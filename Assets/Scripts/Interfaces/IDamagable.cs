using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public float _Health { get; set; }
    public float _Shield { get; set; }
    public void IDamage(float _damageAmount);
    public void IAttack();
    public void IHeal(float _healAmount);
    public void IDead();
}
