using UnityEngine;
using PathCreation;
using UnityEngine.EventSystems;

public class Follower : MonoBehaviour, IDragHandler
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private Follower _otherFollower;

    private float _offsetY = 0.1f;

    private void Start()
    {
        SetFollowerPosition(transform.position);
        LookAtOtherFollower(_otherFollower);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null)
        {
            Vector3 dragPosition = eventData.pointerCurrentRaycast.worldPosition;
            SetFollowerPosition(dragPosition);
        }
    }

    private void SetFollowerPosition(Vector3 position) => 
        transform.position = _pathCreator.path.GetClosestPointOnPath(position) + new Vector3(0, _offsetY, 0);

    private void Update()
    {
        LookAtOtherFollower(_otherFollower);
    }

    private void LookAtOtherFollower(Follower otherFollower)
    {
        transform.LookAt(otherFollower.transform);
    }
}
