using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Requesting : MonoBehaviour
{
    public GameObject[] objectArr;
    public GameObject[] prefabCarList;
    public int amountOfCars;
    public  List<Vector3> targetPositions = new List<Vector3>();
    public string test;
    void Start() {

        StartCoroutine(loopRequests());
        //StartCoroutine(GetPositions());
        
        // string json = "{\"cars\":[{\"id\":0,\"x\":0,\"y\":0},{\"id\":1,\"x\":0,\"y\":0},{\"id\":2,\"x\":0,\"y\":0},{\"id\":3,\"x\":0,\"y\":0},{\"id\":4,\"x\":0,\"y\":0},{\"id\":5,\"x\":0,\"y\":0},{\"id\":6,\"x\":0,\"y\":0},{\"id\":7,\"x\":0,\"y\":0},{\"id\":8,\"x\":0,\"y\":0},{\"id\":9,\"x\":0,\"y\":0},{\"id\":10,\"x\":0,\"y\":0},{\"id\":11,\"x\":0,\"y\":0},{\"id\":12,\"x\":0,\"y\":0},{\"id\":13,\"x\":0,\"y\":0},{\"id\":14,\"x\":0,\"y\":0}],\"lights\":[{\"id\":0,\"state\":\"YELLOW_COLOR\"},{\"id\":1,\"state\":\"YELLOW_COLOR\"},{\"id\":2,\"state\":\"YELLOW_COLOR\"},{\"id\":3,\"state\":\"YELLOW_COLOR\"}]}";


        // Data infoSimul = JsonUtility.FromJson<Data>(json);

        // Debug.Log(infoSimul.cars.Length);
        // amountOfCars=infoSimul.cars.Length;
        // for(int car = 0; car<amountOfCars;car++){
        //       Debug.Log("Spawn");
        //       Instantiate(prefabCarList[0], new Vector3(0, 0, 0), Quaternion.identity);
        // }
        
    }
    
    IEnumerator loopRequests(){
        while(true){
            StartCoroutine(GetPositions());
            Debug.Log("Nuevo Request realizado");
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator GetPositions() {
        float inicio = Time.time;
        
        print("Haciendo request");
        Data infoSimul;
        //Hacemos request a server local de python
        using(UnityWebRequest requestPositions = UnityWebRequest.Get("http://localhost:8080/")){
            yield return requestPositions.SendWebRequest();
            if (requestPositions.result != UnityWebRequest.Result.Success) {
                Debug.Log(requestPositions.error);
            }else{
                //Show results as text
                Debug.Log(requestPositions.downloadHandler.text);
                Debug.Log("Request succesful!");
            }
            infoSimul=JsonUtility.FromJson<Data>(requestPositions.downloadHandler.text);   
        }


        int numObj=0;
        //Debug.Log(positionArray.GetType());
        amountOfCars = infoSimul.cars.Length;
        Debug.Log(amountOfCars);
        //Debug.Log(positionArray.data);
        // for(int car = 0; car<amountOfCars;car++){
        //      Debug.Log("Spawn");
        //      Instantiate(prefabCarList[car], new Vector3(0, 0, 0), Quaternion.identity);
        // }
        //Iteramos sobre arreglo de positions
        foreach(Car c in infoSimul.cars){
            //float step = objectSpeed*Time.deltaTime;
            //Vector3 currentPos = objectArr[numObj].transform.position;
            Vector3 posObj = new Vector3(c.x,c.z,c.y);//invertimos z,y para que no se muevan verticalmente
            objectArr[c.id].transform.position=posObj; //Se teletransporta
            //targetPositions.Add(posObj);
            Debug.Log("Car:"+c.id+"moved to"+posObj);
            //numObj++;
        }
        
        
        
        // float total = Time.time - inicio;
        // print("tomo: " + total);
    }
    void Update() {
        // int numObj=1;
        // foreach(Vector3 a in targetPositions){
        //     float step = objectSpeed*Time.deltaTime;
        //     Vector3 currentPos = objectArr[numObj].transform.position;
        //     Vector3 posObj = new Vector3(a.x,a.y,a.z);
        //     objectArr[numObj].transform.position = Vector3.MoveTowards(currentPos,posObj,step);
        //     numObj++;
        // }
    }

}
