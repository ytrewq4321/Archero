using System.Collections;
using UnityEngine;
using Zenject;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float countdownTime = 3.0f;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject UI;
    private IGameMachine gameMachine;

    [Inject]
    public void Constructor(IGameMachine gameMachine)
    {
        this.gameMachine = gameMachine;
    }

    private void Awake()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {      
        float currentTime = countdownTime;
        while (currentTime > 0)
        {
            countdownText.text = currentTime.ToString("0");
            yield return new WaitForSecondsRealtime(1.0f);
            currentTime -= 1.0f;
        }

        countdownText.text = "Старт";

        yield return new WaitForSecondsRealtime(1.0f);
        countdownText.gameObject.SetActive(false);

        gameMachine.StartGame();
        gameObject.SetActive(false);
    }
}
