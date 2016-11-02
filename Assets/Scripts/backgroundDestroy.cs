using UnityEngine;
using System.Collections;

public class backgroundDestroy : MonoBehaviour {

  public backgroundScroll backgroundScroll;

  void OnTriggerEnter2D(Collider2D hit) {
    if (hit.gameObject.tag == "background") {
      gameObject.SetActive(false);
      backgroundScroll.Scroll();
    }
  }
}
