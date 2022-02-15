using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    #region Private Variables

    Vector3 finalLookDir;

    #endregion

    #region Public or Serialize Variable

    [Header("Camera")]
    [SerializeField] private Camera mainCamera;

    [Header("Turret Settings")]
    [SerializeField] private float RotateSpeed = 0.7f;

    #endregion

    #region In-Built Functions

    void Update()
    {    
        LookMech();
    }

    #endregion

    #region Custom Functions

    void LookMech()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 lookdir = transform.position - new Vector3(hit.point.x, transform.position.y, hit.point.z);
            lookdir.y = 0;
            finalLookDir = Vector3.Lerp(finalLookDir, lookdir, RotateSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(-finalLookDir);
        }
    }
    
    #endregion
}
