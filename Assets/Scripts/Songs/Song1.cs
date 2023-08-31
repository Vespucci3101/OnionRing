using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song1 : Song
{
    const int BUTTONX_IDX = 0;
    const int BUTTONY_IDX = 1;
    const int BUTTONA_IDX = 2;
    const int BUTTONB_IDX = 3;

    public Song1(BattlePhase battlePhase, ButtonsCreator buttonsCreator)
    {
        this.buttonsCreator = buttonsCreator;
        this.battlePhase = battlePhase;
    }

    public override IEnumerator PlaySong()
    {
        // buttonsCreator.CreateButton(buttonSpriteX, BUTTONX_IDX, songSpeed);
        // buttonsCreator.CreateButton(buttonSpriteY, BUTTONY_IDX, songSpeed);
        // buttonsCreator.CreateButton(buttonSpriteA, BUTTONA_IDX, songSpeed);
        // buttonsCreator.CreateButton(buttonSpriteB, BUTTONB_IDX, songSpeed);

        float songSpeed = 0.55f;
        float secondsBetweenButtons = 0.5f;
        float secondsBetweenButtonsLoop = 0.3f;
        float secondsBetweenButtonsSlow = 1f;

        battlePhase.isPlayingSong = true;

        yield return new WaitForSeconds(secondsBetweenButtons);

        buttonsCreator.CreateButton(BUTTONX_IDX, songSpeed);

        yield return new WaitForSeconds(secondsBetweenButtons);

        buttonsCreator.CreateButton(BUTTONY_IDX, songSpeed);

        yield return new WaitForSeconds(secondsBetweenButtons);

        buttonsCreator.CreateButton(BUTTONA_IDX, songSpeed);

        yield return new WaitForSeconds(secondsBetweenButtons);

        buttonsCreator.CreateButton(BUTTONB_IDX, songSpeed);

        yield return new WaitForSeconds(secondsBetweenButtons);

        buttonsCreator.CreateButton(BUTTONA_IDX, songSpeed);

        yield return new WaitForSeconds(secondsBetweenButtons);

        buttonsCreator.CreateButton(BUTTONY_IDX, songSpeed);

        yield return new WaitForSeconds(secondsBetweenButtons);

        for (int i = 0; i < 3; i++)
        {
            buttonsCreator.CreateButton(BUTTONX_IDX, songSpeed);

            yield return new WaitForSeconds(secondsBetweenButtonsLoop);

            buttonsCreator.CreateButton(BUTTONY_IDX, songSpeed);

            yield return new WaitForSeconds(secondsBetweenButtonsLoop);

            buttonsCreator.CreateButton(BUTTONA_IDX, songSpeed);

            yield return new WaitForSeconds(secondsBetweenButtonsLoop);

            buttonsCreator.CreateButton(BUTTONB_IDX, songSpeed);

            yield return new WaitForSeconds(secondsBetweenButtonsLoop);
        }

        for (int i = 0; i < 5; i++)
        {
            buttonsCreator.CreateButton(BUTTONX_IDX, songSpeed);

            yield return new WaitForSeconds(secondsBetweenButtonsLoop);

            buttonsCreator.CreateButton(BUTTONB_IDX, songSpeed);

            yield return new WaitForSeconds(secondsBetweenButtonsLoop);
        }

        for (int i = 0; i < 5; i++)
        {
            buttonsCreator.CreateButton(BUTTONA_IDX, songSpeed);

            yield return new WaitForSeconds(secondsBetweenButtonsSlow);
        }

        buttonsCreator.CreateButton(BUTTONX_IDX, songSpeed);

        yield return new WaitForSeconds(secondsBetweenButtons);

        buttonsCreator.CreateButton(BUTTONY_IDX, songSpeed);

        yield return new WaitForSeconds(secondsBetweenButtons);

        buttonsCreator.CreateButton(BUTTONA_IDX, songSpeed);

        yield return new WaitForSeconds(secondsBetweenButtons);

        buttonsCreator.CreateButton(BUTTONB_IDX, songSpeed);

        yield return new WaitForSeconds(secondsBetweenButtons);

        for (int i = 0; i < 5; i++)
        {
            buttonsCreator.CreateButton(BUTTONY_IDX, songSpeed);

            yield return new WaitForSeconds(secondsBetweenButtons);

            buttonsCreator.CreateButton(BUTTONA_IDX, songSpeed);

            yield return new WaitForSeconds(secondsBetweenButtons);
        }

        for (int i = 0; i < 5; i++)
        {
            buttonsCreator.CreateButton(BUTTONX_IDX, songSpeed);

            yield return new WaitForSeconds(secondsBetweenButtons);
        }

        yield return new WaitForSeconds(5f);

        InitializeBattle();
    }

    void InitializeBattle()
    {
        battlePhase.buttonsCreator = buttonsCreator;
        battlePhase.isPlayingSong = false;
        battlePhase.StartBattle();
    }
}
