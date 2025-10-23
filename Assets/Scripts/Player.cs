using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    public float speed;
    float horizontal;

    Animator anim;
    bool freezePlayer;

  
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(freezePlayer == true)
        {
            return;
        }

        horizontal = Input.GetAxis("Horizontal");

        anim.SetFloat("Horizontal", horizontal);

        Vector3 moveDirection = new Vector2(horizontal, 0);
        transform.position += moveDirection * Time.deltaTime * speed;
    }

    public void FreezePlayer(bool value)
    {
        freezePlayer = value;
    }
}
