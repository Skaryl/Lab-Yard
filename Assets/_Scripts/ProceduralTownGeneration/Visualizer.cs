using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour {
    public LSystemGenerator lSystem;
    private HashSet<Vector3> positions;
    public RoadHelper roadHelper;

    [SerializeField] private bool usePrebuildSequence = false;
    [SerializeField] private TextAsset sequenceFile;

    [Range(1, 50)] public int length = 25;
    private float angle;
    [Range(1, 15)] public int lengthShortening = 5;
    [SerializeField] private bool randomLengthChanging = false;
    [SerializeField] private int randomRangeMin = -3;
    [SerializeField] private int randomRangeMax = 7;

    [Range(0, 10)][SerializeField] private int minimalSpacingBetweenParallelStreets = 2;
    [Range(3, 10)][SerializeField] private int sizeOfAllowedChunks = 4;

    public int Length {
        get {
            if (length > 0)
                return length;
            else
                return 1;
        }
        set => length = value;
    }

    private void Start() {
        angle = 90f;
        positions = new();
        string sequence;
        if (usePrebuildSequence) {
            sequence = sequenceFile.text;
        } else {
            sequence = lSystem.GenerateSentence();
        }

        Debug.Log($"Sequence: {sequence}");
        GenerateStreetPositionsFromSequence(sequence);
        roadHelper.AdjustRoadMap(minimalSpacingBetweenParallelStreets, sizeOfAllowedChunks);
        roadHelper.VisualizeRoadPositions();
    }

    private void GenerateStreetPositionsFromSequence(string sequence) {
        Stack<AgentParameters> savePoints = new();
        Vector3 currentPosition = Vector3.zero;

        Vector3 direction = Vector3.forward;
        Vector3 tempPosition;

        positions.Add(currentPosition);

        Debug.Log($"Visualizing: {sequence}");
        foreach (char letter in sequence) {
            EncodingLetters encoding = (EncodingLetters)letter;
            switch (encoding) {
                case EncodingLetters.save:
                    savePoints.Push(new AgentParameters {
                        position = currentPosition,
                        direction = direction,
                        length = Length
                    });
                    break;
                case EncodingLetters.load:
                    if (savePoints.Count > 0) {
                        AgentParameters agentParameter = savePoints.Pop();
                        currentPosition = agentParameter.position;
                        direction = agentParameter.direction;
                        Length = agentParameter.length;
                    } else {
                        throw new System.Exception("Cannot load from empty stack");
                    }
                    break;
                case EncodingLetters.addPosition:
                    if (length < 0)
                        break;
                    tempPosition = currentPosition;
                    currentPosition += direction * length;
                    Length += randomLengthChanging ? Random.Range(randomRangeMin, randomRangeMax) : lengthShortening;
                    positions.Add(currentPosition);
                    roadHelper.AddStreetPositions(tempPosition, Vector3Int.RoundToInt(direction), Length);
                    break;
                case EncodingLetters.turnRight:
                    direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
                    break;
                case EncodingLetters.turnLeft:
                    direction = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
                    break;
                default:
                    break;
            }

        }


        Debug.Log($"{positions.Count} generated positions.");
        Debug.Log($"{roadHelper.RoadCount} roadpositions.");
        Debug.Log($"{roadHelper.RoadDoublesCount} double roadpositions.");
    }
}
