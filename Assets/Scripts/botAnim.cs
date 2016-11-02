using UnityEngine;
using System.Collections;

public class botAnim : MonoBehaviour {
	
	float lerpTime = 1f;
    float currentLerpTime;
 
    float moveDistance = 1f;
 
    Vector3 startPos;
    Vector3 endPos;
    private bool run;
 
    void OnEnable() {
		currentLerpTime = 0f;
        startPos = transform.position;
        endPos = transform.position;
		endPos.x = endPos.x + Random.Range(-2f, 2f);
		if (endPos.x > 2.5f) endPos.x = 2.5f;
		else if (endPos.x < -2.5f) endPos.x = -2.5f;
        run = true;
        print ("bot has been enabled, run is now: " + run);
    }
 
    void Update() { 
        if (run) {
            //increment timer once per frame
            currentLerpTime += (Time.deltaTime / 2);
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
    
            //lerp!
            float t = currentLerpTime / lerpTime;
            t = t*t*t * (t * (6f*t - 15f) + 10f);
            transform.position = Vector3.Lerp(startPos, endPos, t);       
        }
    }
    void OnDisable() {
        run = false;
        print ("bot has been disabled, run is now: " + run);
    }
}
