using System.Collections;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Animator animator = null;
    [SerializeField]
    CapsuleCollider capsuleCollider = null;
    [SerializeField, Min(0)]
    int maxHp = 3;
    [SerializeField]
    float deadWaitTime = 3;
    // アニメーターのパラメーターのIDを取得（高速化のため）
    readonly int SpeedHash = Animator.StringToHash("Speed");
    readonly int AttackHash = Animator.StringToHash("Attack");
    readonly int DeadHash = Animator.StringToHash("Dead");
    bool isDead = false;
    int hp = 0;
    Transform thisTransform;

    public int Hp
    {
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);
        }
        get
        {
            return hp;
        }
    }
    void Start()
    {
        thisTransform = transform;  // transformをキャッシュ（高速化）
        InitEnemy();
    }
    void Update()
    {
        if (isDead)
        {
            return;
        }
        UpdateAnimator();
    }
    void InitEnemy()
    {
        Hp = maxHp;
    }
    // 被ダメージ処理
    public void Damage(int value)
    {
        if (value <= 0)
        {
            return;
        }
        Hp -= value;
        if (Hp <= 0)
        {
            Dead();
        }
    }
    // 死亡時の処理
    void Dead()
    {
        isDead = true;
        capsuleCollider.enabled = false;
        animator.SetBool(DeadHash, true);
        StartCoroutine(nameof(DeadTimer));
    }
    // 死亡してから数秒間待つ処理
    IEnumerator DeadTimer()
    {
        yield return new WaitForSeconds(deadWaitTime);
        Destroy(gameObject);
    }
    // アニメーターのアップデート処理
    void UpdateAnimator()
    {
    }
}
