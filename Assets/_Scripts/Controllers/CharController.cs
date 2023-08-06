using _Scripts.UI;
using UnityEngine;

namespace _Scripts.Controllers
{
    public class CharController : MonoBehaviour
    {
        [SerializeField] protected Animator AnimatorController;
        [SerializeField] protected CharInfo charInfo;
        
        private void Awake()
        {
            AnimatorController = GetComponent<Animator>();
        }
        
        public void PlayAnim(string animName)
        {
            AnimatorController.Play(animName);
        }
    }
}