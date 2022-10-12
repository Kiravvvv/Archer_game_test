using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_player : Health
{

    #region Methods
    protected override void Change_health(int _change)
    {
        base.Change_health(_change);

        Game_administrator.Instance.Player_Health_info((float)Health_active / (float)Health_default);
    }


    public override void Damage_add(int _damage, Game_character_abstract _killer)
    {
        base.Damage_add(_damage, _killer);

        if (Alive_bool)
        {
            Game_administrator.Instance.Player_add_damage();
        }
    }

    public override void Death()
    {
        base.Death();
        Game_administrator.Instance.End_game(false);
    }
    #endregion
}
