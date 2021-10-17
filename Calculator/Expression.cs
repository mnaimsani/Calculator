using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public class Expression
    {
        public double LHS;
        public string Operator;
        public Expression RHS;
        public int ParentLevel;

        //public Expression(string expressionString, int parentLevel = 1)
        //{
        //    ParentLevel = parentLevel;
            
        //    var tokenQueue = new Queue<string>(expressionString.Split(" "));

        //    while (tokenQueue.Peek() == "(")
        //    {
        //        tokenQueue.Dequeue();
        //        ParentLevel++;
        //    }           

        //    LHS = double.Parse(tokenQueue.Dequeue());

        //    var nextParentLevel = ParentLevel;

        //    string test2 = tokenQueue.Peek();
        //    while (!string.IsNullOrEmpty(test2) && test2 == ")")
        //    {
        //        tokenQueue.Dequeue();
        //        nextParentLevel--;
        //        tokenQueue.TryPeek(out test2);
        //    }

        //    if (tokenQueue.Count > 0)
        //    {
        //        Operator = tokenQueue.Dequeue();
        //        RHS = new Expression(string.Join(" ", tokenQueue), nextParentLevel);
        //    }
        //}

        public Expression(string expressionString, int currentParentLevel = 1)
        {
            var tokenQueue = GetTokenQueue(expressionString);
            currentParentLevel = GetParentLevelFromOpenBracket(ref tokenQueue, currentParentLevel);
            SetParentLevel(currentParentLevel);
            AssignLHS(ref tokenQueue);           
            currentParentLevel = GetParentLevelFromCloseBracket(ref tokenQueue, currentParentLevel);

            if (tokenQueue.Count == 0)
                return;

            AssignRHS(ref tokenQueue, currentParentLevel);
        }

        private Queue<string> GetTokenQueue(string expressionString)
        {
            return new Queue<string>(expressionString.Split(" "));
        }

        private int GetParentLevelFromOpenBracket(ref Queue<string> tokenQueue, int currentParentLevel)
        {
            while (tokenQueue.TryPeek(out string result) 
                && result == "(")
            {
                tokenQueue.Dequeue();
                currentParentLevel++;
            }

            return currentParentLevel;
        }

        private void SetParentLevel(int currentParentLevel)
        {
            ParentLevel = currentParentLevel;
        }

        private void AssignLHS(ref Queue<string> tokenQueue)
        {
            if (double.TryParse(tokenQueue.Dequeue(), out double value))
            {
                LHS = value;
                return;
            }

            throw new FormatException("Invalid expression");
        }

        private int GetParentLevelFromCloseBracket(ref Queue<string> tokenQueue, int currentParentLevel)
        {
            while (tokenQueue.TryPeek(out string result)
                && result == ")")
            {
                tokenQueue.Dequeue();
                currentParentLevel--;
            }

            return currentParentLevel;
        }

        private void AssignRHS(ref Queue<string> tokenQueue, int currentParentLevel)
        {
            Operator = tokenQueue.Dequeue();
            RHS = new Expression(string.Join(" ", tokenQueue), currentParentLevel);
        }
    }
}
