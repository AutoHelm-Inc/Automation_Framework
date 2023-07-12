using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

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


            String[] nonFunctionKeywords = { "for", "if", "elif", "else", "true", "false" };
            this.keywords = new List<String>(Functions.GetNames(typeof(Functions)));
            this.keywords.AddRange(nonFunctionKeywords);
            
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
                    if ((currentState == State.START || currentState == State.KEYWORD) && Char.IsLetter(this.inputString[charIndex]))
                    {
                        currentState = State.KEYWORD;

                        sb.Append(this.inputString[charIndex]);
                        String tokenString = sb.ToString();

                        //If we ever have a succesful matach with a keyword, we add that to the tokens list
                        int foundKeyword = checkKeywords(tokenString);
                        if (foundKeyword != -1)
                        {
                            Token token = new Token(tokenString, Token.Type.KEYWORD);
                            tokensList.Add(token);

                            //Clear string builder and state after we finish
                            sb = sb.Clear();
                            currentState = State.START;
                        }
                    }
                    //Handle the case where we have numbers
                    else if (currentState == State.NUMBER || (currentState == State.START && Char.IsDigit(this.inputString[charIndex])))
                    {

                        if (currentState == State.NUMBER && !Char.IsDigit(this.inputString[charIndex]))
                        {
                            //If we are looking for a number, but get something that is not a digit, we know the number has ended
                            String tokenString = sb.ToString();
                            Token token = new Token(tokenString, Token.Type.NUMBER);
                            tokensList.Add(token);

                            sb = sb.Clear();
                            currentState = State.START;
                            charIndex -= 1;
                        }
                        else
                        {
                            //If we are still getting digits keep creating the number
                            currentState = State.NUMBER;
                            sb.Append(this.inputString[charIndex]);
                        }

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
                    //Handle all other cases like special like { and ! and add them as their own tokens
                    else
                    {
                        sb.Append(this.inputString[charIndex]);
                        String tokenString = sb.ToString();
                        Token token = new Token(tokenString, Token.Type.SYMBOL);
                        tokensList.Add(token);

                        sb = sb.Clear();
                        currentState = State.START;
                    }

                    charIndex += 1;

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
                return null;
            }

        }

        public int consumeNumber()
        {
            if (tokensList[index].type == Token.Type.NUMBER && index < tokensListLength)
            {
                this.index += 1;
                return Int32.Parse(tokensList[index].tokenText);
            }
            else
            {
                throw new Exception("Consume failed, your String is not a number or no tokens to consume!");
                return -1;
            }
        }

        public String consumeString()
        {
            if (tokensList[index].type == Token.Type.STRING && index < tokensListLength)
            {
                //Remove the first and last character because they are just quotation marks
                this.index += 1;
                return tokensList[index].tokenText.Substring(1, tokensList[index].tokenText.Length - 2);
            }
            else
            {
                throw new Exception("Consume failed, your String is not a String or no tokens to consume!");
                return null;
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

        public int checkKeywords(String compare)
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
        public void printTokens()
        {
            for (int i = 0; i < this.tokensList.Count; i++)
            {
                Console.WriteLine(this.tokensList[i].tokenText);
            }
        }

    }

}
