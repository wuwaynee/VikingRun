using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject groundTile;
    Vector3 nextSpawnPoint;


    public void SpawnTile(bool spawnItems)
    {
        Quaternion[] angle = new Quaternion[3];
        angle[0] = Quaternion.Euler(0, 0, 0);
        angle[1] = Quaternion.Euler(0, 90, 0);
        angle[2] = Quaternion.Euler(0, -90, 0);

        int index = Random.Range(0, angle.Length);


        //GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, angle[index]);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
        //nextSpawnPoint = transform.position 
        if (index == 0)
        {
            nextSpawnPoint += /*transform.position*/  transform.forward;
        }
        else if (index == 1)
        {
            nextSpawnPoint += /*transform.position*/  transform.forward  - transform.right ;
        }
        else
        {
            nextSpawnPoint += /*transform.position*/  transform.forward  + transform.right ;
        }



        if (spawnItems)
        {
            temp.transform.GetChild(0).GetComponent<GroundTile>().SpawnObstacle();
            temp.transform.GetChild(0).GetComponent<GroundTile>().SpawnCoins();

            //temp.GetComponent<GroundTile>().SpawnCoins();

        }
    }
        // Start is called before the first frame update
        void Start()
        {
            nextSpawnPoint = transform.position;
            for (int i = 0; i < 150; i++)
            {
                if (i < 3)
                {
                    SpawnTile(false);
                }
                else
                {
                    SpawnTile(true);
                }
            }
        }
    
}
