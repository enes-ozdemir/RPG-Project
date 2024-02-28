using EmeraldAI;
using MalbersAnimations;
using UnityEngine;

namespace _Scripts.Controllers
{
    public class EnemyController : CharController
    {
        MDamageable mDamageable;
        Stats stats;
        EmeraldAISystem mEmeraldAISystem;
        
        private void Start()
        {
            mDamageable = GetComponent<MDamageable>();
            mEmeraldAISystem = GetComponent<EmeraldAISystem>();
            mDamageable.events.OnReceivingDamage.AddListener(OnReceiveDamage);
            mEmeraldAISystem.DeathEvent.AddListener(OnDeath);
            
            // mDamageable.OnReceiveDamage += OnReceiveDamage;
            // mDamageable.stats.PinnedStat.
        }

        private void OnDeath()
        {
            Debug.Log("OnDeath");
        }

        private void OnReceiveDamage(float arg0)
        {
            Debug.Log($"{mEmeraldAISystem.CurrentHealth} OnReceiveDamage: {arg0}");
            mEmeraldAISystem.Damage(10);
        }
    }
}