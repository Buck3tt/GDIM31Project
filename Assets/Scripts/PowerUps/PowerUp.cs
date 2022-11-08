using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PowerUp 
{
    void usepowerup();

    void endpowerup();

}

public enum PowerUpType
{
    SpeedIncrease, SpeedDecrease, TakeDamage, HealDamage, Invonerability
}
