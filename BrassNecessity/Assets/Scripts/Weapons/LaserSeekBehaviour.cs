using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSeekBehaviour : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer laserRender;

    [SerializeField]
    private VolumetricLines.VolumetricLineBehavior laserBeam;

    [SerializeField]
    private GameObject laserSplash;

    [SerializeField]
    private LayerMask ignoreLayers;

    [SerializeField]
    private float baseDamagePerSecond = 15f;

    [SerializeField]
    private ElementComponent weaponElement;

    [SerializeField]
    private ImpactBehaviour impactEffect;

    private EnemyHealthHandler hitEnemy;

    private void Start()
    {
        ignoreLayers = ~ignoreLayers;
        if (weaponElement == null)
        {
            weaponElement = GetComponentInParent<ElementComponent>();
        }
    }

    public void SeekTarget()
    {
        gameObject.SetActive(true);
        RaycastHit target;
        Vector3 raycastStart = transform.position;
        Vector3 raycastDirection = transform.TransformDirection(Vector3.up);
        if (Physics.Raycast(raycastStart, raycastDirection, out target, Mathf.Infinity, ignoreLayers))
        {
            laserRender.enabled = true;
            laserSplash.SetActive(true);
            float targetDistance = target.distance / transform.lossyScale.y;
            laserSplash.transform.localPosition = new Vector3(0, targetDistance, 0);
            Vector3 newEndPos = new Vector3(0, targetDistance, 0);
            laserBeam.EndPos = newEndPos;
            handleEnemyCollision(target);
        }
        laserBeam.UpdateLineScale();
    }

    private void handleEnemyCollision(RaycastHit target)
    {
        if (target.collider.TryGetComponent(out hitEnemy))
        {
            ElementPair enemyElement = target.collider.GetComponent<ElementComponent>().ElementInfo;
            ElementPair laserElement = weaponElement.ElementInfo;
            float multiplier = ElementMultiplierGrid.GetAttackMultiplier(laserElement.Primary, enemyElement.Primary);
            impactEffect.SetImpactEffects(multiplier);
            float damage = baseDamagePerSecond * multiplier * Time.deltaTime;
            hitEnemy.DamageEnemy(damage);
        }
        else
        {
            impactEffect.ResetEffects();
        }
    }

    public void FinishSeeking()
    {
        Vector3 oldStart = laserBeam.StartPos;
        Vector3 targetStart = new Vector3(0, laserBeam.EndPos.y, 0);
        laserBeam.StartPos = targetStart;
        laserSplash?.SetActive(false);
        laserRender.enabled = false;
        gameObject.SetActive(false);
        laserBeam.StartPos = oldStart;
        hitEnemy.StopDamagingEnemy();
        hitEnemy = null;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 raycastStart = transform.position;
        Vector3 raycastDirection = transform.TransformDirection(Vector3.up);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastStart, raycastDirection);
        if (Physics.Raycast(raycastStart, raycastDirection, out RaycastHit target, Mathf.Infinity, ignoreLayers))
        {
            float targetDistance = target.distance / transform.lossyScale.y;
            Vector3 newEndPos = transform.TransformVector(new Vector3(0, targetDistance, 0)); ;
            Gizmos.DrawSphere(newEndPos, .1f);
        }
    }
}
