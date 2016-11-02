using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class backgroundScroll : MonoBehaviour {

  private int pooledAmt = 5;

  public GameObject background;

  List<GameObject> backgrounds;

	void Start () {
    backgrounds = new List<GameObject>();
    for (int i = 0; i < pooledAmt; i++) {
      GameObject obj = (GameObject)Instantiate(background);
      obj.SetActive(false);
      backgrounds.Add(obj);
    }
    for (int i = 0; i < backgrounds.Count; i++) {
      if (!backgrounds[i].activeInHierarchy) {
        if (i == 0) {
          backgrounds[i].transform.position = transform.position;
        }
        else {
          backgrounds[i].transform.position = new Vector3(0, (backgrounds[i-1].transform.position.y - 9.8f), 10);          
        }
        backgrounds[i].transform.rotation = transform.rotation;
        backgrounds[i].SetActive(true);
      }
    }

	}
  public void Scroll() {
    for (int i = 0; i < backgrounds.Count; i++) {
      if (!backgrounds[i].activeInHierarchy) {
        backgrounds[i].transform.position = transform.position;
        backgrounds[i].transform.rotation = transform.rotation;
        backgrounds[i].SetActive(true);
        break;
      }
    }
  }
}
