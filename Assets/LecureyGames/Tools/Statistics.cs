using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace LecureyGames.Utilities {
    public static class Statistics {
        public static void MeasureTime(Action action) {
            string actionName = ActionHelper.GetMethodName(action);
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            Debug.Log($"{actionName} : {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}