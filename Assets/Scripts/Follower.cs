using UnityEngine;
using PathCreation;
using UnityEngine.EventSystems;

public class Follower : MonoBehaviour, IDragHandler
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private Follower _otherFollower;

    private void Start()
    {
        SetFollowerPosition(transform.position);
        LookAtOtherFollower();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null)
        {
            Vector3 dragPosition = eventData.pointerCurrentRaycast.worldPosition;
            SetFollowerPosition(dragPosition);
        }
    }

    private void SetFollowerPosition(Vector3 position)
    {
        transform.position = _pathCreator.path.GetClosestPointOnPath(position);
    }

    private void Update()
    {
        LookAtOtherFollower();
    }

    private void LookAtOtherFollower()
    {
        transform.LookAt(_otherFollower.transform);
    }
}
