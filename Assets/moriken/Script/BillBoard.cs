using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public float nowPosi;
    private void Start()
    {
        nowPosi = gameObject.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, nowPosi + Mathf.Sin(2 * Time.time) / 5, transform.position.z);

        Vector3 p = Camera.main.transform.position;
        //p.y = transform.position.y;
        transform.LookAt(p); 
    }
}
