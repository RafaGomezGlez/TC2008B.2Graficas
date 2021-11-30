using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Follow = null;
    private Vector3 offset;

    private void Start()
    {
        offset = Follow.transform.position - this.transform.position;
    }
    private void LateUpdate()
    {
        this.transform.position = Follow.transform.position - offset;
    }
}