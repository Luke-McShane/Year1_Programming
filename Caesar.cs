//IMPORT THE SYSTEM LIBRARY AND OTHER SUB-LIBRARYS FROM SYSTEM
using System;
using System.IO;
using System.Linq;

class Caesar
{
    public static void Main()
    {
		//CREATE AN INSTANCE OF ANOTHER CLASS TO ALLOW IT TO BE CALLED WITHIN THE CAESAR CLASS
		alterText alterText = new alterText();
		//ALLOW THE USER TO CHOOSE WHETHER THEY WANT TO ENCRYPT OR DECRYPT DATA
		Console.WriteLine("Would you like to Decrypt or Encrypt data? \nPlease type \"1\" for Encyption \nPlease type \"2\" for Decryption");
		//STORE THE USER'S INPUT AS A VARIABLE
		string userDecision = Convert.ToString(Console.ReadLine());
		//TWO IF STATEMENTS AND AN ELSE STATEMENT TO ENSURE THE PROGRAM IS ABLE TO DEAL WITH ALL USER INPUTS
		if(userDecision == "1")
		{
			alterText.encryptText();
		}
		
		else if(userDecision == "2")
		{
			alterText.decryptText();
		}
		
		else
		{
			//CALLS THE MAIN FUNCTION IF THE USER ENTERS AN INVALID INPUT, SO THE PROGRAM WILL RE-ASK FOR AN INPUT
			Console.WriteLine("Please enter a valid input");
			Main();
		}
    }	
}

class alterText
{
	public static void decryptText()
	{
		Console.WriteLine("Would you like to decrypt the default text or a specific text, or perhaps perform Frequency Analysis?\nPlease enter \"1\" for the default text\nPlease enter \"2\" for a specific text\nPlease enter \"3\" for Frequency Analysis\nPlease enter \"4\" for Affine decryption");
		string whichFunction = Convert.ToString(Console.ReadLine());
		if(whichFunction == "1")
		{
			try
			//TRY/CATCH STATEMENTS USED TO HANDLE ANY POTENTIAL ERROS
			{//OPENS THE TEXT FILE USER A STREAM READER, THIS IS WHERE ONE OF THE SUB-LIBRARYS IS USED
				using (StreamReader stream = new StreamReader("caesarShiftEncodedText.txt"))
				{
					//READS THE STREAM TO A STRING, STORING IT AS A VARIABLE
					string line = stream.ReadToEnd();
					//CALLS THE DECRYPT FUNCTION
					Decrypt(line);
				}
			}		
			catch (Exception e)
			{
				//NOTIFIES THE USER OF THE ERROR AND OUTPUTS IT TO THE CONSOLE
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}
		}
		
		else if(whichFunction == "2")
		{
			//THIS IMPLIES THAT THE USER HAS THEIR OWN TEXT FILE THAT THEY WOULD LIKE TO BE DECRYPTED 
			Console.WriteLine("Please enter the complete file path if the text file is not in the same folder this program is stored in. Otherwise, please enter the text file name (including \".txt\")");
			string myFile = Convert.ToString(Console.ReadLine());
			try
			{
			//OPENS THE TEXT FILE USER A STREAM READER, THIS IS WHERE ONE OF THE SUB-LIBRARYS IS USED
				using (StreamReader stream = new StreamReader(myFile))
				{
					//READS THE STREAM TO A STRING, STORING IT AS A VARIABLE
					string line = stream.ReadToEnd();
					Decrypt(line);
				}
			}		
			catch (Exception e)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}
		}
		
		else if(whichFunction == "3")
		{
			try
			{//OPENS THE TEXT FILE USER A STREAM READER, THIS IS WHERE ONE OF THE SUB-LIBRARYS IS USED
				using (StreamReader stream = new StreamReader("caesarShiftEnhancedEncoding.txt"))
				{
					//CREATES AN INSTANCE OF THE FREQUENCY ANALYSIS CLASS, ALLOWING IT TO BE CALLED AND FOR A VARIABLE(S) TO BE PASSED THROUGH IT
					frequencyAnalysis frequencyAnalysis = new frequencyAnalysis();
					//READS THE STREAM TO A STRING, STORING IT AS A VARIABLE
					string line = stream.ReadToEnd();
					frequencyAnalysis.fA(line);
				}
			}		
			catch (Exception e)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}
		}
		
		else if(whichFunction == "4")
		{
			try
			{//OPENS THE TEXT FILE USER A STREAM READER, THIS IS WHERE ONE OF THE SUB-LIBRARYS IS USED
				using (StreamReader stream = new StreamReader("caesarShiftEnhancedEncoding.txt"))
				{
					//CREATES AN INSTANCE OF THE FREQUENCY ANALYSIS CLASS, ALLOWING IT TO BE CALLED AND FOR A VARIABLE(S) TO BE PASSED THROUGH IT
					affineDecryption affineDecryption = new affineDecryption();
					//READS THE STREAM TO A STRING, STORING IT AS A VARIABLE
					string line = stream.ReadToEnd();
					affineDecryption.decryptAffine(line, 7, 3);
				}
			}		
			catch (Exception e)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}
		}
		else
		{
			Console.WriteLine("Please enter a valid input");
			decryptText();
		}
	
	}
	public static void encryptText()
	{
		//CREATES AN ARRAY STORING ALL THE LETTERS WITHIN THE ALPHABET, THE PROGRAM USES THIS TO SHIFT THE STRING BY X AMOUNT OF PLACES
		string[] alphabetArray = new string[] {"A","B","C","D","E","F","G","H","I",
		"J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};		
		//THIS IS AN ERROR-CHECK VARIABLE THAT IS USED TO CHECK IF THERE ARE INVALID CHARACTERS IN THE TEXT	
		Console.WriteLine("You have chose to encrypt your text. Please enter the shift key. Remember this key as you will need this when decrypting your text.");
		//REQUESTS A SHIFT KEY FROM THE USER
		int shiftKey = Convert.ToInt32(Console.ReadLine());
		Console.WriteLine("Please now enter the file path/file name (including \".txt\") that contains the plaintext");
		//REQUESTS A FILE PATH FROM THE USER
		string myFile = Convert.ToString(Console.ReadLine());
		try
		{
			using (StreamReader stream = new StreamReader(myFile))
			{
				//STORES ALL TEXT WITHIN THE FILE AS A STRING
				string userText = stream.ReadToEnd();
				//A SINGLE ITERATION IN THE FOR LOOP, THE USER'S STRING WILL SHIFT THE EXACT AMOUNT OF PLACES THEIR SHIFT KEY INDICATES
				for(int i=shiftKey; i==shiftKey; i++)
				{
					//CREATES AN EMPTY STRING WHERE THE ENCRYPTED TEXT WILL BE STORED 
					string encryptedText = "";
					//A TEMPORARY VARIABLE THAT WOULD OTHERWISE ITERATE IF THE USER'S TEXT WERE BEING DECRYPTED
					int currentIndex = i;
					foreach(char unchangedCharacter in userText)
					{
						string character =  (unchangedCharacter.ToString()).ToUpper();
						//CHECKS IF THERE IS A SPACE IN THE TEXT, IF SO, THEN THE SPACE IS WRITTEN TO THE ENCRYPTED TEXT STRING
						if (character == " ")
						{
							encryptedText += " ";
						}
						//CHECKS IF THERE IS AN APOSTROPHE, IF SO, WRITE IT TO THE ENCYRTED TEXT STRING
						else if (character == "'")
						{
							encryptedText += "'";
						}
						//CHECKS IF THERE IS LINE-BREAK, IF SO, WRITE IT TO THE ENCYRTED TEXT STRING
						else if(character == "\r\n" || character == "\n")
						{
							encryptedText += "\r\n";							
						}
						//CHECKS IF THERE IS ANOTHER CHARACTER THAT IS NOT IN THE ALPHABET, IF SO, IGNORE IT (HENCE THE CONTINUE KEYWORD)
						else if (!(alphabetArray.Contains(character)))
						{
							continue;
						}
						//THIS ELSE IF STATEMENT IS USED IF THERE IS A CHARACTER FROM THE ALPHABET THAT NEEDS TO BE ENCRYPTED
						else if (alphabetArray.Contains(character))
						{
							//STORES THE CURRENT CHARACTER'S INDEX AS AN INT VARIABLE
							int characterIndex = Array.IndexOf(alphabetArray, character);
							//CHECKS IF THE CHARACTER'S CURRENT INDEX PLUS THE SHIFT KEY WILL BE GREATER THAN 25
							if ((characterIndex + currentIndex) >25)
							{
								//IF SO, THEN MODULUS IS APPLIED SO THE NEW INDEX WILL START AT THE BEGINNING OF THE ALPHABET AND BEGIN ITERATING
								int finalIndex = (characterIndex + currentIndex)%26;
								//ADDS THE ENCRYPTED LETTER TO THE ENCRYPTED TEXT STRING BY FINDING THE RELEVANT LETTER IN THE ALPHABET ARRAY
								encryptedText += alphabetArray[finalIndex];
							}
							//THIS ELSE STATEMENT IS USED IF THE CURRENT INDEX OF THE LETTER ADDED TO THE SHIFT KEY IS LESS THAN 26, MEANING MODULUS DOES NOT NEED TO BE APPLIED
							else
							{
								//ADDS THE ENCRYPTED LETTER TO THE ENCRYPTED TEXT STRING BY FINDING THE RELEVANT LETTER IN THE ALPHABET ARRAY
								encryptedText += alphabetArray[characterIndex + currentIndex ];
							}
						}
						//IF THE LETTER DOES NOT MEET ANY OF THE IF ELSE STATEMENT CRITERIA, THEN THE LOOP CONTINUES AND REITERATES 
						else
						{
							continue;
						}
					}
					//PRINTS THE ENCRYPTED TEXT TO THE USER
					Console.WriteLine("Shift key: {0}", currentIndex);
					Console.WriteLine("Your encrypted text is as follows: \n{0}",encryptedText);
					//CALLS THE WRITE TO FILE METHOD AND PASSES THE ENCRYPTED TEXT THROUGH IT AS A VARIABLE
					writeToFile(encryptedText);
				}
			}
		}		
		catch (Exception e)
		{
			Console.WriteLine("The file could not be read:");
			Console.WriteLine(e.Message);
			encryptText();
		}
		
	}
	
	
	
	public static void Decrypt(string userText)
	{
		string[] alphabetArray = new string[] {"A","B","C","D","E","F","G","H","I",
		"J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};		
			
		//HERE THERE ARE 26 ITERATIONS TO ENSURE EVERY POSSIBLE CAESAR-SHIFT IS COVERED
		for(int i=25; i>=0; i--)
		{
			string decipheredText = "";
			int currentIndex = i;
			foreach(char unchangedCharacter in userText)
			{
				string character =  (unchangedCharacter.ToString()).ToUpper();
				if (character == " ")
				{
					decipheredText += " ";
				}
				else if (character == "'")
				{
					decipheredText += "'";
				}
				else if(character == "\r\n" || character == "\n")
				{
					decipheredText += "\r\n";				
				}
				else if (!(alphabetArray.Contains(character)))
				{
					continue;
				}
				else if (alphabetArray.Contains(character))
				{
					int characterIndex = Array.IndexOf(alphabetArray, character);
					
					if ((characterIndex + currentIndex) >25)
					{
						int finalIndex = (characterIndex + currentIndex)%26;
						decipheredText += alphabetArray[finalIndex];
					}
					
					else
					{
						decipheredText += alphabetArray[characterIndex + currentIndex ];
					}
				}
				else
				{
					continue;
				}
			}
			//WRITE THE POTENTIAL DECIPHERED TEXT TO THE USER 
			Console.WriteLine("Shift key: {0}", currentIndex);
			Console.WriteLine("{0} ", decipheredText);
			//ASKS WHETHER THIS IF THE CODE HAS BEEN DECIPHERED
			Console.WriteLine("Has this code been deciphered?");
			//STORES THE USER'S INPUT AS A STRING AND CONVERTS IT TO UPPERCASE TO IMPROVE EFFICIENCY IN THE CODE
			string hasItBeenDesciphered = (Console.ReadLine()).ToUpper();
			if (hasItBeenDesciphered == "YES")
			{
				//THE PROGRAM TELLS THE USER THAT IS HAS CRACKED THE CODE, THAT THE CODE WILL BE OUTPUT TO A TEXT FILE, AND INFORMS THE USER OF THE SHIFT KEY 
				Console.WriteLine("The program has cracked the code. /n The plaintext will now be written to a text file named \"decipheredText.txt\" \nThe shift key for this decryption is: {0}", i);
				writeToFile(decipheredText);
			}
			else
			{
				//INFORMS THE USER THAT THE PROGRAM WILL PERFORM THE NEXT ITERATION OF DECIPHER
				Console.WriteLine("The next iteration of deciphering:");
				//THE PROGRAM SLEEPS FOR 500 MILLISECONDS
				System.Threading.Thread.Sleep(500);
			}
		}	
	}
	
	//FINISHED TEXT IS STORED AS A STRING AS AN ARGUMENT
	public static void writeToFile(string finishedText)
	{
		//THIS QUESTION IMPROVES EFFIFCIENCY BY REMOVING THE NEED FOR TWO WRITE TO FILE METHODS TO BE CREATED 
		Console.WriteLine("Have you just encrypted or decrypted some text? Please enter \"Encrypted\" or \"Decrypted\"");
		string encryptedOrDecrypted = (Console.ReadLine()).ToUpper();
		if(encryptedOrDecrypted == "ENCRYPTED")
		{
			//CALLS THE WAIT TIME FUNCTION AND PASSES 2 VARIABLES THROUGH 
			waitTime(2, 3);
			//ACCESSES THE SYSTEM LIBRARY AND THE FILE KEYWORD, USING IT TO WRITE THE FINISHED TEXT TO THE DESIRED FILE 
			File.WriteAllText("cipherText.txt", finishedText);
			//THIS CLOSES THE PROGRAM AFTER WRITING HAS TAKEN PLACE 
			Environment.Exit(1);
		}
		else if(encryptedOrDecrypted == "DECRYPTED")
		{
			waitTime(2, 3);
			File.WriteAllText("decipheredText.txt", finishedText);
			Environment.Exit(1);
		}
		else
		{
			//CALLS THE FUNCTION AGAIN IF THE USER ENTERS AN INVALID INPUT 
				Console.WriteLine("Please enter a valid input");
				writeToFile(finishedText);
		}
		System.Threading.Thread.Sleep(1000);
	}
	

	
	//THIS IS A METHOD THAT SIMPLY ADDS SOME AESTHETICS TO THE PROGRAM WHEN WRITING TO A FILE
	//TWO INT VARIABLES ARE PASSED THROUGH AS ARGUMENTS
	public static void waitTime(int howManyRepititions, int howManyDots)
	{
		for(int j=1;j<=howManyRepititions;j++)
		{
			//THIS NESTED FOR STATEMENT IS NECESSARY AS THE USER HAS THE CHOICE TO SELECT HOW MANY REPITITIONS OF HOWEVER MANY "DOTS" ARE OUTPUT TO THE CONSOLE 
			for(int i=1;i<=howManyDots;i++)
			{
				//A 0.25 SECOND DELAY BETWEEN EACH DOT IS USED 
				System.Threading.Thread.Sleep(250);
				Console.Write(".");
			}
			//A 0.25 SECOND DELAY BETWEEN THE FINAL DOT AND THE NEXT ITERATION OF THE INITIAL FOR LOOP 
			System.Threading.Thread.Sleep(250);
			//THIS LINE CLEARS THE LATEST LINE FROM THE CONSOLE, THIS, ALONG WITH THE REST OF THIS METHOD, GIVES THE EFFECT THAT THE PROGRAM IS WRITING TO THE FILE 
			Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r"); 
		}
		Console.WriteLine("Data saved successfully");
	}
}


class frequencyAnalysis
{
	//PASSES THE UNANALYSED TEXT AS A STRING ARGUMENT 
	public static void fA(string unanalysedText)
	{
		int outputLetterIterator = 0;
		string[] alphabetArray = new string[] {"A","B","C","D","E","F","G","H","I",
		"J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
		//THIS ARRAY IS OF LENGTH 26, EACH INTEGER REPRESENTS THE AMOUNT OF OCCURENCES OF THAT LETTER IN THE UNANALYSED TEXT 
		int[] analysisArray = new int [] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
		
		foreach(char character in unanalysedText)
   		{
			//CHECKS IF THE CURRENT CHARACTER IS NOT IN THE ALPHABET, IF SO, THEN THE FOREACH LOOP "CONTINUES" BY PROGRESSING ONTO THE NEXT ITERATION 
			if (!(alphabetArray.Contains(character.ToString())))			
				continue;
			
			else
			{
				//THIS LINE OF CODE FINDS THE RELEVANT PLACE IN THE ANALYSIS ARRAY BY INDEXING THE ALPHABET ARRAY BY USING THE CURRENT CHARACTER. THE CODE THEN ITERATES THE INTEGER IN THE ANALYSYS ARRAY BY ONE AS THIS
				//SHOWS THAT THERE HAS BEEN AN OCCURENCE OF THE LETTER IN THE UNANALYSED STRING 
				analysisArray[Array.IndexOf(alphabetArray, character.ToString())] += 1;
			}
		}
		//THIS FOREACH LOOP ITERATES THROUGH THE ANALYSIS ARRAY AND PRINTS OUT THE VALUES OF EACH OF THE LETTERS IN A SINGLE LINE OF CODE 
		foreach(int val in analysisArray)
		{			
			Console.Write("{0}: {1}  ",alphabetArray[outputLetterIterator], val);
			outputLetterIterator += 1;
		}
		System.Threading.Thread.Sleep(1000);
	}
}

class affineDecryption
{
	//THREE VARIABLES ARE PASSED THROUGH AS ARGUMENTS
	public static void decryptAffine(string encryptedText, int a, int b)
	{
		string[] alphabetArray = new string[] {"A","B","C","D","E","F","G","H","I",
		"J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
		string plainText = "";
		//CREATES THE MULTIPLICATATIVE INVERSE AS A VARIABLE BY CALLING A FUNCTION WHICH CALCULATES IT 
		int modularMultiplicativeInverse = findInverse(a);
		foreach (char character in encryptedText)
		{
			if(character.ToString() == " ")
			{
				plainText += " ";
				continue;
			}
			else if (character.ToString() == "\r" || character.ToString() == "\r\n")
			{
				plainText += "\r\n";
				continue;
			}
			else if (!(alphabetArray.Contains(character.ToString())))
				continue;
			//CREATES A VARIABLE THAT STORES THE INDEX OF THE CURRENT DECRYPTED CHARACTER 
			int index = Array.IndexOf(alphabetArray, character.ToString());
			//USES THE DECRYPTION FORUMLA (TO REVERSE THE AFFINE ENCRYPTION)
			int newIndex = modularMultiplicativeInverse * (index - b);
			//CHECKS IF THE NEW, DECRYPTED INDEX (THAT WILL REPRESENT THE LETTER POSITION) IS LESS THAN 0
			if (newIndex < 0)
			{
				//IF THE NEW INDEX IS LESS THAN 0, WE TAKE REMAINDER WHEN THE NEW INDEX HAS BEEN DIVIDED BY 26 (THE MODULUS) AWAY FROM 26 
				newIndex = 26-(Math.Abs(newIndex) % 26);
			}
			else
				//IF THE NEW INDEX IS NOT LESS THAN 0, THERE ARE NO PROBLEMS AND WE CAN SIMPLY PERFORM THE MODULUS OF 26 ON THE NEW INDEX 
				newIndex = newIndex % 26;
			
			plainText += alphabetArray[newIndex];
		}
		Console.WriteLine("{0}", plainText);
		alterText alterText = new alterText();
		alterText.writeToFile(plainText);
	}
	
	public static int findInverse(int inverse)
	{
		//FLAG VARIABLE THAT WILL TURN FALSE IF THERE IS A POSSIBLE INVERSE 
		bool flag = true;
		//ITERATES EVERY POSSIBLE VIABLE VALUE 
		for (int i=0; i<=26; i++)
		{
			//CHECKS WHETHER THIS ITERATION EQUATES TO THE MODULAR MULTIPLICATATIVE INVERSE
			//USUALLY, AN INVERSE IS THE RECIPROCAL OF A NUMBER, MEANING 1/(THE ORIGINAL NUMBER), WHEN THE RECIPROCAL AND THE ORIGINAL NUMBER ARE MULTIPLIED TOGETHER, THE ANSWER WILL BE ONE 
			if ((inverse * i) % 26 == 1)
			{
				flag = false;
				return i;
				
			}
		}
		if (flag == true)
			Console.WriteLine("No inverse could be found");
			Environment.Exit(1);
			return 0;
	}
}



