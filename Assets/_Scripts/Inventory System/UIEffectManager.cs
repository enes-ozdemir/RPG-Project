using DG.Tweening;
using UnityEngine;

namespace _Scripts.Inventory_System
{
    public class UIEffectManager : MonoBehaviour

    {
        [SerializeField] private RectTransform targetRectTransform;

        [SerializeField] private float endScale = 6f;
        [SerializeField] private float startPositionX = 5f;
        [SerializeField] private float endPositionX = 0f;

        private Sequence _sequence;

        public void FlyIn()
        {
            targetRectTransform.DOLocalMoveX(endPositionX, 0.5f, false);
        }

        public void FlyOut()
        {
            targetRectTransform.DOLocalMoveX(startPositionX, 0.5f, false);
        }

        private void Scale()
        {
            targetRectTransform.DOScale(endScale, 0.5f);
        }

        public void SequenceFlyIn()
        {
            _sequence = DOTween.Sequence().SetAutoKill(false);

            _sequence.Append(targetRectTransform.DOLocalMoveX(endPositionX, 2F, false).SetEase(Ease.InElastic));
            _sequence.Append(targetRectTransform.DOScale(endScale, 0.5f));
        }

        public void Rewind() => _sequence.Rewind();

    }
}