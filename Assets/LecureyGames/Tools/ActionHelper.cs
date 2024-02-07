using System;
using System.Reflection;

namespace LecureyGames.Utilities {
    public static class ActionHelper {
        public static string GetMethodName(Action action) {
            if (action == null)
                return "Unknown";

            MethodInfo methodInfo = action.Method;
            string methodName = methodInfo.Name;

            // If the method is an anonymous method, try to get the name from the source code
            if (methodName.Contains(">")) {
                string[] tokens = methodName.Split('>');
                if (tokens.Length >= 2) {
                    methodName = tokens[1];
                }
            }

            return methodName;
        }
    }
}