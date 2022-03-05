using UnityEngine;

public class SManager : MonoBehaviour
{
    [SerializeField] 
    Camera fpsCamera;

    [SerializeField]
    KeyCode ShootKey;

    [SerializeField]
    int bulletDamage = 1; 

    RaycastHit hit;

    void Update()
    {
        if(Input.GetKeyDown(ShootKey))
        {
            if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, 100))
            {
                var damage = hit.transform.GetComponent<ShootManager>();
                if(damage != null)
                {
                    damage.Damage(bulletDamage);
                }
                Debug.Log(hit.transform.name);
            }
        }
    }
}
