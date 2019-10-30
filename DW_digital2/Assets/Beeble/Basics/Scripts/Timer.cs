using UnityEngine;
using UnityEngine.Events;

namespace Beeble
{
    public class Timer
    {
        [SerializeField]
        public float duration;
        [SerializeField]
        bool looping;
        
        bool active;
        float startTime;

        public bool Completed { get => Time.time - startTime > duration; }
        public float Percent { get => Mathf.Clamp01((Time.time - startTime) / duration); }
        public float StartTime { get => startTime; }

        public UnityEvent OnComplete;

        public Timer()
        {
            duration = 0;
            looping = false;
            active = false;
            startTime = Time.time;
            OnComplete = new UnityEvent();
        }

        public Timer(float duration)
        {
            this.duration = duration;
            looping = false;
            active = false;
            startTime = Time.time;
            OnComplete = new UnityEvent();
        }

        public void Start()
        {
            startTime = Time.time;
            active = true;
        }

        public void Tick()
        {
            if (active && Completed) {
                OnComplete.Invoke();
                active = false;
            }
        }
    }
}
