using UnityEngine;

public class BrickBehaviour : MonoBehaviour
{

    public int HitPoints;
    float MinHP;
    float MaxHP;
    float hpoffset;
    public TMPro.TextMeshPro hpDisplay;

    public void Start()
    {
        MinHP = GameLogic.instance.MinHP;
        MaxHP = GameLogic.instance.MaxHP;
        HitPoints = Random.Range((int)MinHP, (int)MaxHP);
        hpoffset = (MaxHP - MinHP) / 4;
        ColorChange();

        hpDisplay.text = HitPoints + "";
    }


    void ColorChange()
    {
        if (HitPoints < MinHP + hpoffset)
        {
            GetComponent<MeshRenderer>().material = GameLogic.instance.colorDifficulty[0];
        }
        else if (HitPoints < MinHP + hpoffset * 2)
        {
            GetComponent<MeshRenderer>().material = GameLogic.instance.colorDifficulty[1];
        }
        else if (HitPoints <= MinHP * hpoffset * 3)
        {
            GetComponent<MeshRenderer>().material = GameLogic.instance.colorDifficulty[2];
        }
        else
        {
            GetComponent<MeshRenderer>().material = GameLogic.instance.colorDifficulty[3];
        }
    }


    public void OnHit()
    {
        int damage = GameLogic.instance.buffedDamage;

        HitPoints -= damage;
        if (HitPoints <= 0)
            OnDeath();
        else
            hpDisplay.text = HitPoints + "";

        ColorChange();

        GameLogic.instance.AddScore(damage);

    }

    public void OnDeath()
    {
        Destroy(gameObject);
        if (GameLogic.instance.LineKillChance > Random.value)
        {
            if (transform.parent.childCount > 3)
                Destroy(this.transform.parent.gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Bullet")
        {
            OnHit();
            Destroy(other.gameObject);
        }
        if (other.transform.tag == "Player")
        {
            Debug.Log("dead");
        }
    }
}
