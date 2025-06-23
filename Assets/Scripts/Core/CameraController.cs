using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform camMin;
    [SerializeField] private Transform camMax;

    [SerializeField] public Transform player;
    private void Update()
    {
        //Room Camera//
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);//

        float xpos = Mathf.Clamp(player.position.x, camMin.position.x, camMax.position.x);

        //Follow Player//
        transform.position = new Vector3(xpos, transform.position.y, transform.position.z);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
