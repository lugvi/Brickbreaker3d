using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    #region singleton
    public static GameLogic instance;
    private void Awake() { instance = this; }
    #endregion

    [Header("Prefabs")]
    public PlayerBehaviour player;
    public Transform wall;


    public Transform Gun;

    public Rigidbody Bullet;

    public Material[] colorDifficulty;
    [Header("UI")]
    public Text scoreText;

    public GameObject UpgradesPanel;
    public GameObject OptionsPanel;


    public Toggle UpgradesToggle;
    public Toggle OptionsToggle;

    public Text DamageUpgradeCostText;

    public Text CurrentDamageText;
    public Text FirerateUpgradeCostText;
    public Text CurrentFirerateText;

    public Button DamageUpgradeButton;
    public Button FirerateUpgradeButton;

    [Header("Variables")]

    [SerializeField]
    private float minHP;
    [SerializeField]
    private float maxHP;



    [SerializeField]
    private float wallSpeed;



    [SerializeField]
    private float wallSpawnDelay;



    [SerializeField]
    private float bulletVelocity;
    [SerializeField]
    private float playerSpeed;

    [SerializeField]
    private float fireRate;

    [Range(0, 1)]
    [SerializeField]
    private float lineKillChance;



    public int damage=1;

    public int damageUpgradeCost=1;
    public int firerateUpgradeCost=1;



    [Header("Powerups")]
    [Range(1, 10)]
    [SerializeField]
    private float fireRateBuff;

    public float buffedFirerate;
    [SerializeField]
    private float fireRateBuffDuration;

    [SerializeField]
    private int damageBuffValue;
    public int buffedDamage;
    [SerializeField]
    private float damageBuffDuration;

    [Range(0.1f, 100)]
    [SerializeField]
    private float powerupSpawnDelay;
    [SerializeField]
    private float powerupMoveSpeed;

    public GameObject[] Powerups;




    public int score;
#region  "encap"
    public float MinHP
    {
        get
        {
            return minHP;
        }

        set
        {
            minHP = value;
        }
    }

    public float MaxHP
    {
        get
        {
            return maxHP;
        }

        set
        {
            maxHP = value;
        }
    }

    public float WallSpeed
    {
        get
        {
            return wallSpeed;
        }

        set
        {
            wallSpeed = value;
        }
    }

    public float WallSpawnDelay
    {
        get
        {
            return wallSpawnDelay;
        }

        set
        {
            wallSpawnDelay = value;
        }
    }

    public float BulletVelocity
    {
        get
        {
            return bulletVelocity;
        }

        set
        {
            bulletVelocity = value;
        }
    }

    public float PlayerSpeed
    {
        get
        {
            return playerSpeed;
        }

        set
        {
            playerSpeed = value;
        }
    }

    public float FireRate
    {
        get
        {
            return fireRate;
        }

        set
        {
            fireRate = value;
        }
    }

    public float FireRateBuff
    {
        get
        {
            return fireRateBuff;
        }

        set
        {
            fireRateBuff = value;
        }
    }

    public float FireRateBuffDuration
    {
        get
        {
            return fireRateBuffDuration;
        }

        set
        {
            fireRateBuffDuration = value;
        }
    }

    public float DamageBuffValue
    {
        get
        {
            return damageBuffValue;
        }

        set
        {
            damageBuffValue = (int)value;
        }
    }

    public float DamageBuffDuration
    {
        get
        {
            return damageBuffDuration;
        }

        set
        {
            damageBuffDuration = value;
        }
    }

    public float PowerupSpawnDelay
    {
        get
        {
            return powerupSpawnDelay;
        }

        set
        {
            powerupSpawnDelay = value;
        }
    }

    public float PowerupMoveSpeed
    {
        get
        {
            return powerupMoveSpeed;
        }

        set
        {
            powerupMoveSpeed = value;
        }
    }

    public float LineKillChance
    {
        get
        {
            return lineKillChance;
        }

        set
        {
            lineKillChance = value;
        }
    }

    #endregion

    public void Start()
    {
        InitListeners();
        InitUi();
        InitValues();
        StartCoroutine(AutoShoot());
        StartCoroutine(SpawnWall());
        StartCoroutine(PowerupSpawner());
    }

    void InitValues()
    {
        buffedDamage = damage;
        buffedFirerate = fireRate;
    }

    public void InitListeners()
    {
        UpgradesToggle.onValueChanged.AddListener((b) =>
        {
            UpgradesPanel.SetActive(b);
            Time.timeScale = b ? 0 : 1;
            checkInteractable();
          

        });
        OptionsToggle.onValueChanged.AddListener((b) =>
        {
            OptionsPanel.SetActive(b);
            Time.timeScale = b ? 0 : 1;
            
        });

        DamageUpgradeButton.onClick.AddListener(()=>
        {
            damage++;
            buffedDamage = damage;
            AddScore(-damageUpgradeCost);
            damageUpgradeCost*=2;
            DamageUpgradeCostText.text = damageUpgradeCost+"";
            checkInteractable();
            CurrentDamageText.text = damage+"";
            
        });

       FirerateUpgradeButton.onClick.AddListener(()=>
        {
            fireRate++;
            buffedFirerate = fireRate;
            AddScore(-firerateUpgradeCost);
            firerateUpgradeCost*=2;
            FirerateUpgradeCostText.text = firerateUpgradeCost+"";
            checkInteractable();
            CurrentFirerateText.text = fireRate+"";
        });

    }

    void InitUi()
    {
        DamageUpgradeCostText.text = damageUpgradeCost+"";
        CurrentDamageText.text = damage+"";
        FirerateUpgradeCostText.text = firerateUpgradeCost+"";
        CurrentFirerateText.text = fireRate+"";
    }

    void checkInteractable()
    {
        DamageUpgradeButton.interactable = score>damageUpgradeCost;            
        FirerateUpgradeButton.interactable = score>firerateUpgradeCost;
    }


    void CheckButtonInteractable(Button b,int val)
    {
        b.interactable = score>val;
    }
    public void Shoot()
    {
        Rigidbody tempBullet = Instantiate(Bullet, Gun.transform.position, Quaternion.identity);
        tempBullet.velocity = Gun.transform.up * bulletVelocity;
        Destroy(tempBullet.gameObject, 10f);

    }
    public IEnumerator SpawnWall()
    {
        while (true)
        {
            Instantiate(wall);
            yield return new WaitForSeconds(wallSpawnDelay);
        }
    }
    public void SpawnPowerUp()
    {
        Vector3 spawnpos = new Vector3(Random.Range(-4.5f, 4.5f), 1, 40);
        Instantiate(Powerups[Random.Range(0, Powerups.Length)], spawnpos, Quaternion.identity).GetComponent<MoveBack>().speed = powerupMoveSpeed;
    }

    public IEnumerator PowerupSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerupSpawnDelay);
            SpawnPowerUp();
        }
    }



    public IEnumerator AutoShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(1 / (buffedFirerate));
        }
    }

    public void AddScore(int sc)
    {
        score += sc;

        scoreText.text = score + "";
    }


    
    public IEnumerator DamageBuff()
    {
        buffedDamage = damage + damageBuffValue;
        yield return new WaitForSeconds(damageBuffDuration);

        buffedDamage = damage;
    }

    public IEnumerator FirerateBuff()
    {
        buffedFirerate = fireRate*fireRateBuff;
        yield return new WaitForSeconds(fireRateBuffDuration);
        buffedFirerate= fireRate;
    }

    public void DamageInc()
    {
        damage++;
    }

    public void FirerateInc()
    {
        fireRate++;
    }



    public void LateUpdate()
    {

#if UNITY_EDITOR
        player.transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * Time.deltaTime * playerSpeed);
#endif

#if UNITY_ANDROID

        if (Input.GetMouseButton(0))
        {
            player.transform.Translate((Input.mousePosition.x > Screen.width / 2 ? 1 : -1) * Vector3.right * Time.deltaTime * playerSpeed);
        }

#endif
    }



}
