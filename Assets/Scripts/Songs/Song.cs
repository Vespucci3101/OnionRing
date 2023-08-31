using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song
{
    public BattlePhase battlePhase;
    public ButtonsCreator buttonsCreator;

    public virtual IEnumerator PlaySong() { yield return new WaitForSeconds(1f); }

}
