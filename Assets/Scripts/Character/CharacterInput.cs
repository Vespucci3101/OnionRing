using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{

    public MeshGenerator meshGenerator;
    public MeshGeneratorPart2 MeshGeneratorPart2;
    public PlayerScore playerScore;
    public BattlePhase battlePhase;

    public Queue<MovingButtons> buttonAs = new Queue<MovingButtons>();
    public Queue<MovingButtons> buttonBs = new Queue<MovingButtons>();
    public Queue<MovingButtons> buttonYs = new Queue<MovingButtons>();
    public Queue<MovingButtons> buttonXs = new Queue<MovingButtons>();

    public bool isSongPlaying = false;

    Rigidbody rb;

    const int GREEN_COLOR = 0;
    const int RED_COLOR = 1;
    const int BLUE_COLOR = 2;
    const int YELLOW_COLOR = 3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCapsule();

        if (battlePhase && battlePhase.isInBattlePhase) return;

        if (Input.GetButtonDown("A"))
        {
            if (buttonAs.Count <= 0) return;

            MovingButtons buttonA = buttonAs.Dequeue();
            
            // Useful to detect if the button as already been destroyed and should look the next buttonA on the screen.
            while (buttonA == null && buttonAs.Count > 0)
            {
                buttonA = buttonAs.Dequeue();
                playerScore.BreakCombo();
            }

            meshGenerator.ChangeMeshMaterial(GREEN_COLOR);
            MeshGeneratorPart2.ChangeMeshMaterial(GREEN_COLOR);

            buttonA.CheckButtonTiming();
        }

        if (Input.GetButtonDown("B"))
        {
            if (buttonBs.Count <= 0) return;

            MovingButtons buttonB = buttonBs.Dequeue();

            // Useful to detect if the button as already been destroyed and should look the next buttonB on the screen.
            while (buttonB == null && buttonBs.Count > 0)
            {
                buttonB = buttonBs.Dequeue();
                playerScore.BreakCombo();
            }

            meshGenerator.ChangeMeshMaterial(RED_COLOR);
            MeshGeneratorPart2.ChangeMeshMaterial(RED_COLOR);

            buttonB.CheckButtonTiming();
        }

        if (Input.GetButtonDown("X"))
        {
            if (buttonXs.Count <= 0) return;

            MovingButtons buttonX = buttonXs.Dequeue();

            // Useful to detect if the button as already been destroyed and should look the next buttonX on the screen.
            while (buttonX == null && buttonXs.Count > 0)
            {
                buttonX = buttonXs.Dequeue();
                playerScore.BreakCombo();
            }

            meshGenerator.ChangeMeshMaterial(BLUE_COLOR);
            MeshGeneratorPart2.ChangeMeshMaterial(BLUE_COLOR);

            buttonX.CheckButtonTiming();
        }

        if (Input.GetButtonDown("Y"))
        {
            if (buttonYs.Count <= 0) return;

            MovingButtons buttonY = buttonYs.Dequeue();

            // Useful to detect if the button as already been destroyed and should look the next buttonY on the screen.
            while (buttonY == null && buttonYs.Count > 0)
            {
                buttonY = buttonYs.Dequeue();
                playerScore.BreakCombo();
            }

            meshGenerator.ChangeMeshMaterial(YELLOW_COLOR);
            MeshGeneratorPart2.ChangeMeshMaterial(YELLOW_COLOR);

            buttonY.CheckButtonTiming();
        }
    }

    void MoveCapsule()
    {
        rb.position += Vector3.forward * 25f * Time.deltaTime;
    }
}
