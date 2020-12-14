using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrouchCheck : MonoBehaviour {
    public Dictionary<Collider, float> data;
    public LayerMask hideMask;
    public PlayerMovement pm;
    
    // Start is called before the first frame update
    void Start() {
        data = new Dictionary<Collider, float>();
    }

    // Update is called once per frame
    void Update() {
        foreach(KeyValuePair<Collider, float> entry in data.ToList()) {
            data[entry.Key] = Vector3.Distance(transform.position, entry.Key.ClosestPoint(transform.position));
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(1<<other.gameObject.layer == hideMask.value) {
            data.Add(other, Vector3.Distance(transform.position, other.ClosestPoint(transform.position)));
        }
    }

    private void OnTriggerExit(Collider other) {
        if(data.ContainsKey(other)) {
            data.Remove(other);
            pm.hiding = false;
            pm.wasHiding = true;
        }
    }
}
