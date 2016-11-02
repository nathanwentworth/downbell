using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class manager : MonoBehaviour {

  private bool
    paused,
    over,
    newHighScore,
    reset,
    timerStart;
  private int
    highscore,
    localScore;
  private float resetTimer;
  public GameObject
    pauseMenu, 
    gameOverScreen, 
    resumeGame, 
    restartGame;
  public Text
    highScoreText,
    highScoreDisplay,
    newHighScoreText,
    resetText;
  public Image resetSlider;

  void Start() {
    paused = false;
    over = false;
    newHighScore = false;
    pauseMenu.SetActive(false);
    Time.timeScale = 1f;
    gameOverScreen.GetComponent<CanvasGroup>().alpha = 0;
    gameOverScreen.GetComponent<CanvasGroup>().interactable = false;
    gameOverScreen.SetActive(false);
    if (File.Exists(Application.persistentDataPath + "/highscores.dat")) {
      Load();
    } else {
      Save();
    }
  }

  void Update() {
    if (timerStart) {
      resetTimer += Time.deltaTime;
      resetSlider.fillAmount = resetTimer / 3;
      if (resetTimer > 3) {
        highscore = 0;
        Save();
        resetText.text = "HIGHSCORE CLEARED!";
        timerStart = false;
      }
    }
    if (Input.GetButtonDown("Pause") && !over) {
      Pause();
    }
  }

  public void AddToList(int score) {
    localScore = score;
    if (score > highscore) {
      highscore = score;
      newHighScore = true;
    }
    Save();
  }

  public void Over() {
    over = true;
    gameOverScreen.SetActive(true);
    gameOverScreen.GetComponent<CanvasGroup>().alpha = 1;
    gameOverScreen.GetComponent<CanvasGroup>().interactable = true;
    EventSystem.current.SetSelectedGameObject(restartGame);
    highScoreText.text = "YOU GOT " + localScore + " POINTS";
    highScoreDisplay.text = "HIGHSCORE: " + highscore;
    if (newHighScore) newHighScoreText.text = "NEW HIGH\nSCORE!";
    else newHighScoreText.text = "";
  }
  public void Restart() {
    Save();
    SceneManager.LoadScene("main");
  }
  public void Quit() {
    Application.Quit();
  }
  public void Pause() {
    if (!paused) {
      pauseMenu.SetActive(true);
      paused = true;
      EventSystem.current.SetSelectedGameObject(resumeGame);
      Time.timeScale = 0f;
    }
    else {
      pauseMenu.SetActive(false);
      paused = false;
      Time.timeScale = 1f;
    }
  }
  public void Reset(bool reset) {
    print (reset);
    timerStart = reset;
  }
  public void Save() {
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Open(Application.persistentDataPath + "/highscores.dat", FileMode.Create);

    HighScores data = new HighScores();
    data.highscore = highscore;

    bf.Serialize(file, data);
    file.Close();
    
    print("saved");
  }

  public void Load() {
    if (File.Exists(Application.persistentDataPath + "/highscores.dat")) {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(Application.persistentDataPath + "/highscores.dat", FileMode.Open);
      HighScores data = (HighScores)bf.Deserialize(file);
      file.Close();
      highscore = data.highscore;
      print ("Successfully loaded, highscore is: " + highscore);
    }
  }
}

[Serializable]
class HighScores {
  public int highscore;
}
