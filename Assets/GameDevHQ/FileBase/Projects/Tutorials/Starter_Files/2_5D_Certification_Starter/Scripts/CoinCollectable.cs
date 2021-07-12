using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectable : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 2.0f;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, _rotationSpeed * Time.deltaTime, 0));
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player==null)
            {
                Debug.LogError("Error: Player es null");
            }

            player.AddCoins();
            Destroy(this.gameObject);

        }
    }
}
