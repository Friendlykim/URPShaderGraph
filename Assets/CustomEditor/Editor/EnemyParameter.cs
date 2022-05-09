
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObj/EnemyParameter")]
public class EnemyParameter : ScriptableObject
{
    [SerializeField]
    private float hp;
    public float Hp { get { return hp; }}

    [SerializeField]
    private float movespeed;
    public float MoveSpeed { get { return movespeed; } }

    [SerializeField]
    private float attackrange;
    public float AttackRange { get { return attackrange; } }
    [SerializeField]
    private float damage;
    public float Damage { get { return damage; } }
}
