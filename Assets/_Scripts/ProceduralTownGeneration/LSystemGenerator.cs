using System;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class LSystemGenerator : MonoBehaviour {
    public Rule[] rules;
    public string rootSentence;
    [Range(0, 10)] public int maxIterations = 5;

    public bool randomIgnoreRuleModifier = true;
    [Range(0.0f, 1.0f)]
    public float chaneToIgnoreRule = 0.3f;

    public string GenerateSentence(string word = null) {
        if (word == null) {
            word = rootSentence;
        }
        return GrowRecursive(word);
    }

    private string GrowRecursive(string word, int iterationIndex = 0) {
        if (iterationIndex >= maxIterations) {
            return word;
        }

        StringBuilder newWord = new();

        foreach (Char c in word) {
            newWord.Append(c);
            ProcessRulesRecursivelly(newWord, c, iterationIndex);
        }

        return newWord.ToString();
    }



    private void ProcessRulesRecursivelly(StringBuilder newWord, char c, int iterationIndex) {
        foreach (Rule rule in rules) {
            if (randomIgnoreRuleModifier && iterationIndex > 1 && Random.value < chaneToIgnoreRule) {
                return;
            }
            if (rule.letter == c.ToString()) {

                newWord.Append(GrowRecursive(rule.GetResult(), iterationIndex + 1));
            }
        }
    }
}