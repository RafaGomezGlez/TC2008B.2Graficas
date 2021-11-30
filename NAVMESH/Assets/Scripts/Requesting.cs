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

        //StartCoroutine(GetPositions());
        StartCoroutine(GetPositions());

        // test de parsing
            string json = "{"+
        "\"data\": ["+
            "{\"x\":0, \"y\":1, \"z\":2},"+
            "{\"x\":3, \"y\":4, \"z\":5},"+
            "{\"x\":6, \"y\":7, \"z\":8}"+
        "]"+
    "}";
        Data posiciones = JsonUtility.FromJson<Data>(json);
        
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
        Data positionArray;
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
            positionArray=JsonUtility.FromJson<Data>(requestPositions.downloadHandler.text);
            
        }


        //int numObj=0;
        //Debug.Log(positionArray.GetType());
        amountOfCars = positionArray.data.Length;
        Debug.Log(amountOfCars);
        // for(int car = 0; car<amountOfCars;car++){
        //      Debug.Log("Spawn");
        //      Instantiate(prefabCarList[car], new Vector3(0, 0, 0), Quaternion.identity);
        // }
        // //Iteramos sobre arreglo de positions
        // foreach(Position p in positionArray.data){
        //     //float step = objectSpeed*Time.deltaTime;
        //     //Vector3 currentPos = objectArr[numObj].transform.position;
        //     Vector3 posObj = new Vector3(p.x,p.y,p.z);
        //     //objectArr[numObj].transform.position=posObj; //Se teletransporta
        //     targetPositions.Add(posObj);
        //     Debug.Log(posObj);
        //     numObj++;
        // }
        
        
        
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
