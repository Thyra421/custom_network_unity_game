using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CastBarGUI : MonoBehaviour
{
    [SerializeField]
    private Character _character;
    [SerializeField]
    private Slider _timeSlider;
    [SerializeField]
    private TMP_Text _nameText;
    private ActivityState _activityState;

    private enum ActivityState
    {
        None, Casting, Channeling
    }

    private void Process() {
        if (_activityState == ActivityState.Casting) {
            _timeSlider.value += Time.deltaTime;
            if (_timeSlider.value >= _timeSlider.maxValue)
                Stop();
        } else if (_activityState == ActivityState.Channeling) {
            _timeSlider.value -= Time.deltaTime;
            if (_timeSlider.value <= 0)
                Stop();
        }
    }

    private void Start() {
        _character.Activity.OnCast += InitializeCast;
        _character.Activity.OnChannel += InitializeChannel;
        _character.Activity.OnStopActivity += Stop;
    }

    private void FixedUpdate() {
        Process();
    }

    public void InitializeChannel(string activityName, int ticks, float intervalTimeInSeconds) {
        _timeSlider.maxValue = ticks * intervalTimeInSeconds;
        _timeSlider.value = ticks * intervalTimeInSeconds;
        _nameText.text = activityName;
        _activityState = ActivityState.Channeling;
        _timeSlider.gameObject.SetActive(true);
    }

    public void InitializeCast(string activityName, float castTimeInSeconds) {
        _timeSlider.maxValue = castTimeInSeconds;
        _timeSlider.value = 0;
        _nameText.text = activityName;
        _activityState = ActivityState.Casting;
        _timeSlider.gameObject.SetActive(true);
    }

    public void Stop() {
        _activityState = ActivityState.None;
        _timeSlider.gameObject.SetActive(false);
    }

    public void Initialize(Character character) {
        _character = character;
    }
}