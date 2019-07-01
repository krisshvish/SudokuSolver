using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;


public class Bruteforce : MonoBehaviour {
	const int N = 9;
	public struct aditya{
		public int number;
		public bool fix;
	}
	public aditya[,] grid = new aditya[N,N];

public void Input()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag ("GameController");
		int x = 0;

		for (int i = 0; i < N; i++) {
			for (int j = 0; j < N; j++) {
				gameObject.transform.GetChild (x).gameObject.GetComponent<InputField> ().readOnly = false;
				string str = gameObject.transform.GetChild (x).gameObject.GetComponent<InputField> ().text;

				if (str != string.Empty) {

					gameObject.transform.GetChild (x).gameObject.GetComponent<InputField> ().textComponent.color = Color.red;
					gameObject.transform.GetChild (x).gameObject.GetComponent<InputField> ().textComponent.fontStyle = FontStyle.Bold;
					gameObject.transform.GetChild (x).gameObject.GetComponent<InputField> ().textComponent.fontSize = 16;
					gameObject.transform.GetChild (x).gameObject.GetComponent<Image> ().color = Color.yellow;
					grid [i, j].fix = true;
					grid [i, j].number = int.Parse (str);
				} else {

					grid [i, j].fix = false;
					grid [i, j].number = 0;
				}

				x++;
			}
		}
		Stopwatch timer = new Stopwatch ();
		timer.Start ();
		if (solve (0, 0)) { 
			timer.Stop ();
			PrintGrid ();
			GameObject.FindGameObjectWithTag ("Timecomp").gameObject.GetComponent<Text> ().text = GameObject.FindGameObjectWithTag ("Timecomp")
				.gameObject.GetComponent<Text> ().text + "\n" + "Bruteforce:" + "\n" + timer.Elapsed.ToString ();
		} else {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			print ("!No Solution Exists");
		}
}

	private void PrintGrid()
	{
		GameObject gameobject=GameObject.FindGameObjectWithTag("GameController");
		InputField[] abc=gameobject.GetComponentsInChildren<InputField> ();
		int counti=0;
		int countj=-1;
		foreach(InputField jnt in abc)
		{   countj++;
			jnt.text=grid[counti,countj].number.ToString();
			if (countj == 8) {
				countj = -1;
				counti++;
			}
		}
	}

	bool check_row(int row,int column)
	{bool stat=true;
		for(int i=0;i<9;i++){
			if(i!=column){
				if (grid [row, i].number == grid [row, column].number) {
					stat = false;
					break;
				}
			}
		}
		return stat;
	}


	bool check_column(int row,int column)
	{bool stat=true;
		for (int i = 0; i < 9; i++) {
			if (i != row) {
				if (grid [i, column].number == grid [row, column].number) {
					stat = false;
					break;
				}
			}
		}
		return stat;		
	}

	bool check_square(int row,int column){
		bool stat = true;
		int vsquare = row / 3;
		int hsquare = column / 3;
		for(int i=vsquare*3;i<(vsquare*3+3);i++)	
			for(int j=hsquare*3;j<(hsquare*3+3);j++)
				if(!(i==row && j==column))
				{
					if (grid [row, column].number == grid [i, j].number) {
						stat = false;
						break;}
				}
		return stat;
	}
 bool solve (int row, int column)
	{
		while (grid [row, column].fix == true) {
			column++;
			if (column > 8) {
				column = 0;
				row++;
			}
			if (row > 8)
				return true;
		}
		for (int p = 1; p < 10; p++) {
			int nextrow, nextcolumn;
			grid [row, column].number = p;
			if (check_column (row, column))
			if (check_row (row, column))
			if (check_square (row, column)) { 
				nextrow = row;
				nextcolumn = column;
				nextcolumn++;
				if (nextcolumn > 8) {
					nextcolumn = 0;
					nextrow++;
				}
				if (nextcolumn == 0 && nextrow == 9) {
					return true;
				}
				if (solve (nextrow, nextcolumn))
					return true;
				                     
			}
		}
		grid [row, column].number = 0;
		return false;
	}
}