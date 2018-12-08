using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "DamageUp")
        {
            StartCoroutine(GameLogic.instance.DamageBuff());
            Destroy(other.gameObject);
        }
        if (other.tag == "FirerateUp")
        {
            StartCoroutine(GameLogic.instance.FirerateBuff());
            Destroy(other.gameObject);
        }

    }





}
