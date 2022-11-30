using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardView : MonoBehaviour
{
    
    public bool OnRoad;
    public LayerMask roadLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Physics.Raycast(this.gameObject.transform.position, Vector3.down, 50, roadLayer))
        {
            OnRoad = true;
        }
        else OnRoad = false;
    }
}
