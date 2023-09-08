using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Net.Security;

namespace Automation_Project.src.ast
{
    class Token
    {
        public enum Type
        {
            SYMBOL,
            ID,
            KEYWORD,
            STRING,
            NUMBER,
            EOF,
        }

        public String tokenText;
        public Type type;

        public Token(String tokenText, Type type)
        {
            this.tokenText = tokenText;
            this.type = type;
        }
    }
    class Lexer
    {
        private char[] inputString;
        private int index;
        private int tokensListLength;
        private List<String> keywords;
        private List<Token> tokensList;


        private enum State
        {
            START,
            KEYWORD,
            NUMBER,
            STRING,
        }

        public Lexer(String fileName)
        {
            this.inputString = (File.ReadAllText(fileName)).ToCharArray();
            this.tokensList = new List<Token>();
            this.tokensListLength = 0;
            this.index = 0;

            this.keywords = new List<String>(Functions.GetNames(typeof(Functions)));
            this.keywords.AddRange(new List<String>(Keywords.GetNames(typeof(Keywords))));
            this.tokenize();
            
        }

        public void tokenize()
        {

            int charIndex = 0;
            State currentState = State.START;
            StringBuilder sb = new StringBuilder();

            while (charIndex < inputString.Length)
            {
                //First we ensure the current character is not any form of whitespace, otherwise eliminate it
                if (Char.IsWhiteSpace(this.inputString[charIndex]) && currentState != State.STRING)
                {
                    if (charIndex == inputString.Length - 1)
                    {
                        break;
                    }
                    else
                    {
                        charIndex++;
                        continue;
                    }
                }
                else
                {
                    //Handle the case where we have a keyword since keywords only have letters
                    if ((currentState == State.START|| currentState == State.KEYWORD) && Char.IsLetter(this.inputString[charIndex]))
                    {
                        currentState = State.KEYWORD;

                        sb.Append(this.inputString[charIndex]);
                        String tokenString = sb.ToString();

                        //If we ever have a succesful matach with a keyword, we also need to check if there are matches with other substrings
                        //for instance, Write and WriteLine
                        if (checkMatch(tokenString) >= 0)
                        {
                            ArrayList checkKeywordsRet = checkSubstringMatch(tokenString);

                            //Here, if we only have one match, it will be the root word, ex. Write, so we just add it to tokens
                            //We also add if we are at the end of the inputString
                            if (checkKeywordsRet.Count == 1 || (checkKeywordsRet.Count > 1 && (charIndex >= inputString.Length-1)))
                            {
                                Token token = new Token(tokenString, Token.Type.KEYWORD);
                                tokensList.Add(token);

                                //Clear string builder and state after we finish
                                sb = sb.Clear();
                                currentState = State.START;
                            }
                            //However, if there are multiple matches, we check to see if there are any "future" matches, but
                            //if not, we just add the original match token
                            else if (checkKeywordsRet.Count > 1)
                            {

                                bool foundPossibleMatch = findAlternateMatches(tokenString, charIndex, checkKeywordsRet);

                                if (!foundPossibleMatch)
                                {
                                    Token token = new Token(tokenString, Token.Type.KEYWORD);
                                    tokensList.Add(token);

                                    //Clear string builder and state after we finish
                                    sb = sb.Clear();
                                    currentState = State.START;
                                }

                            }
                        }
                    }
                    //Handle the case where we have numbers
                    else if ((currentState == State.NUMBER || currentState == State.START) && Char.IsDigit(this.inputString[charIndex]))
                    {

                        //If we are still getting digits keep creating the number
                        currentState = State.NUMBER;
                        sb.Append(this.inputString[charIndex]);

                    }
                    else if ((currentState == State.STRING) || (currentState == State.START && this.inputString[charIndex] == '\"'))
                    {
                        if (currentState == State.STRING && this.inputString[charIndex] == '\"')
                        {
                            //This condition  means we found the ending quote, so token complete
                            sb.Append(this.inputString[charIndex]);
                            String tokenString = sb.ToString();

                            Token token = new Token(tokenString, Token.Type.STRING);
                            tokensList.Add(token);

                            sb = sb.Clear();
                            currentState = State.START;

                        }
                        else
                        {
                            //If we still have not found ending quote, keep adding the characters
                            currentState = State.STRING;
                            sb.Append(this.inputString[charIndex]);
                        }
                    }
                    else if (currentState == State.START && !Char.IsLetterOrDigit(this.inputString[charIndex]))
                    {
                        //Handle all other cases like special like { and ! and add them as their own tokens
                        sb.Append(this.inputString[charIndex]);
                        String tokenString = sb.ToString();
                        Token token = new Token(tokenString, Token.Type.SYMBOL);
                        tokensList.Add(token);

                        sb = sb.Clear();
                        currentState = State.START;
                    }
                
                    else
                    {
                        //Here we handle boundary cases like when we are building a keyword but we suddenly get a number
                        //We create the new keyword token and start building a number by changing the state
                        if (currentState ==  State.KEYWORD)
                        {
                            String tokenString = sb.ToString();
                            Token token = new Token(tokenString, Token.Type.KEYWORD);
                            tokensList.Add(token);

                            sb = sb.Clear();
                            currentState = State.START;
                            charIndex -= 1;
                        }
                        else if (currentState == State.NUMBER)
                        {
                            String tokenString = sb.ToString();
                            Token token = new Token(tokenString, Token.Type.NUMBER);
                            tokensList.Add(token);

                            sb = sb.Clear();
                            currentState = State.START;
                            charIndex -= 1;
                        }
                        else if (currentState == State.STRING)
                        {
                            sb.Append("\"");
                            String tokenString = sb.ToString();
                            Token token = new Token(tokenString, Token.Type.STRING);
                            tokensList.Add(token);

                            sb = sb.Clear();
                            currentState = State.START;
                            charIndex -= 1;
                        }
                        else
                        {
                            char err = this.inputString[charIndex];
                            throw new Exception("Lexer found an unexpected character: " + err);
                        }
                        
                    }

                    charIndex += 1;

                }

            }

            //In case we cannot finish building a token by the end of the input stream, we create the respective token
            if (sb.Length > 0)
            {
                if (currentState == State.KEYWORD)
                {
                    String tokenString = sb.ToString();
                    Token token = new Token(tokenString, Token.Type.KEYWORD);
                    tokensList.Add(token);

                    sb = sb.Clear();
                    currentState = State.START;
                }
                else if (currentState == State.NUMBER)
                {
                    String tokenString = sb.ToString();
                    Token token = new Token(tokenString, Token.Type.NUMBER);
                    tokensList.Add(token);

                    sb = sb.Clear();
                    currentState = State.START;
                }
                else if (currentState == State.STRING)
                {
                    sb.Append("\"");
                    String tokenString = sb.ToString();
                    Token token = new Token(tokenString, Token.Type.STRING);
                    tokensList.Add(token);

                    sb = sb.Clear();
                    currentState = State.START;
                }
            }
            this.tokensListLength = tokensList.Count;

        }
        public bool inspect(String s)
        {
            //Check if the string is the same as the token at current index
            if (index < tokensListLength)
            {
                return s.Equals(tokensList[index].tokenText);
            }
            //If we have consumed all tokens already we should return false
            else
            {
                return false;
            }
        }


        public bool inspectNumber()
        {
            if (index < tokensListLength)
            {
                return tokensList[index].type == Token.Type.NUMBER;
            }
            else
            {
                return false;
            }
        }

        public bool inspectString()
        {
            if (index < tokensListLength)
            {
                return tokensList[index].type == Token.Type.STRING;
            }
            else
            {
                return false;
            }
        }

        public bool inspectEOF()
        {
            //If the index is past all tokens in the list then that means there are no other tokens as thus we reached EOF
            if (index >= tokensListLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public String consume(String s)
        {

            if (s.Equals(tokensList[index].tokenText) && index < tokensListLength)
            {
                this.index += 1;
                return s;
            }
            else
            {
                throw new Exception("Consume failed, your String is not a token or no tokens to consume!");
            }

        }

        public int consumeNumber()
        {
            if (tokensList[index].type == Token.Type.NUMBER && index < tokensListLength)
            {
                Int32 num = Int32.Parse(tokensList[index].tokenText);
                this.index += 1;
                return num;
            }
            else
            {
                throw new Exception("Consume failed, your String is not a number or no tokens to consume!");
            }
        }

        public String consumeString()
        {
            if (tokensList[index].type == Token.Type.STRING && index < tokensListLength)
            {
                //Remove the first and last character because they are just quotation marks
                String s = tokensList[index].tokenText.Substring(1, tokensList[index].tokenText.Length - 2);
                this.index += 1;
                return s;
            }
            else
            {
                throw new Exception("Consume failed, your String is not a String or no tokens to consume!");
            }
        }

        public bool consumeEOF()
        {
            if (index >= tokensListLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int checkMatch(String compare)
        {
            for (int i = 0; i < this.keywords.Count; i++)
            {
                if ((this.keywords[i]).Equals(compare))
                {
                    return i;
                }
            }
            return -1;
        }
        private ArrayList checkSubstringMatch(String compare)
        {
            ArrayList ret = new ArrayList();
            for (int i = 0; i < this.keywords.Count; i++)
            {
                if ((this.keywords[i]).Equals(compare) || (this.keywords[i]).StartsWith(compare))
                {
                    ret.Add(this.keywords[i]);
                }
            }
            return ret;
        }

        private bool findAlternateMatches(String compare, int charIndex, ArrayList checkKeywordsRet)
        {
            foreach (String str in checkKeywordsRet)
            {
                if (!compare.Equals(str) && str.Length > compare.Length)
                {
                    //for each potential match, see if there is a full match for any of the strings
                    int i = 0;
                    bool isMatch = false;

                    while (((charIndex + i) < this.inputString.Length) && (compare.Length + i < str.Length))
                    {
                        char Comp1 = this.inputString[charIndex + 1 + i];
                        char Comp2 = str[compare.Length + i];
                        if ((Comp1 != Comp2))
                        {
                            isMatch = false;
                            break;
                        }
                        else
                        {
                            isMatch = true;
                        }
                        i += 1;
                    }

                    if (isMatch)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public void printTokens()
        {
            for (int i = 0; i < this.tokensList.Count; i++)
            {
                Console.WriteLine(this.tokensList[i].tokenText);
            }
        }

    }

}
