using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class NewBehaviourScript : MonoBehaviour {
	/*public InputField[] abc;
	//public int[9][9] ArrayList
	public void calling() {
		print ("coming inide ");
		GameObject p = GameObject.FindGameObjectWithTag ("GameController");
		abc=p.GetComponentsInChildren<InputField> ();
		int i = 1;
		foreach(InputField jnt in abc)
		//{if(!string.IsNullOrEmpty(jnt.text))
		{jnt.text=i++.ToString();}
}*/const int N = 9;
	private int[,] grid = new int[N,N];
	public Color FilledColor;
	public Color TextColor;

	public void quittext()
	{
		GameObject.FindGameObjectWithTag ("Timecomp").gameObject.GetComponent<Text> ().text = null;
	}
		
	private void Awake()
	{ 
		for (int i = 0; i < N; i++)
			for (int j = 0; j < N; j++)
				grid[i,j] = 0;
	}

	public void ClearSudoku()
	{GameObject gameObject=GameObject.FindGameObjectWithTag("GameController");
		for (int i = 0; i < 81; i++) {
			gameObject.transform.GetChild (i).gameObject.GetComponent<InputField> ().readOnly = false;
			gameObject.transform.GetChild (i).gameObject.GetComponent<InputField> ().text = null;
			gameObject.transform.GetChild (i).gameObject.GetComponent<Image> ().color= new Color32(15,240,0,255);
			gameObject.transform.GetChild (i).gameObject.GetComponent<InputField>().textComponent.color= Color.black;
			gameObject.transform.GetChild (i).gameObject.GetComponent<InputField> ().textComponent.fontStyle = FontStyle.Normal;
			gameObject.transform.GetChild (i).gameObject.GetComponent<InputField> ().textComponent.fontSize=14;

		}
	}

	public void ClearOnly()
	{GameObject gameObject=GameObject.FindGameObjectWithTag("GameController");
		for (int i = 0; i < 81; i++) {
			if (gameObject.transform.GetChild (i).gameObject.GetComponent<InputField> ().textComponent.color == Color.red)
				gameObject.transform.GetChild (i).gameObject.GetComponent<InputField> ().readOnly = true;
			else
				gameObject.transform.GetChild (i).gameObject.GetComponent<InputField> ().text = null;
		}
	}


		
	public void Solve()
	{GameObject gameObject=GameObject.FindGameObjectWithTag("GameController");
		int x = 0;

		for(int i=0; i<N; i++)
		{
			for(int j=0; j<N; j++)
			{
				string str = gameObject.transform.GetChild(x).gameObject.GetComponent<InputField>().text;
				gameObject.transform.GetChild (x).gameObject.GetComponent<InputField> ().readOnly = false;
				if (str != string.Empty)
				{

					gameObject.transform.GetChild (x).gameObject.GetComponent<InputField>().textComponent.color= Color.red;
					gameObject.transform.GetChild (x).gameObject.GetComponent<InputField> ().textComponent.fontStyle = FontStyle.Bold;
					gameObject.transform.GetChild (x).gameObject.GetComponent<InputField> ().textComponent.fontSize=16;
					gameObject.transform.GetChild (x).gameObject.GetComponent<Image> ().color= Color.yellow;
					grid[i, j] = int.Parse(str);

				}             
				else
				{

					grid[i, j] = 0;
				}
				gameObject.transform.GetChild (x).gameObject.GetComponent<InputField> ().textComponent.alignment =(TextAnchor) TextAlignment.Center;
				x++;

			}
		}
		Stopwatch timer = new Stopwatch ();
		timer.Start();
		if (SolveSudoku (grid)) {
			timer.Stop ();
			PrintGrid (ref grid);
			GameObject.FindGameObjectWithTag ("Timecomp").gameObject.GetComponent<Text> ().text = GameObject.FindGameObjectWithTag ("Timecomp").gameObject.GetComponent<Text> ().text + "\n" + "Backtracking:" + "\n" + timer.Elapsed.ToString ();
		} else {
			UnityEngine.Debug.Log ("No solution!");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	private void PrintGrid(ref int[,] grid)
	{GameObject gameobject=GameObject.FindGameObjectWithTag("GameController");
		InputField[] abc=gameobject.GetComponentsInChildren<InputField> ();
		int counti=0;
		int countj=-1;
		foreach(InputField jnt in abc)
		{   countj++;
			jnt.text=grid[counti,countj].ToString();
			if (countj == 8) {
				countj = -1;
				counti++;
			}
		}
	}

	private bool FindUnassignedLocation(int[,] grid, ref int row, ref int col)
	{
		for (row = 0; row < N; row++)
			for (col = 0; col < N; col++)
				if (grid[row, col] == 0)
					return true;
		return false;
	}

	private bool UsedInRow(int[,] grid, int row, int num)
	{
		for (int col = 0; col < N; col++)
			if (grid[row, col] == num)
				return true;
		return false;
	}

	private bool UsedInCol(int[,] grid, int col, int num)
	{
		for (int row = 0; row < N; row++)
			if (grid[row, col] == num)
				return true;
		return false;
	}

	private bool UsedInBox(int[,] grid, int BoxStartRow, int BoxStartCol, int num)
	{
		for(int row=0; row<3; row++)
			for (int col = 0; col < 3; col++)
				if (grid[row+BoxStartRow, col+BoxStartCol] == num)
					return true;
		return false;
	}

	private bool isSafe(int[,] grid, int row, int col, int num)
	{
		return !UsedInRow(grid, row, num)
			&& !UsedInCol(grid, col, num)
			&& !UsedInBox(grid, row-row%3, col-col%3, num);
	}

	private bool SolveSudoku(int[,] grid)
	{
		int row = new int();
		int col = new int();

		if (!FindUnassignedLocation(grid, ref row, ref col))
			return true;

		for(int num=1; num<=9; num++)
		{
			if (isSafe (grid, row, col, num)) {
				grid [row, col] = num;
				if (SolveSudoku (grid))
					return true;
				grid [row, col] = 0;
			} 
		}
		return false;
	}

}
