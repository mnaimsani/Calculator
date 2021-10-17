using System;

namespace Calculator
{
    public class Service
    {
        public double Calculate(string sum)
        {
            var expression = ConstructExpression(sum);
            var maxParentLevel = GetMaxParentLevel(expression);
            for (var i=maxParentLevel; i> 0; i--)
            {
                while (IsAnyOperationNotExecuted(expression, i))
                {
                    CalculateByParentLevel(ref expression, i);
                }
                UpdateParentLevel(ref expression, i);
            }

            return expression.LHS;
        }

        private Expression ConstructExpression(string expressionString)
        {
            return new Expression(expressionString);
        }

        public void CalculateByParentLevel(ref Expression expression, int parentLevel)
        {
            if (expression.RHS == null)
                return;

            if (expression.ParentLevel == parentLevel 
                && expression.RHS.ParentLevel == parentLevel)
            {
                switch (expression.Operator)
                {
                    case "+":
                        expression.RHS.LHS = expression.LHS + expression.RHS.LHS;
                        break;
                    case "-":
                        expression.RHS.LHS = expression.LHS - expression.RHS.LHS;
                        break;
                    case "*":
                        expression.RHS.LHS = expression.LHS * expression.RHS.LHS;
                        break;
                    case "/":
                        expression.RHS.LHS = expression.LHS / expression.RHS.LHS;
                        break;
                    default:
                        throw new FormatException("Invalid operator");
                }

                expression = expression.RHS;

                if (expression.RHS == null)
                    return;
            }
            
            CalculateByParentLevel(ref expression.RHS, parentLevel);
        }

        private int GetMaxParentLevel(Expression expression)
        {
            var maxParentLevel = 0;
            var currentRHS = expression.RHS;
            while(currentRHS != null)
            {
                if (currentRHS.ParentLevel > maxParentLevel)
                    maxParentLevel = currentRHS.ParentLevel;

                currentRHS = currentRHS.RHS;
            }

            return maxParentLevel;
        }

        private void UpdateParentLevel(ref Expression expression, int parentLevel)
        {
            if (expression.ParentLevel == parentLevel)
                expression.ParentLevel--;

            if (expression.RHS == null)
                return;

            UpdateParentLevel(ref expression.RHS, parentLevel);
        }

        private bool IsAnyOperationNotExecuted(Expression expression, int parentLevel)
        {
            var currentRHS = expression;
            while (currentRHS.RHS != null)
            {
                if (currentRHS.ParentLevel == parentLevel 
                    && currentRHS.ParentLevel == currentRHS.RHS.ParentLevel)
                    return true;

                currentRHS = currentRHS.RHS;
            }

            return false;           
        }
    }
}
