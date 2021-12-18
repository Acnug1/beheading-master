using UnityEngine;

[ExecuteAlways]
public class BezierTest : MonoBehaviour
{
    [SerializeField] private Transform _point0;
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;
    [SerializeField] private Transform _point3;
    [SerializeField] [Range(0, 1)] private float _delta;

    private void Update()
    {
        transform.position = Bezier.GetPoint(_point0.position, _point1.position, _point2.position, _point3.position, _delta);
        transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(_point0.position, _point1.position, _point2.position, _point3.position, _delta));
    }

    private void OnDrawGizmos()
    {
        int sigmentsNumber = 20;
        Vector3 previousPoint = _point0.position;

        for (int i = 1; i < sigmentsNumber + 1; i++)
        {
            float parameter = (float)i / sigmentsNumber;
            Vector3 point = Bezier.GetPoint(_point0.position, _point1.position, _point2.position, _point3.position, parameter);
            Gizmos.DrawLine(previousPoint, point);
            previousPoint = point;
        }
    }
}
