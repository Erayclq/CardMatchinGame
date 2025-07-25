using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer Text")]
    public TextMeshProUGUI timerText;

    [Tooltip("Süre Bittiğinde ekrana gelecek.")]
    [SerializeField] GameObject gameOverPanel;

    private float remainingTime;
    private bool isRunning;

    void Start()
    {
        remainingTime = GameData.selectedSecond; // Sayacı Başlat
        isRunning = true;
        UpdateTimerText();
    }

    void Update()
    {
        if (!isRunning) return;

        remainingTime -= Time.deltaTime; // time deltatime ile süreyi azalt.

        if (remainingTime <= 0)
        {
            remainingTime = 0f; // 0'ın altına inme
            isRunning = false;
            OnTimerFinished();
        }
        UpdateTimerText();
    }

    private void UpdateTimerText() // sürenin ekrandaki azalmasını kontrol eder.
    {
        int second = Mathf.CeilToInt(remainingTime);// ceilinge yuvarlayıp int saniyeye çevir.
        timerText.text = second.ToString(); // ekrana yaz.
    }

    private void OnTimerFinished() // Timer Bittiğinde yapılacaklar.
    {
        gameOverPanel.SetActive(true);
    }

    public void StopTimer() // Oyun sayaç bitişinden önce biterse bu method CardControllerdan çağırılacak.
    {
        isRunning = false;
    }
    public void ResumeTimer()
    {
        isRunning = true;
    }
}
