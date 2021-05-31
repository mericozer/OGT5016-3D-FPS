using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletUIFollower : MonoBehaviour
{
    [SerializeField] private Transform obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        //follow the attached object on map
        if (obj.gameObject.activeSelf == true)
        {
            Vector3 newPos = obj.position;
            newPos.y = transform.position.y;
            transform.position = newPos;
        }
    }
}
