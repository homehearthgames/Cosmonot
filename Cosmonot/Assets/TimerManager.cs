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

    private void Start()
    {
        mainTimerCurrentTime = mainTimerAmountInSeconds;
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
            enabled = false;
        }
        else if(waveIntervalCurrentTime<=0){
            CreateWaveTimerInterval();
            waveIntervalCurrentTime = waveIntervalTime;
            onWaveIntervalTimerOver.Invoke();
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
