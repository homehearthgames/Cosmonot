using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mainTimerText;

    [SerializeField] float mainTimerAmountInSeconds = 2400f; //40 minutes converted to seconds
    public UnityEvent onTimerOver;
    public UnityEvent onWaveIntervalTimerOver;

    private float mainTimerCurrentTime;
    public float waveIntervalTime;
    public float waveIntervalCurrentTime;
    private bool waveIntervalStarted = false; // flag to check if waveInterval has started

    private void Start()
    {
        mainTimerCurrentTime = mainTimerAmountInSeconds;
        waveIntervalStarted = false;
        CreateWaveTimerInterval();
        waveIntervalCurrentTime = waveIntervalTime;
    }

    private void Update()
    {
        mainTimerCurrentTime -= Time.deltaTime;
        int minutes = (int) mainTimerCurrentTime / 60;
        int seconds = (int) mainTimerCurrentTime % 60;
        mainTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(waveIntervalCurrentTime>0)
            waveIntervalCurrentTime -= Time.deltaTime;

        if (mainTimerCurrentTime <= 0)
        {
            onTimerOver.Invoke();
            Debug.Log("Main timer has ended.");
            enabled = false;
        }
        
        if(waveIntervalCurrentTime<=0){
            CreateWaveTimerInterval();
            onWaveIntervalTimerOver.Invoke();
            waveIntervalStarted = false;
            waveIntervalCurrentTime = waveIntervalTime;
            Debug.Log("Wave timer has ended.");
        }
    }

    private void CreateWaveTimerInterval()
    {
        // Get a random amount of time between 4 and 8 minutes, depending on the currentTime
        float currentTimeProgression = mainTimerCurrentTime / mainTimerAmountInSeconds;
        float maxTime = 8000f;
        float minTime = 4000f;
        float randomTime = Random.Range(minTime, maxTime);
        waveIntervalTime = Mathf.Lerp(minTime, randomTime, currentTimeProgression);
    }
}
