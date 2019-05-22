using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    [SerializeField] private float hitTextureDisappearDelay = 0.3f;
    [SerializeField] private GameObject hitTexturePrefab;

    private void Awake()
    {
        Messenger<RaycastHit>.AddListener(GameEvents.HIT, OnHit);
    }

    private void OnDestroy()
    {
        Messenger<RaycastHit>.RemoveListener(GameEvents.HIT, OnHit);
    }

    private void OnHit(RaycastHit hit)
    {
        if(hit.transform.tag == "Wall")
        {
            //TODO
            Vector3 finalPosition = hit.point + hit.normal * 0.01f;
            Object hitMark = Instantiate(hitTexturePrefab, finalPosition, Quaternion.LookRotation(hit.normal));
            Destroy(hitMark, hitTextureDisappearDelay);
        }
        else if (hit.transform.tag == "Target")
        {
            DestroyableWithEffects target = hit.transform.gameObject.GetComponent<DestroyableWithEffects>();
            if (target != null) {
                target.PlayEffectThenDestroy();
            }
        }
    }
}
