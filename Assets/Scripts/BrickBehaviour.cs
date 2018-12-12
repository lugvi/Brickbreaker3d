using UnityEngine;
using System.Collections.Generic;

public class BrickBehaviour : MonoBehaviour
{

    public int HitPoints;
    float MinHP;
    float MaxHP;
    float hpoffset;
    public TMPro.TextMeshPro hpDisplay;

    public Stack<GameObject> tower;

    public GameObject topBrick;

    public Material tempmat;

    public void Start()
    {
        MinHP = GameLogic.instance.MinHP;
        MaxHP = GameLogic.instance.MaxHP;
        HitPoints = Random.Range((int)MinHP, (int)MaxHP);
        tower = new Stack<GameObject>();
        for (int i = 0; i < HitPoints; i++)
        {

            GameObject cube = Instantiate(topBrick);
            cube.transform.parent = this.transform.parent;
            cube.transform.position = this.transform.position + Vector3.up * i * 2;//+Vector3.forward*40;
            tower.Push(cube);
        }
        hpoffset = (MaxHP - MinHP) / 4;
        ColorChange();

        hpDisplay.text = HitPoints + "";
    }


    void ColorChange()
    {
        if (HitPoints < MinHP + hpoffset)
        {
            GetComponent<MeshRenderer>().material = GameLogic.instance.colorDifficulty[0];
            tempmat.color = GameLogic.instance.colorDifficulty[0].color;
        }
        else if (HitPoints < MinHP + hpoffset * 2)
        {
            GetComponent<MeshRenderer>().material = GameLogic.instance.colorDifficulty[1];
            tempmat.color = GameLogic.instance.colorDifficulty[1].color;
        }
        else if (HitPoints <= MinHP * hpoffset * 3)
        {
            GetComponent<MeshRenderer>().material = GameLogic.instance.colorDifficulty[2];
            tempmat.color = GameLogic.instance.colorDifficulty[2].color;
        }
        else
        {
            GetComponent<MeshRenderer>().material = GameLogic.instance.colorDifficulty[3];
            tempmat.color = GameLogic.instance.colorDifficulty[3].color;
        }

    }


    public void OnHit()
    {
        int damage = GameLogic.instance.buffedDamage;

        for (int i = 0; i < damage; i++)
        {

            Destroy(tower.Pop());

        }

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
