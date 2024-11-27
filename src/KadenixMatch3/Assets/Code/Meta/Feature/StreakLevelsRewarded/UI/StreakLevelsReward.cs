using Code.Meta.Feature.StreakLevelsRewarded.Services;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.Feature.StreakLevelsRewarded.UI
{
    public class StreakLevelsReward : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _firstStar;
        [SerializeField] private Image _secondStar;
        [SerializeField] private Image _chestTop;
        [SerializeField] private Image _chestBottom;

        private Color _originalFirstStarColor;
        private Color _originalSecondStarColor;

        private IStreakLevelsRewardUIService _streakLevelsRewardUIService;

        private int _currentStreak;
        private int _maxStreak;
        private int _currentLevelReward;
        private int _maxLevelReward;

        [Inject]
        public void Construct(IStreakLevelsRewardUIService streakLevelsRewardUIService)
        {
            _streakLevelsRewardUIService = streakLevelsRewardUIService;
        }

        public void Start()
        {
            _currentStreak = _streakLevelsRewardUIService.GetCurrentStreak();
            _maxStreak = _streakLevelsRewardUIService.GetMaxStreak();
            _currentLevelReward = _streakLevelsRewardUIService.GetCurrentLevelReward();
            _maxLevelReward = _streakLevelsRewardUIService.GetMaxLevelReward();

            _originalFirstStarColor = _firstStar.color;
            _originalSecondStarColor = _secondStar.color;

            if (_currentLevelReward > 1)
            {
                _chestBottom.gameObject.SetActive(true);
            }

            AnimateSlider();
        }

        
        private void AnimateSlider()
        {
            if (_currentStreak > 0)
            {
                int startValue = _currentStreak - 1;
                _slider.value = startValue;

                _slider.DOValue(_currentStreak, 1f).SetEase(Ease.Linear)
                    .OnUpdate(() =>
                    {
                        UpdateStarColors();
                    })
                    .OnComplete(() =>
                    {
                        FillProgress();
                    });
            }
            else
            {
                _slider.value = 0;
                FillProgress();
            }
        }

        private void UpdateStarColors()
        {
            if (_slider.value >= 2)
            {
                _firstStar.DOColor(Color.white, 0.2f);
                _secondStar.DOColor(Color.white, 0.2f);
            }
            else if (_slider.value >= 1)
            {
                _firstStar.DOColor(Color.white, 0.2f);

                _secondStar.DOColor(_originalSecondStarColor, 0.2f);
            }
            else
            {
                _firstStar.DOColor(_originalFirstStarColor, 0.2f);
                _secondStar.DOColor(_originalSecondStarColor, 0.2f);
            }
        }

        private void FillProgress()
        {
            if (_currentStreak >= 1)
            {
                _firstStar.color = Color.white;
            }

            if (_currentStreak >= 2)
            {
                _secondStar.color = Color.white;
            }

            if (_currentStreak >= 3 && _currentLevelReward == _maxLevelReward)
            {
                _chestTop.color = Color.white;
            }
            else if (_currentStreak >= 3 && _currentLevelReward < _maxLevelReward)
            {
                ChestAnimation();
            }

            if (_currentLevelReward >= 2)
            {
                _chestBottom.gameObject.SetActive(true);
            }
        }

        private void ChestAnimation()
        {
            _chestTop.gameObject.SetActive(false);
            _chestBottom.gameObject.SetActive(true);

            var startPosition = _chestBottom.transform.position;
            var startScale = _chestBottom.transform.localScale;
            var targetColor = _chestTop.color;
            var endColor = Color.white;

            _chestBottom.transform.position = _chestTop.transform.position;
            _chestBottom.color = targetColor;

            Sequence scaleAndColorSequence = DOTween.Sequence();

            scaleAndColorSequence
                .Join(_chestBottom.DOColor(endColor, 0.6f))
                .Join(_chestBottom.transform.DOScale(startScale * 0.75f, 0.2f).SetEase(Ease.Linear))
                .Append(_chestBottom.transform.DOScale(startScale, 0.2f).SetEase(Ease.Linear))
                .OnComplete(() => AnimateMovement(startPosition));
        }

        private void AnimateMovement(Vector3 targetPosition)
        {
            float duration = 0.9f;
            Vector3 startPosition = _chestBottom.transform.position;
            float startSliderValue = _slider.value;

            bool secondStarFaded = false;
            bool firstStarFaded = false;

            _chestBottom.transform.DOMove(targetPosition, duration)
                .SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    float progress = Vector3.Distance(_chestBottom.transform.position, targetPosition) /
                                     Vector3.Distance(startPosition, targetPosition);

                    _slider.value = Mathf.Lerp(0, startSliderValue, progress);

                    if (!secondStarFaded && _slider.value < 2)
                    {
                        secondStarFaded = true;
                        FadeOutStar(_secondStar);
                    }

                    if (!firstStarFaded && _slider.value < 1)
                    {
                        firstStarFaded = true;
                        FadeOutStar(_firstStar);
                    }
                })
                .OnComplete(() =>
                {
                    if (_currentLevelReward < _maxLevelReward)
                    {
                        _slider.value = 0;
                        _streakLevelsRewardUIService.LevelRewardCompleted();
                    }

                    AnimateStarsAndChestToOriginalPositions();
                });
        }

        private void AnimateStarsAndChestToOriginalPositions()
        {
            Vector3 originalFirstStarPosition = _firstStar.transform.position;
            Vector3 originalSecondStarPosition = _secondStar.transform.position;

            Vector3 originalChestTopPosition = _chestTop.transform.position;
            Color originalChestTopColor = _chestTop.color;
            Color originalChestBottomColor = _chestBottom.color;

            _firstStar.transform.position = _chestBottom.transform.position;
            _secondStar.transform.position = _chestBottom.transform.position;
            _chestTop.transform.position = _chestBottom.transform.position;

            _firstStar.gameObject.SetActive(true);
            _secondStar.gameObject.SetActive(true);
            _chestTop.gameObject.SetActive(true);
            _chestBottom.gameObject.SetActive(true);

            _firstStar.color = _originalFirstStarColor;
            Sequence firstStarSequence = DOTween.Sequence();
            firstStarSequence
                .Join(_firstStar.transform.DOMove(originalFirstStarPosition, 0.6f).SetEase(Ease.OutQuad))
                .Join(_firstStar.transform.DOScale(Vector3.one, 0.6f).SetEase(Ease.OutQuad))
                .Join(_firstStar.DOFade(1, 0.6f));

            _secondStar.color = _originalSecondStarColor;
            Sequence secondStarSequence = DOTween.Sequence();
            secondStarSequence
                .Join(_secondStar.transform.DOMove(originalSecondStarPosition, 0.6f).SetEase(Ease.OutQuad))
                .Join(_secondStar.transform.DOScale(Vector3.one, 0.6f).SetEase(Ease.OutQuad))
                .Join(_secondStar.DOFade(1, 0.6f));

            Sequence chestSequence = DOTween.Sequence();
            chestSequence
                .Join(_chestTop.transform.DOMove(originalChestTopPosition, 0.6f).SetEase(Ease.OutQuad))
                .Join(_chestTop.DOColor(originalChestTopColor, 0.6f))
                .Join(_chestBottom.DOColor(originalChestBottomColor, 0.6f));

            secondStarSequence.Insert(0.2f, firstStarSequence);
            chestSequence.Insert(0.4f, secondStarSequence);
        }

        private void FadeOutStar(Image starImage)
        {
            starImage.DOFade(0f, 0.3f);
        }
    }
}
