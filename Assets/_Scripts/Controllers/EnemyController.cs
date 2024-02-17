using MalbersAnimations;

namespace _Scripts.Controllers
{
    public class EnemyController : CharController
    {
        MDamageable mDamageable;
        
        
        private void Start()
        {
            mDamageable = GetComponent<MDamageable>();
            // mDamageable.OnReceiveDamage += OnReceiveDamage;
            // mDamageable.stats.PinnedStat.
        }
    }
}