using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Assertion
{
    public static bool Equal(List<int> input, List<int> expected)
    {
        var result = input.Count == expected.Count;
        result = result && !input.Where((t, i) => !t.Equals(expected[i])).Any();

        if (!result)
        {
            Display(input, expected);
        }
        return result;
    }

    public static void Display(List<int> input, List<int> expected)
    {
        Debug.LogWarning("Input: ");
        Display(input);
        Debug.LogWarning("\nExpected: ");
        Display(expected);
    }

    public static void Display(List<int> input)
    {
        var result = "[";
        input.ForEach(item => result += item);
        result += "]";
        Debug.LogWarning(result);
    }
}
