using UnityEngine;
using System.Collections;

public class camFollow : MonoBehaviour {

  public GameObject player;
  private float smoothing = 5f;

  Vector3 offset;

	void Start () {
    offset = new Vector3(0, transform.position.y - player.transform.position.y, 0);
	}
	
	void FixedUpdate () {
    if (player != null) {
      Vector3 targetCamPos = new Vector3 (0, player.transform.position.y);
      transform.position = Vector3.Lerp (transform.position, targetCamPos + offset, smoothing * Time.deltaTime); 
    }
	}
}
