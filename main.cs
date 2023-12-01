using System;

class Program 
{
  private static string rock = "ROCK".ToUpper();
  private static string paper = "PAPER".ToUpper();
  private static string scissors = "SCISSORS".ToUpper();
  
  public static void Main (string[] args) 
  {
    Console.WriteLine("\t\tRock Paper Scissors\n\t\t\tBy Jarosław Rybak\n\nPlease Enter Your Name:");
    string playerName = Console.ReadLine();
    Console.WriteLine("Hello "+playerName);
    Console.WriteLine("\n");
    string difficulty = selectDifficulty();

    run(playerName, difficulty);
    Console.WriteLine("\t\tThank You For Playing");
  }

  public static void run(string playerName, string difficulty)
  {
    string playerSelection = "";
    string playerPreviousSelection = "";
    int playerScore = 0;
    string AISelection = "";
    int AIScore = 0;
    bool continueGame = true;

    while(continueGame)
    {
      playerSelection = playerTrun();

      //AI takes their turn
      if(difficulty == "Easy".ToUpper())
        AISelection = easy(playerPreviousSelection);
      else if(difficulty == "Random".ToUpper())
        AISelection = random();
      else if(difficulty == "Impossible".ToUpper())
        AISelection = impossible(playerSelection);

      //Compare AI vs Player
      if(playerSelection == "exit".ToUpper())
      {
        continueGame = false;
        saveScore(playerName, playerScore, AIScore, difficulty);
        return;
      }

      Console.WriteLine("\n\t"+playerName+" ("+playerSelection+") vs AI ("+AISelection+")");
      
      if(playerSelection == AISelection)
      {
        Console.WriteLine("\n\t\tTie:\n\t"+playerName+" Score ("+playerScore+")\t|\tAI Score ("+AIScore+")\n");
        playerPreviousSelection = playerSelection;
      }
      else if(playerSelection == rock)
      {
        if(AISelection == paper)
        {
          AIScore++;
          printScore("AI",playerName,playerScore,AIScore);
        }
        else if(AISelection == scissors)
        {
          playerScore++;
          printScore(playerName,playerName,playerScore,AIScore);
        }
        playerPreviousSelection = playerSelection;
      }
      else if(playerSelection == paper)
      {
        if(AISelection == scissors)
        {
          AIScore++;
          printScore("AI",playerName,playerScore,AIScore);
        }
        else if(AISelection == rock)
        {
          playerScore++;
          printScore(playerName,playerName,playerScore,AIScore);
        }
        playerPreviousSelection = playerSelection;
      }
      else if(playerSelection == scissors)
      {
        if(AISelection == rock)
        {
          AIScore++;
          printScore("AI",playerName,playerScore,AIScore);
        }
        else if(AISelection == paper)
        {
          playerScore++;
          printScore(playerName,playerName,playerScore,AIScore);
        }
        playerPreviousSelection = playerSelection;
      } 
      else
      {
        Console.WriteLine("\tERROR\tERROR\tERROR");
      }
    }
  }

  public static void saveScore(string playerName, int playerScore, int AIScore, string difficulty)
  {
    string scoreBaordEntry = playerName+" Score (Difficulty: "+difficulty+")\n\t"+playerName+": ("+playerScore+")\n\tAI: ("+AIScore+")\n";
    System.IO.File.AppendAllText("ScoreBaord.txt",scoreBaordEntry);    
  }

  public static void printScore(string victor, string playerName, int playerScore, int AIScore)
  {
    Console.WriteLine("\n\t\t\t"+victor+" Victory:\n\t"+playerName+" Score ("+playerScore+")\t|\tAI Score ("+AIScore+")\n\n");
  }

  public static string selectDifficulty()
  {
    string difficulties = "\tEasy\tRandom\t\tImpossible".ToUpper();
    string difficulty = "";
    bool incorrect = true;
    
    while(incorrect)
    {
      Console.WriteLine("Please Select Your Diffculty:\n"+difficulties);
      difficulty = Console.ReadLine().ToUpper();

      if(difficulties.Contains(difficulty))
      {
        Console.WriteLine ("You have selected: "+difficulty);
        incorrect = false;
      }
      else
      {
        Console.WriteLine ("Incorrect Diffculty: "+difficulty);
      }
    }

    return difficulty;
  }

  public static string playerTrun()
  {
    string options = $"\t{rock}\t{paper}\t{scissors}";
    string selected = "";
    bool incorrect = true;

    while(incorrect)
    {
      Console.WriteLine("Please Type Your Selection Or Exit To Exit The Game:\nOptions: "+options);
      selected = Console.ReadLine().ToUpper();

      if(selected == rock || selected == paper || selected == scissors ||selected=="EXIT")
      {
        Console.WriteLine ("You have selected: "+selected);
        incorrect = false;
      }
      else
      {
        Console.WriteLine ("Incorrect Selection: "+selected);
      }
    }
    
    return selected;
  }
  
  public static string easy(string playerIN)
  {
    if(playerIN == rock)
      return scissors;
    if(playerIN == paper)
      return rock;
    if(playerIN == scissors)
      return paper;
    return random();
  }

  public static string random()
  {
    string[] difficulties = {rock,paper,scissors};
    Random RNG = new Random();
    return difficulties[RNG.Next(3)];
  }

  //AI Behavior: will always choose the prfect option to win
  public static string impossible(string playerIN)
  {
    if(playerIN == rock)
      return paper;
    if(playerIN == paper)
      return scissors;
    if(playerIN == scissors)
      return rock;
    return random();
  }
}