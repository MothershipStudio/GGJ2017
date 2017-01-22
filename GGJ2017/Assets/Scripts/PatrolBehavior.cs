using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PatrolBehavior 
{
    public static void ChangeDirection(Collision2D collision, PatrollingObject po)
    {
        var v1 = po.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
        var v2 = new Vector2(collision.transform.position.x, collision.transform.position.y) - new Vector2(po.transform.position.x, po.transform.position.y);
        v2.Normalize();

        var r = Vector2.Dot(v1, v2);
        if (r >= 0.9990f || r <= 1.0001f)
        {
            var t = po.current;
            po.current = po.next;
            po.next = t;

            var l = po.gameObject.transform.localScale;
            l.y = l.y * -1;
            po.gameObject.transform.localScale = l;
        }
    }

    public static void MantainPlayer(Collision2D collision, PatrollingObject po)
    {
        if (collision.gameObject.tag.Equals("Players"))
        {
            //TODO: manter player sobre a plataforma
        }
    }

    public static void ApplyDamage(Collision2D collision, PatrollingObject po)
    {
        var t = po.GetComponent<EnemyProperties>();

        if(t != null)
        {
            t.currentLife -= collision.gameObject.GetComponent<ObjectProperties>().Damage;
        }
        if(t.currentLife < 0)
        {
            //TODO: fazer um evento de morte
        }
    }


}
