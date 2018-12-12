using UnityEngine;
using System.Collections.Generic;

public class BrickBehaviour : MonoBehaviour
{

    public int HitPoints;
    float MinHP;
    float MaxHP;
    float hpoffset;
    public TMPro.TextMeshPro hpDisplay;

    Stack<GameObject> tower;
    MeshRenderer mr;

    GameLogic gl;

    public void Start()
    {   
        mr = this.GetComponent<MeshRenderer>();
        gl = GameLogic.instance;
        MinHP = gl.MinHP;
        MaxHP = gl.MaxHP;
        HitPoints = Random.Range((int)MinHP, (int)MaxHP);
        tower = new Stack<GameObject>();
        for (int i = 0; i < HitPoints; i++)
        {
            Transform cube = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            cube.parent = this.transform;
            cube.localScale = Vector3.one;
            cube.position = this.transform.position + Vector3.up * i * 2;
            cube.GetComponent<MeshRenderer>().material = mr.material;
            tower.Push(cube.gameObject);
        }
        hpoffset = (MaxHP - MinHP) / gl.difcolors.Length;
        ColorChange();

        hpDisplay.text = HitPoints + "";
    }


    void ColorChange()
    {
        for (int i = 1; i < gl.difcolors.Length; i++)
        {
            if (HitPoints < MinHP + hpoffset * i)
            {
                mr.material.color = gl.difcolors[i - 1];
                return;
            }
            mr.material.color = gl.difcolors[gl.difcolors.Length - 1];

        }
    }


    public void OnHit()
    {
        int damage = gl.buffedDamage;

        HitPoints -= damage;
        if (HitPoints <= 0)
            OnDeath();
        else
        {
            hpDisplay.text = HitPoints + "";
            for (int i = 0; i < damage; i++)
            {

                Destroy(tower.Pop());

            }
        }

        ColorChange();

        gl.AddScore(damage);

    }

    public void OnDeath()
    {
        Destroy(gameObject);
        if (gl.LineKillChance > Random.value)
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
