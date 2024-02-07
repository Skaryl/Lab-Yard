using System.Runtime.CompilerServices;
using UnityEngine;

namespace LecureyGames.Utilities {
    public static class ValidationHelper {
        public static void CheckNull<T>(T obj, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) {
            if (obj == null) {
                Debug.Log($"Null Error: ({typeof(T)}) in {file} Zeile {line}");
            } else {
                Debug.Log($"Object {obj} NullCheck: Passed ({typeof(T)}) in {file} Zeile {line}");
            }
        }
    }
}