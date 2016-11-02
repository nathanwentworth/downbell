using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerInteractions : MonoBehaviour {

  public levelSpawner levelSpawner;
  public timer timer;
  public manager manager;
  public Text scoreText;

  public AudioClip bellSound;

  private float speed = 5;

  public int score;
	
  void Start() {
    score = 0;
    scoreText.text = score + "";
    timer.timeStart = true;
  }

	void FixedUpdate () {
    float movementHorz = Input.GetAxis("Horizontal");
    Vector3 movement = new Vector3 (movementHorz, 0, 0);

    Vector3 v = GetComponent<Rigidbody2D>().velocity;
    v.x = movement.x * speed;
    GetComponent<Rigidbody2D>().velocity = v;
	
	}

  void OnCollisionEnter2D(Collision2D hit) {
    if (hit.gameObject.tag == "bot") {
      levelSpawner.SpawnBot();
      score++;
      scoreText.text = score + "";
      if (hit.transform.GetComponent<AudioSource>() != null) {
        hit.transform.GetComponent<AudioSource>().PlayOneShot(bellSound, 1);        
      } else {
        print ("no audio source attached!");
      }
      hit.transform.gameObject.GetComponent<Animator>().SetTrigger("shake");
      hit.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
      hit.transform.parent.GetChild(1).gameObject.GetComponent<BoxCollider2D>().enabled = false;
      StartCoroutine(SetInactive(hit));
    }
    if (hit.gameObject.tag == "destroyer") {
      timer.timeStart = false;
      manager.AddToList(score);
      manager.Over();
      gameObject.SetActive(false);
    }
  }

  IEnumerator SetInactive(Collision2D hit) {
    yield return new WaitForSeconds(2f);
    hit.transform.parent.gameObject.SetActive(false);
  }
}
