using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PowerUp 
{
    void usepowerup();

    void endpowerup();

    float getTime();

    //float weight();
}

public enum PowerUpType { Immune, DoubleDamage, SpeedUp, SpeedDown, Heal}




