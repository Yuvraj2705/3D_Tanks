using UnityEngine;

public class PSelection : MonoBehaviour
{
    [SerializeField] 
    Camera myCamera;

    [SerializeField]
    KeyCode IntereactKey;

    RaycastHit hit;

    void Update()
    {
        if(Input.GetKeyDown(IntereactKey))
        {
            if(Physics.Raycast(myCamera.transform.position, myCamera.transform.forward, out hit, 100))
            {
                var interactor = hit.transform.GetComponent<SelectionManager>();
                if(interactor != null)
                {
                    interactor.ImplementCode();
                }
                //Debug.Log(hit.transform.name);
            }
        }
    }
}
