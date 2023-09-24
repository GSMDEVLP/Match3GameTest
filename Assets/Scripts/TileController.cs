using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileController : MonoBehaviour
{
	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
	private static TileController previousSelected = null;

	private SpriteRenderer render;
	private bool isSelected = false;

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
	private bool matchFound = false;
	private bool waitingForMatchCheck = false;
	
	private ScoreManager _scoreManager;
	private int _point = 0;
	[Inject]
	public void Construct(ScoreManager scoreService)
	{
		_scoreManager = scoreService;
	}


	void Awake()
	{
		render = GetComponent<SpriteRenderer>();
	}


	private void Update()
	{
		if (!waitingForMatchCheck)
			StartCoroutine(DelayedMatchCheck());
	}

	private IEnumerator DelayedMatchCheck()
	{
		waitingForMatchCheck = true;
		yield return new WaitForSeconds(2f); 
		ClearAllMatches();
		waitingForMatchCheck = false;
	}
	private void Select()
	{
		isSelected = true;
		render.color = selectedColor;
		previousSelected = gameObject.GetComponent<TileController>();
	}

	private void Deselect()
	{
		isSelected = false;
		render.color = Color.white;
		previousSelected = null;
	}
	void OnMouseDown()
	{
		if (isSelected)
		{
			Deselect();
		}
		else
		{
			if (previousSelected == null)
			{
				Select();
			}
            else
            {
                if (GetAllAdjacentTiles().Contains(previousSelected.gameObject))
                { // 1
                    SwapSprite(previousSelected.render); // 2
					previousSelected.ClearAllMatches();
					previousSelected.Deselect();
					ClearAllMatches();
				}
                else
                { // 3
                    previousSelected.GetComponent<TileController>().Deselect();
                    Select();
                }
            }
        }
		
	}

	public void SwapSprite(SpriteRenderer render2)
	{ // 1
		if (render.sprite == render2.sprite)
		{ // 2
			return;
		}

		Sprite tempSprite = render2.sprite; // 3
		render2.sprite = render.sprite; // 4
		render.sprite = tempSprite; // 5
	}

	private GameObject GetAdjacent(Vector2 castDir)
	{
		RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, castDir);
		if (hit.collider != null)
		{
			return hit.collider.gameObject;
		}
		return null;
	}
	private List<GameObject> GetAllAdjacentTiles()
	{
		List<GameObject> adjacentTiles = new List<GameObject>();
		for (int i = 0; i < adjacentDirections.Length; i++)
		{
			adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
		}
		return adjacentTiles;
	}

	private List<GameObject> FindMatch(Vector2 castDir)
	{ // 1
		List<GameObject> matchingTiles = new List<GameObject>(); // 2
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir); // 3
		while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
		{ // 4
			matchingTiles.Add(hit.collider.gameObject);
			hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
		}
		return matchingTiles; // 5
	}

	private void ClearMatch(Vector2[] paths) // 1
	{
		List<GameObject> matchingTiles = new List<GameObject>(); // 2
		
		for (int i = 0; i < paths.Length; i++) // 3
		{
			matchingTiles.AddRange(FindMatch(paths[i]));
		}
		if (matchingTiles.Count >= 2) // 4
		{
			for (int i = 0; i < matchingTiles.Count; i++) // 5
			{
				matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
			}
			matchFound = true; // 6
			_scoreManager.UpdateScore(SetScore(matchingTiles.Count + 1));
		}
		
	}

    private int SetScore(int score)
    {
		
		switch(score)
        {
			case 3:
				_point = score;
				break;
			case 4:
				_point = score * 2;
				break;
			case 5:
				_point = score * 3;
				break;
		}
		return _point;
	}

    public void ClearAllMatches()
	{		
		if (render.sprite == null)
			return;

		ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
		ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
		if (matchFound)
		{
			Debug.Log("Произошло удаление без клика");
			render.sprite = null;
			matchFound = false;
		}
	}

}
