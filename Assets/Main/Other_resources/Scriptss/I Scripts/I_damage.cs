using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_damage
{
    public void Damage();

    public void Damage(int _damage);

    public void Damage(int _damage, Game_character_abstract _killer);
}
