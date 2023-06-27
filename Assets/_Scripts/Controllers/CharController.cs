using _Scripts.UI;
using UnityEngine;

namespace _Scripts.Controllers
{
    public class CharController : MonoBehaviour
    {
        private Animator _animatorController;
        [SerializeField] protected CharInfo charInfo;
        
        private void Awake()
        {
            
            _animatorController = GetComponent<Animator>();
        }
        
        public void PlayAnim(string animName)
        {
            _animatorController.Play(animName);
        }
    }
}