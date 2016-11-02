using UnityEngine;
using System.Collections;
using System;

public class highscores : IComparable<highscores> {
    public int score;
    
    public highscores(int newScore) {
        score = newScore;
    }
    
    public int CompareTo(highscores other) {
        if (other == null) {
            return 1;
        }
        return 0;
    }
}