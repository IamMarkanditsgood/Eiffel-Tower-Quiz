using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ConfettiEffectUI : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Sprite _confettiSprite;
    [SerializeField] private int _particleCount = 25;

    [Header("Effect Shape")]
    [SerializeField] private RectTransform[] _spawnPositions;
    [SerializeField] private float _spreadAngle = 60f; 
    [SerializeField] private float _minDistance = 100f;
    [SerializeField] private float _maxDistance = 200f;

    [Header("Confetti Size")]
    [SerializeField] private Vector2 _minSize = new Vector2(8f, 8f);
    [SerializeField] private Vector2 _maxSize = new Vector2(16f, 16f);
    void Awake()
    {
        DOTween.SetTweensCapacity(600, 250);
    }

    public void PlayConfetti(float time)
    {
        for (int i = 0; i < _particleCount; i++)
        {
            CreateConfettiParticle(time);
        }
    }

    private void CreateConfettiParticle(float time)
    {
        for (int i = 0; i < _spawnPositions.Length; i++)
        {
            var rect = CreateConfettiObject(out Image img, _spawnPositions[i]);
            Vector2 target = CalculateTargetDirection(rect.anchoredPosition, out float distance);
            AnimateConfetti(rect, img, target, time);
        }
    }
    //Object creation
    private RectTransform CreateConfettiObject(out Image particle, RectTransform spawnPosition)
    {
        particle = PoolObjectManager.instant.ConfettiPool.GetFreeComponent();

        particle.sprite = _confettiSprite;
        particle.color = Random.ColorHSV();

        RectTransform rect = particle.gameObject.GetComponent<RectTransform>();
        rect.anchoredPosition = spawnPosition.anchoredPosition;
        float sizeX = Random.Range(_minSize.x, _maxSize.x);
        float sizeY = Random.Range(_minSize.y, _maxSize.y);
        rect.sizeDelta = new Vector2(sizeX, sizeY);

        return rect;
    }
    //flying direction
    private Vector2 CalculateTargetDirection(Vector2 startPosition, out float distance)
    {
        float halfAngle = _spreadAngle / 2f;
        float angle = Random.Range(-halfAngle, halfAngle);
        float rad = angle * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad)).normalized;
        distance = Random.Range(_minDistance, _maxDistance);
        return startPosition + direction * distance;
    }

    //Animation
    private void AnimateConfetti(RectTransform rect, Image img, Vector2 target, float duration)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(rect.DOAnchorPos(target, duration).SetEase(Ease.OutCubic));
        seq.Join(rect.DORotate(new Vector3(0, 0, Random.Range(90f, 360f)), duration, RotateMode.FastBeyond360));
        seq.Join(img.DOFade(0f, duration));
        seq.OnComplete(() => PoolObjectManager.instant.ConfettiPool.DisableComponent(img));
        seq.SetAutoKill(true);
    }
}