
using UnityEngine;

public class RearWheelDrive : MonoBehaviour
{
    #region --- helper ---
    [System.Serializable]
    public struct WheelInfo
    {
        public Transform visualwheel;
        public WheelCollider wheelcollider;
    }
    #endregion 

    public float motor = 800;
    public float steer = 50;
    public float brake = 440;
    public WheelInfo FL;
    public WheelInfo FR;
    public WheelInfo BL;
    public WheelInfo BR;

    private void Start()
    {
        GetTheWheels();
    }
    private void FixedUpdate()
    {
        //steer and accelerate car (wasd, arrows, leftanalog gamepad)
        float vert = Input.GetAxis("Vertical");  //-1..0..1
        float horz = Input.GetAxis("Horizontal");
        FL.wheelcollider.steerAngle = horz * steer;
        FR.wheelcollider.steerAngle = horz * steer;
        BL.wheelcollider.motorTorque = vert * motor;
        BR.wheelcollider.motorTorque = vert * motor;

        //brake car
        if (Input.GetButton("Fire1") == true) //leftctrl, mouseleftbutton, gamepad A
        {
            FL.wheelcollider.brakeTorque = brake;
            FR.wheelcollider.brakeTorque = brake;
            BL.wheelcollider.brakeTorque = brake;
            BR.wheelcollider.brakeTorque = brake;
        }
        else
        {
            FL.wheelcollider.brakeTorque = 0;
            FR.wheelcollider.brakeTorque = 0;
            BL.wheelcollider.brakeTorque = 0;
            BR.wheelcollider.brakeTorque = 0;
        }

        UpdateVisualWheels();
    }
    private void UpdateVisualWheels()
    {
        Vector3 pos;
        Quaternion rot;

        FL.wheelcollider.GetWorldPose(out pos, out rot);
        FL.visualwheel.position = pos;
        FL.visualwheel.rotation = rot;

        FR.wheelcollider.GetWorldPose(out pos, out rot);
        FR.visualwheel.position = pos;
        FR.visualwheel.rotation = rot;

        BL.wheelcollider.GetWorldPose(out pos, out rot);
        BL.visualwheel.position = pos;
        BL.visualwheel.rotation = rot;

        BR.wheelcollider.GetWorldPose(out pos, out rot);
        BR.visualwheel.position = pos;
        BR.visualwheel.rotation = rot;
    }
    private void GetTheWheels()
    {
        GameObject wheels = GetChildByName(this.gameObject, "Wheels");        
        FL.visualwheel = GetChildByName(wheels, "FL").transform;
        FR.visualwheel = GetChildByName(wheels, "FR").transform;
        BL.visualwheel = GetChildByName(wheels, "BL").transform;
        BR.visualwheel = GetChildByName(wheels, "BR").transform;

        GameObject colliders = GetChildByName(this.gameObject, "Colliders");
        FL.wheelcollider = GetChildByName(colliders, "wcFL").GetComponent<WheelCollider>();        
        FR.wheelcollider = GetChildByName(colliders, "wcFR").GetComponent<WheelCollider>();
        BL.wheelcollider = GetChildByName(colliders, "wcBL").GetComponent<WheelCollider>();
        BR.wheelcollider = GetChildByName(colliders, "wcBR").GetComponent<WheelCollider>();
    }
    private GameObject GetChildByName(GameObject go, string name)
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            if (go.transform.GetChild(i).name == name)  //case sensitive
            {
                return go.transform.GetChild(i).gameObject;
            }
        }

        Debug.LogError("ERR: Could not find child gameobject " + name + ". Check spelling and case.");
        return null;
    }
}