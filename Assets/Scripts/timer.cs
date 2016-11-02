using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timer : MonoBehaviour {

  private float timeVal;
  public bool timeStart;
  private float min;

  private string secStr;
  private string minStr;

	void Start () {
    timeVal = 0f;
	}
	
	void Update () {
    if (timeStart) {
      timeVal += Time.deltaTime;
      if (timeVal > 60f) {
        timeVal = 0f;
        min++;
      }
      if (timeVal < 10) {
        secStr = FormatTime(timeVal);
      } else {
        secStr = timeVal.ToString();
      }
      if (min < 10) {
        minStr = FormatTime(min);
      } else {
        minStr = min.ToString();
      }
      GetComponent<Text>().text = minStr + ":" + secStr;
    }
	}

  string FormatTime(float t) {
    string s = t.ToString();
    s = "0" + s;
    return s;
  }
}
