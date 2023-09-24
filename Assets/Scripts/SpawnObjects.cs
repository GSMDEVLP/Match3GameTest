using UnityEngine;
using Zenject;

public class SpawnObjects : MonoBehaviour
{
    private ResourceManager _resourceManager;

    [Inject]
    private void Construct(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
    }

    void Start()
    {
        Debug.Log("Начало Спавн Обджектов!");
        SpawnGameObjects();
    }

    private void SpawnGameObjects()
    {
        Sprite[] gameSprite = _resourceManager.LoadGameObjects("GameSprites");
        Transform gameObj = gameObject.transform;
        SpriteRenderer spriteRenderer = new SpriteRenderer();

        foreach (Transform item in gameObj)
        {
            item.position = new Vector3(item.position.x, item.position.y, -2f);
            spriteRenderer = item.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = gameSprite[Random.Range(0, gameSprite.Length)];
        }
    }

    private void Update()
    {
        Sprite[] gameSprite = _resourceManager.LoadGameObjects("GameSprites");
        Transform gameObj = gameObject.transform;
        SpriteRenderer spriteRenderer = new SpriteRenderer();
        foreach (Transform item in gameObj)
        {
            spriteRenderer = item.GetComponent<SpriteRenderer>();
            if(spriteRenderer.sprite == null)
                spriteRenderer.sprite = gameSprite[Random.Range(0, gameSprite.Length)];
        }
    }
}
