using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    /// <summary>
    /// изначально я рассчитывал FirePoint с помощью тригонометрии:
    /// у меня есть угол поворота самолета, и огневая точка находится на расстоянии 100, и
    /// через арктангенс можно получить новую позицию огневой точки.
    /// Но потом я просто решил поместить на сцене огневую точку и убрать тригонометрию.
    /// Код закомментировал просто для истории
    /// </summary>
    /// <param name="target"></param>
    
    
    //[SerializeField] private RectTransform _firePoint;
    //[SerializeField] private Vector3 _point;
    // private float _xShakeValue;
    // private float _yShakeValue;
    // private Vector3 _startFirePointPosition;

    // private void Awake()
    // {
    //     _startFirePointPosition = _firePoint.position;
    // }

    //public void HideFirePoint() => _firePoint.gameObject.SetActive(false);

    public void UpdateFirePointPosition(Vector3 target) //Quaternion value)
    {
        _bullet.UpdateFirePointCoordinate(target);

        // var xShift = -100 * Mathf.Tan(value.z);
        // var yShift = -100 * Mathf.Tan(value.x);
        //
        // var firePoint = new Vector3(_point.x + xShift, _point.y + yShift, _point.z);
        //
        // _bullet.UpdateFirePointCoordinate(firePoint);
        // _firePoint.position = new Vector3(_startFirePointPosition.x + xShift, _startFirePointPosition.y + yShift * 7,
        //     _startFirePointPosition.z);
    }
}