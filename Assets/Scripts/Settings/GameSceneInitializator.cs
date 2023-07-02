using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneInitializator : MonoBehaviour
{
    private Pedestal pedestal;
    private Jumper jumper;
    private GameObject judge;
    private LinkSaver linkSaver;
    private Button byb;
    private Water water;

    private void Start()
    {
        judge = GameObject.FindWithTag("GameJudge");
        linkSaver = FindAnyObjectByType<LinkSaver>();
        byb = GameObject.FindWithTag("byb").GetComponent<Button>();
        water = GameObject.FindWithTag("Water").GetComponent<Water>();

        if (CrossSceneInfo.MultiplayerMode)
        {
            StartCoroutine(StartMultiplayerGame());
        }
        else
        {
            StartCoroutine(StartSinglePlayer());
        }
    }

    private IEnumerator StartMultiplayerGame()
    {
        pedestal = GameObject.FindWithTag("Pedestal").AddComponent<PedestalMultiplayer>();
        jumper = GameObject.FindWithTag("Player").AddComponent<JumperMultiplayer>();
        yield return null;
        linkSaver.Initial(jumper, pedestal);
        yield return null;
        judge.AddComponent<GameJudgeMultiplayer>();
        yield return null;
        FinalConstruct();
    }

    private IEnumerator StartSinglePlayer()
    {
        pedestal = GameObject.FindWithTag("Pedestal").AddComponent<Pedestal>();
        jumper = GameObject.FindWithTag("Player").AddComponent<Jumper>();
        yield return null;
        linkSaver.Initial(jumper, pedestal);
        yield return null;
        judge.AddComponent<GameJudge>();
        yield return null;
        FinalConstruct();
    }

    private void FinalConstruct()
    {
        byb.onClick.AddListener(jumper.BigButtonPressed);
        water.Initialize(judge.GetComponent<GameJudge>());
        Destroy(gameObject);
    }
}
