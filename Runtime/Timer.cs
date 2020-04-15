using System;
using System.Collections;
using UnityEngine;

namespace StudioName.Runtime {
    /// <summary>
    /// Will countdown from <see cref="EndTime"/> and finish when <see cref="ElapsedTime"/> == 0
    /// </summary>
    public class Timer : ICountdownable {
        /// <summary>
        /// Fired when the timer finishes
        /// </summary>
        public event Action OnFinish;
        private float elapsedTime;
        private float endTime;
        private bool running;
        private bool isFinished;
        
        //TODO reliance on mono sucks find some way around this when you have more time
        private MonoBehaviour mono;
        
        /// <summary>
        /// Creates a timer from a mono (used for the coroutine)
        /// </summary>
        /// <param name="mono">Any mono in order to run the coroutine</param>
        public Timer(MonoBehaviour mono) {
            this.mono = mono;
        }
        
        /// <summary>
        /// Begin counting down the timer.
        /// </summary>
        /// <param name="endTime">The point to start counting down from</param>
        public void Start(float endTime) {
            if (running) {
                Debug.LogWarning("Timer running, wait till it's finished to start another one.");
            }
            this.endTime = endTime;
            elapsedTime = endTime;
            isFinished = false;
            mono.StartCoroutine(StarTimer(endTime));
        }

        /// <inheritdoc cref="Start"/>
        /// <returns>IEnumerator for coroutine</returns>
        private IEnumerator StarTimer(float endTime) {
            running = true;
            while (elapsedTime > 0) {
                elapsedTime -= Time.deltaTime;
                yield return null;
            }
            elapsedTime = 0;
            isFinished = true;
            running = false;
            OnFinish?.Invoke();
        }

        public bool IsFinished {
            get { return isFinished; }
        }

        public float ElapsedTime {
            get { return elapsedTime; }
        }

        public float EndTime {
            get { return endTime; }
        }

        public bool IsRunning {
            get { return running; }
        }
    }
}