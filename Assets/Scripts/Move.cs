using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 movementSpeed;
    public Space space;

    void Update()
    {
        transform.Translate(movementSpeed * Time.deltaTime, space);
    }
}