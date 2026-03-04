using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PegsController : MonoBehaviour
{
    public int DesiredOrangePegs = 25;

    private List<Peg> pegs = new();

    private int orangePegCount = 0;
    private int hitOrangePegTotal = 0;

    private System.Random rng;

    private float totalPoints = 0;

    private void Start()
    {
        pegs = GetComponentsInChildren<Peg>().ToList();

        rng = new();

        pegs = pegs.OrderBy(x => rng.Next()).ToList();

        orangePegCount = Mathf.Min(DesiredOrangePegs, pegs.Count);

        for (int i = 0; i < orangePegCount; ++i)
        {
            var peg = pegs[i];
            peg.GetComponent<Peg>().SetType(Peg.PegType.Orange);
        }

        int purple = orangePegCount + rng.Next(0, pegs.Count - orangePegCount);
        pegs[purple].GetComponent<Peg>().SetType(Peg.PegType.Purple);
    }

    private void OnEnable()
    {
        GameEvents.EndOfTurnEvent += OnEndOfTurnEventCalled;
        GameEvents.EndOfGameEvent += OnEndOfGameEventCalled;
    }

    private void OnDisable()
    {
        GameEvents.EndOfTurnEvent -= OnEndOfTurnEventCalled;
        GameEvents.EndOfGameEvent -= OnEndOfGameEventCalled;
    }

    private void OnEndOfGameEventCalled(object sender, bool arg)
    {
        int highscore = PlayerPrefs.GetInt("Highscore", 0);

        if (totalPoints > highscore)
        {
            PlayerPrefs.SetInt("Highscore", Mathf.RoundToInt(totalPoints));
        }
    }

    private void OnEndOfTurnEventCalled(object sender, bool arg)
    {
        pegs.Clear();

        pegs = GetComponentsInChildren<Peg>().ToList();

        List<Peg> noneHitPeg = pegs.FindAll(x => !x.Hit);
        List<Peg> hitPegs = pegs.FindAll(x => x.Hit);

        List<Peg> hitOrangePegs = hitPegs.FindAll(x => x.Type == Peg.PegType.Orange);
        List<Peg> hitPurplePegs = hitPegs.FindAll(x => x.Type == Peg.PegType.Purple);

        float points = 0f;

        foreach(var peg in hitPegs)
        {
            points += peg.GetPoint();
        }

        totalPoints += points;

        GameEvents.CallScoreCalculatedEvent(totalPoints);

        if (hitPurplePegs.Count > 0)
        {
            List<Peg> noneHitBluePeg = noneHitPeg.FindAll(x => x.Type == Peg.PegType.Blue);
            int amountToTurnPurple = Mathf.Min(hitPurplePegs.Count, noneHitBluePeg.Count);
            for (int i = 0; i < amountToTurnPurple; ++i)
            {
                noneHitBluePeg[i].SetType(Peg.PegType.Purple);
            }
        }

        hitOrangePegTotal += hitOrangePegs.Count;

        foreach (var hitPeg in hitPegs)
        {
            Destroy(hitPeg.gameObject);
        }

        if (hitOrangePegTotal >= orangePegCount)
        {
            GameEvents.CallEndOfGameEvent(true);
        }
    }
}
