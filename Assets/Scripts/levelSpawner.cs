using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class levelSpawner : MonoBehaviour {

  public GameObject[] nodes;
  public Vector3[] nodePos;
  public GameObject player;
  public GameObject cam;
  public GameObject bot;

  private int pooledAmt = 6;

  List<GameObject> bots;


	void Start () {
    // makes an array for storing the positions of the spawning nodes
    nodePos = new Vector3[nodes.Length];
    // gets position of empty game objects, returns their vector3 pos
    for (int i = 0; i < nodes.Length; i++) {
      nodePos[i] = nodes[i].transform.position;
    }

    bots = new List<GameObject>();
    for (int i = 0; i < pooledAmt; i++) {
      GameObject obj = (GameObject)Instantiate(bot);
      obj.SetActive(false);
      bots.Add(obj);
    }

    for (int i = 0; i < bots.Count; i++) {
      if (!bots[i].activeInHierarchy) {
        if (i == 0) {
          bots[i].transform.position = new Vector3(Random.Range(-2f, 2f), cam.transform.position.y - Random.Range(2.2f, 4.8f), 0);
        }
        else {
          bots[i].transform.position = new Vector3(Random.Range(-3f, 2f), (bots[i-1].transform.position.y - 9.8f), 0);        }
        bots[i].transform.rotation = transform.rotation;
        bots[i].SetActive(true);
        break;
      }
    }

    InvokeRepeating("SpawnBot", 3.5f, 8.5f);
	}

	void Update () {
    if (player != null) {
      if (player.transform.position.y < (cam.transform.position.y - 1)) {
        // if the player is one unit below the center
        // of the screen, spawn platforms below the camera
      }
    }
	}
  public void SpawnBot() {
    for (int i = 0; i < bots.Count; i++) {
      if (!bots[i].activeInHierarchy) {
        bots[i].transform.position = new Vector3(Random.Range(-2f, 2f), cam.transform.position.y - Random.Range(4.8f, 6.5f), 0);
        bots[i].transform.rotation = transform.rotation;
        bots[i].transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().enabled = true;
        bots[i].transform.GetChild(1).gameObject.GetComponent<BoxCollider2D>().enabled = true;
        bots[i].SetActive(true);
        break;
      }
    }
  }
}
