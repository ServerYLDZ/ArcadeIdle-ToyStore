
using UnityEngine;
[System.Serializable]
public class WorkerData 
{
    public int index;
    public int workerLevel;
    public float[] position= new float[3];

    public WorkerData(Worker worker){
    index=worker.index;
    workerLevel=worker.workerLevel;
    Vector3 wPos=worker.transform.position;
    position[0]=wPos.x;
      position[1]=wPos.y;
        position[2]=wPos.z;
        
    }
   
}
