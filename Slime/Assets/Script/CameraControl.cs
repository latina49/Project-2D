using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;
    public GameObject Character;
    public Vector3 offset;
    public Vector3 LocationCamera;
    public Vector3 LocationCharacter;
    public float smooth = 10;
    // Start is called before the first frame update
    public void Init()
    {
        gameObject.transform.position = DataStore.instance.map.locationCamera;
        offset = transform.position - Character.transform.position;
    }

    // Update is called once per frame
    public void LateUpDate()
    {
        float x = Mathf.Clamp(Character.transform.position.x + offset.x, 0, 31);
        float y = Mathf.Clamp(Character.transform.position.y + offset.y, 5, 7.65f);
        Vector3 targetPosition = new Vector3(Character.transform.position.x, Character.transform.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, targetPosition+offset, Time.deltaTime * smooth);
        LocationCamera = transform.position;
        LocationCharacter = Character.transform.position;
    }
    
}
