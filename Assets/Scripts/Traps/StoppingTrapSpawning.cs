using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoppingTrapSpawning : MonoBehaviour
{
    public GameObject[] ExclamationMarks;
    public GameObject TrapExample;

    Trap[] Traps;
    int[] ExclToggleIterations;
    const int toggleIterations = 60;

    class Trap
    {
        public GameObject trap;
        int iterations = 25;

        public bool isEnd()
        {
            iterations--;
            return iterations == 0;
        }
    }

    void Start()
    {
        Traps = new Trap[5] { null, null, null, null, null };
        ExclToggleIterations = new int[5] { toggleIterations, toggleIterations, toggleIterations, toggleIterations, toggleIterations };
    }

    void Update()
    {
        if (GameController.destroyAllGameObjects)
        {
            for (int f = 0; f < ExclamationMarks.Length; f++)
            {
                if (Traps[f] != null)
                {
                    Destroy(Traps[f].trap);
                    Traps[f] = null;
                }
            }
        }
    }

    public void ControlMarks()
    {
        for (int f = 0; f < ExclamationMarks.Length; f++)
        { 
            if (Traps[f] != null)
            {
                if ( 
                    (f%2 == 0) ? 
                    (Traps[f].trap.transform.position.y - MainObjects.Player.transform.position.y - 3 > 0)
                    :
                    (Traps[f].trap.transform.position.y - MainObjects.Player.transform.position.y + 3 < 0)
                ) {


                    ExclToggleIterations[f]--;

                    if ((ExclToggleIterations[f] == 0))
                    {
                        ExclamationMarks[f].SetActive(!ExclamationMarks[f].activeSelf);
                        ExclToggleIterations[f] = toggleIterations;
                    }
                } else
                {
                    ExclamationMarks[f].SetActive(false);
                }

            } else
            {
                ExclamationMarks[f].SetActive(false);
            }
        }
    }

    public void ControlTraps(int lineIndex)
    {
        if (Traps[lineIndex] != null) return;

        int r = UnityEngine.Random.Range(0, 14);

        if (r == 0)
        {
            Traps[lineIndex] = new Trap();

            Traps[lineIndex].trap = Transform.Instantiate(TrapExample, TrapExample.transform.position, Quaternion.identity);
            Traps[lineIndex].trap.transform.position = new Vector3(
                GameController.CountXPlace(lineIndex),
                GameController.CountYStartPlace(lineIndex, true),
                TrapExample.transform.position.z
                );
        }

        for (int f = 0; f < Traps.Length; f++)
        {
            if (Traps[f] != null)
            {
                if ((Traps[f].isEnd()) || 
                    (Math.Abs(Traps[f].trap.transform.position.y - MainObjects.Player.transform.position.y) > 11))
                {
                    Destroy(Traps[f].trap);
                    Traps[f] = null;
                }
            }
        }
    }
}
