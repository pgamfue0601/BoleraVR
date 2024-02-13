using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLookingAtPlayer : MonoBehaviour
{
    private Camera lookAtPlayer;
    // Start is called before the first frame update
    void Start()
    {
        lookAtPlayer = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.rotation = lookAtPlayer.transform.rotation;
    }
}
