using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CSVReaderScript : MonoBehaviour {

	public bool test = false;
	public TextAsset csvFile;
	private static List<Category> categories = new List<Category>();
	public struct Category {
		public string grouping;
		public List<Choices> choices;
	};

	public struct Choices{
		public string whyFail;
		public string message;
		public int time;
		public string cumulativeEffect;
		public string nextGroup;
		public string addResponsibility;
		public string addEffect;
		public StatusUpdates statusUpdates;
		public Requirements requirements;

	};
	public struct StatusUpdates{
		public int happy;
		public int gold;
		public int skill;
		public int socialize;
	};

	public struct Requirements{
		public int gold;
		public int skill;
		public int socialize;
		public string responsibility;
		public string effect;
	};
	// Use this for initialization
	void Start () {
		string[,] grid = SplitCsvGrid(csvFile.text);
		Debug.Log("size = " + (1+ grid.GetUpperBound(0)) + "," + (1 + grid.GetUpperBound(1))); 
		ProcessOutputGrid(grid);

		// Category haha = GetRandomGroup();
		// Debug.Log(haha.grouping);
		// Category hihi = GetSpecificGroup("haha");
		// Debug.Log(hihi.grouping);
		Category cat = GetRandomGroup();
	}
	
	// Update is called once per frame
	void Update () {
		if(test == true){
			//call function
		}
	}

	static public void ProcessOutputGrid(string[,] grid)
	
	{
		for (int y = 1; y < grid.GetUpperBound(1) - 1; y++) {
		 	bool shouldCreateNew = true;
		 	int shouldChangeIndex = 0;
		 	for (int i = 0; i < categories.Count; i++){
		 		if (categories[i].grouping == grid[6,y] || (grid[6,y] == "" && categories[i].grouping == "random")){
		 			shouldCreateNew = false;
		 			shouldChangeIndex = i;
		 			break;	
		 		}
		 		
		 	}
		 
			Category category;
			if (shouldCreateNew){
				category = new Category();
				category.grouping = grid[6,y] == "0"? "random" : grid[6,y];
				category.choices = new List<Choices>();
				categories.Add(category);
				shouldChangeIndex = categories.Count - 1;
				// category.choices.Add(new Choices());
				// category.choices.statusUpdates = new StatusUpdates();
				// category.choices.requirements = new Requirements();
			}
			
			categories[shouldChangeIndex].choices.Add(new Choices{
				message = grid[16, y],
				time = grid[1,y] == "" ? 0 : Int32.Parse(grid[1, y]),
				nextGroup = grid[7, y] == "0"? "" : grid[7,y],
				cumulativeEffect = grid[8, y],
				addResponsibility = grid[9, y],
				addEffect = grid[10, y],
				statusUpdates = new StatusUpdates{
					happy = grid[2,y] == "" ? 0 : Int32.Parse(grid[2, y]),
					gold = grid[3,y] == "" ? 0 : Int32.Parse(grid[3, y]),
					skill = grid[4,y] == "" ? 0 : Int32.Parse(grid[4, y]),
					socialize = grid[5,y] == "" ? 0 : Int32.Parse(grid[5, y]),
				},
				requirements = new Requirements {
					gold = grid[11,y] == "" ? 0 : Int32.Parse(grid[11, y]),
					skill = grid[15,y] == "" ? 0 : Int32.Parse(grid[15, y]),
					socialize = grid[14,y] == "" ? 0 : Int32.Parse(grid[14, y]),
					responsibility = grid[12, y],
					effect = grid[13, y]
				}
			});	
		}
	}

 	// Split CSV grid 
	static public string[,] SplitCsvGrid(string csvText)
	{
		string[] lines = csvText.Split("\n"[0]); 
		// finds the max width of row
		int width = 0; 
		for (int i = 0; i < lines.Length; i++)
		{
			string[] row = SplitCsvLine( lines[i] ); 
			width = Mathf.Max(width, row.Length); 
		}
 
		// creates new 2D string grid to output to
		string[,] outputGrid = new string[width + 1, lines.Length + 1]; 
		for (int y = 0; y < lines.Length; y++)
		{
			string[] row = SplitCsvLine( lines[y] ); 
			for (int x = 0; x < row.Length; x++) 
			{
				outputGrid[x,y] = row[x]; 
 
				// This line was to replace "" with " in my output. 
				// Include or edit it as you wish.
				outputGrid[x,y] = outputGrid[x,y].Replace("\"\"", "\"");
			}
		}
 
		return outputGrid; 
	}
 
	// splits a CSV row 
	static public string[] SplitCsvLine(string line)
	{
		return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
		@"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)", 
		System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
		select m.Groups[1].Value).ToArray();
	}

	static public Category GetRandomGroup()
	{	
		Category item = new Category();
		// Cond 1: Specific category
		if (ScoreManager.nextGroup != ""){
			foreach(var category in categories){
				if (category.grouping == ScoreManager.nextGroup){
					item = category;
					break;
				}
			}	 
		} else {
			// Cond 2: Random category
			System.Random r = new System.Random(DateTime.Now.Millisecond);
			int rInt = r.Next(0, categories.Count);
			item = categories[rInt];
			// temp fix, assuming that each choice grouping has the same responsibility and effect requirements.
			while(!(item.choices[0].requirements.responsibility == "0" || ScoreManager.responsibility.Contains(item.choices[0].requirements.responsibility))
				 || !(item.choices[0].requirements.effect == "0" || ScoreManager.effect.Contains(item.choices[0].requirements.effect))
				){
				rInt = r.Next(0, categories.Count);
				item = categories[rInt];
			}
		}
		
		if (item.choices.Count != 3){ // got more than 3 choices
			System.Random r = new System.Random(DateTime.Now.Millisecond);
			while (item.choices.Count > 3){
				item.choices.RemoveAt(r.Next(0, item.choices.Count - 1));
			}
		}
		for (int i = 0; i < item.choices.Count; i++){
			int gold = item.choices[i].requirements.gold;
		    int skill = item.choices[i].requirements.skill;
			int socialize = item.choices[i].requirements.socialize;
			var choice = item.choices[i];
			choice.whyFail = "";
			if (ScoreManager.gold < gold){
				choice.whyFail = "Insufficient money"; 
			}
			if (ScoreManager.skill < skill){
				choice.whyFail = "Insufficient skills";
			}
			if (ScoreManager.socialize < socialize){
				choice.whyFail = "Insufficient social skills";
			}
			item.choices[i] = choice;

		}	

		return item;	
		

		
		// return new Category();
	}
}
