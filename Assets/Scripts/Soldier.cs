using UnityEngine;


public class Soldier : Character
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        Debug.DrawRay(transform.position + new Vector3(0f, 1.5f, 0f), vectorToMove * attackRange, Color.red, 0.2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, hitEnemy.point);
    }
}