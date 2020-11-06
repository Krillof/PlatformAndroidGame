using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPadTopBehavior : MonoBehaviour
{
    public GameObject PlayerPad;
    public Text PlayerDistance;
    public Text MoneyText;
    public GameObject[] LinesAccumulators;
    public GameObject[] Rays;
    public Sprite[] RaySprites;
    public int[] AccumulatorsValues;

    private const float MaxAccumulatorSize = 0.4f;

    private int DisableIterations = 0;
    private const int DisableIterationsMax = 100;

    private int RayNumber = -1;

    public void SetMoneyText(string str)
    {
        MoneyText.text = str;
    }

    void Start()
    {
        AccumulatorsValues = new int[LinesAccumulators.Length];
        for (int f = 0; f < Config.linesAmount; f++)
        {
            AccumulatorsValues[f] = 0;
        }
    }

    void Update()
    {
        for (int f = 0; f < Config.linesAmount; f++)
            LinesAccumulators[f].transform.localScale
                    = new Vector3(
                        MaxAccumulatorSize * (AccumulatorsValues[f] / (float)Config.maxAccumulatorValue),
                        LinesAccumulators[f].transform.localScale.y,
                        LinesAccumulators[f].transform.localScale.z
                        );
    }

    public void ResetAccumulatorsValues()
    {
        DisableIterations = DisableIterationsMax;
        for (int f = 0; f < AccumulatorsValues.Length; f++)
        {
            AccumulatorsValues[f] = 0;
        }
    }

    public void Accumulate()
    {
        if (DisableIterations > 0)
        {
            DisableIterations--;
            return;
        }

        float x = MainObjects.Player.transform.position.x;
        for (int f = 0; f < Config.linesAmount; f++)
        {
            if ((x > Config.linesXStart - 0.5f + Config.linesXSize * f)
                && (x < Config.linesXStart - 0.5f + Config.linesXSize * (f + 1)))
            {

                if (AccumulatorsValues[f] < Config.maxAccumulatorValue)
                    AccumulatorsValues[f]++;

                if (AccumulatorsValues[f] == Config.maxAccumulatorValue)
                {
                    RayNumber = f;
                    StartCoroutine(RayAnimation());
                    AccumulatorsValues[f] = 0;
                }
            }
            else
            {
                if (AccumulatorsValues[f] > 0)
                    AccumulatorsValues[f]--;
            }

            
        }
    }

    public void Moving()
    {
        PlayerDistance.text = ((int)MainObjects.Player.transform.position.y).ToString();
    }

    public void SetText(string Text)
    {
        PlayerDistance.text = Text;
    }

    IEnumerator RayAnimation()
    {
        GameObject Ray = Rays[RayNumber];
        Ray.SetActive(true);

        SpriteRenderer sr = Ray.GetComponent<SpriteRenderer>();

        for (int f = 0; f < RaySprites.Length*10; f++)
        {
            sr.sprite = RaySprites[f / 10];

            yield return new WaitForSeconds(0.015f);
        }

        Ray.SetActive(false);
    }
}
