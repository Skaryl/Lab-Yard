using System;
using System.Collections.Generic;
using UnityEngine;

namespace LecureyGames.Utilities {
    public class FunctionTimer {
        private static List<FunctionTimer> timerList;
        private static GameObject initGameObject;

        private static void InitIfNeeded() {
            if (initGameObject != null)
                return;
            initGameObject = new("FunctionTimer_Global");
            timerList = new();
        }

        public static FunctionTimer Create(Action action, float timer) {
            return Create(action, timer, "", false, false);
        }
        public static FunctionTimer Create(Action action, float timer, string functionName) {
            return Create(action, timer, functionName, false, false);
        }

        public static FunctionTimer Create(Action action, float timer, string functionName, bool useUnscaledDeltaTime) {
            return Create(action, timer, functionName, useUnscaledDeltaTime, false);
        }

        public static FunctionTimer Create(Action action, float timer, string functionName, bool useUnscaledDeltaTime, bool stopAllWithSameName) {
            InitIfNeeded();

            if (stopAllWithSameName)
                StopAllTimersWithName(functionName);

            GameObject gameObject = new($"FunctionTimer Object {functionName}", typeof(MonoBehaviourHook));
            FunctionTimer functionTimer = new(gameObject, action, timer, functionName, useUnscaledDeltaTime);
            gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;

            timerList.Add(functionTimer);

            return functionTimer;
        }

        private static void RemoveTimer(FunctionTimer functionTimer) {
            InitIfNeeded();
            timerList.Remove(functionTimer);
        }

        public static List<FunctionTimer> GetTimersByName(string functionName) {
            List<FunctionTimer> timers = new();
            foreach (FunctionTimer timer in timerList)
                if (timer.functionName == functionName)
                    timers.Add(timer);
            return timers;
        }

        public static void PauseTimersWithName(string functionName, bool pauseOnlyFirst = false) {
            for (int i = timerList.Count - 1; i >= 0; i--) {
                if (timerList[i].functionName == functionName) {
                    timerList[i].PauseTimer();
                    if (pauseOnlyFirst)
                        return;
                }
            }
        }

        public static void ResumeTimersWithName(string functionName, bool onlyFirst = false) {
            for (int i = timerList.Count - 1; i >= 0; i--) {
                if (timerList[i].functionName == functionName) {
                    timerList[i].ResumeTimer();
                    if (onlyFirst)
                        return;
                }
            }
        }

        public static void StopAllTimersWithName(string functionName) {
            for (int i = timerList.Count - 1; i >= 0; i--) {
                if (timerList[i].functionName == functionName) {
                    timerList[i].DestroySelf();
                }
            }
        }
        public static void StopFirstTimerWithName(string functionName) {
            for (int i = 0; i < timerList.Count; i++) {
                if (timerList[i].functionName == functionName) {
                    timerList[i].DestroySelf();
                    return;
                }
            }
        }
        public static void StopLastTimerWithName(string functionName) {
            for (int i = timerList.Count - 1; i >= 0; i--) {
                if (timerList[i].functionName == functionName) {
                    timerList[i].DestroySelf();
                    return;
                }
            }
        }
        public static void StopAllTimers() {
            for (int i = timerList.Count - 1; i >= 0; i--) {
                timerList[i].DestroySelf();
            }
        }

        private class MonoBehaviourHook : MonoBehaviour {
            public Action onUpdate;
            private void Update() {
                if (onUpdate == null)
                    return;
                onUpdate();
            }
        }

        private GameObject gameObject;
        private float timer;
        private readonly string functionName;
        private bool active;
        private bool useUnscaledDeltaTime;
        private Action action;

        private FunctionTimer(GameObject gameObject, Action action, float timer, string functionName, bool useUnscaledDeltaTime) {
            this.gameObject = gameObject;
            this.action = action;
            this.timer = timer;
            this.functionName = functionName;
            this.useUnscaledDeltaTime = useUnscaledDeltaTime;
        }

        private void Update() {
            if (!active)
                return;

            if (useUnscaledDeltaTime)
                timer -= Time.unscaledDeltaTime;
            else
                timer -= Time.deltaTime;

            timer -= Time.deltaTime;
            if (timer <= 0) {
                action();
                DestroySelf();
            }
        }

        public void PauseTimer() {
            active = false;
        }

        public void ResumeTimer() {
            active = true;
        }

        public void DestroySelf() {
            RemoveTimer(this);
            if (gameObject != null)
                UnityEngine.Object.Destroy(gameObject);

        }
    }

    public class FunctionTimerObject {
        private Action callback;
        private float timer;

        public FunctionTimerObject(Action callback, float timer) {
            this.callback = callback;
            this.timer = timer;
        }

        public void Update() {
            Update(Time.deltaTime);
        }

        public void Update(float deltaTime) {
            timer -= deltaTime;
            if (timer <= 0) {
                callback();
            }
        }

        public static FunctionTimerObject CreateObject(Action callback, float timer) {
            return new FunctionTimerObject(callback, timer);
        }
    }
}