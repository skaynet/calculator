using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void CheckExpressionTest_EmptyExpression_ValidException()
        {
            // arrange
            string expression = "";
            Calculator calculator = new Calculator();

            // assert
            Assert.ThrowsException<ArgumentException>(()=>calculator.CheckExpression(expression));
        }

        [TestMethod]
        public void CheckExpressionTest_MissingClosingBracketExpression_ValidException()
        {
            // arrange
            string expression = "1+(2*2+3";
            Calculator calculator = new Calculator();

            // assert
            Assert.ThrowsException<FormatException>(() => calculator.CheckExpression(expression));
        }

        [TestMethod]
        public void CheckExpressionTest_MissingOpeningBracketExpression_ValidException()
        {
            // arrange
            string expression = "1+2*2)+3";
            Calculator calculator = new Calculator();

            // assert
            Assert.ThrowsException<FormatException>(() => calculator.CheckExpression(expression));
        }

        [TestMethod]
        public void CheckExpressionTest_IncorrectCountOpeningBracketExpression_ValidException()
        {
            // arrange
            string expression = "1+((2*2)+3";
            Calculator calculator = new Calculator();

            // assert
            Assert.ThrowsException<FormatException>(() => calculator.CheckExpression(expression));
        }

        [TestMethod]
        public void CheckExpressionTest_IncorrectCountClosingBracketExpression_ValidException()
        {
            // arrange
            string expression = "1+(2*2))+3";
            Calculator calculator = new Calculator();

            // assert
            Assert.ThrowsException<FormatException>(() => calculator.CheckExpression(expression));
        }

        [TestMethod]
        public void CalculateExpressionTest_EmptyBracketExpression_ValidException()
        {
            // arrange
            string expression = "1+()+3";
            Calculator calculator = new Calculator();

            // assert
            Assert.ThrowsException<FormatException>(() => calculator.CalculateExpression(expression));
        }

        [TestMethod]
        public void CalculateExpressionTest_DivideByZero_ValidException()
        {
            // arrange
            string expression = "1+2/0+3";
            Calculator calculator = new Calculator();

            // assert
            Assert.ThrowsException<DivideByZeroException>(() => calculator.CalculateExpression(expression));
        }

        [TestMethod]
        public void CalculateExpressionTest_MissingOneOperand_ValidException()
        {
            // arrange
            string expression = "1+2+3+";
            Calculator calculator = new Calculator();

            // assert
            Assert.ThrowsException<FormatException>(() => calculator.CalculateExpression(expression));
        }

        [TestMethod]
        public void CheckExpressionTest_LetterInsteadOfNumber_ValidException()
        {
            // arrange
            string expression = "1+x+3";
            Calculator calculator = new Calculator();

            // assert
            Assert.ThrowsException<ArgumentException>(() => calculator.CheckExpression(expression));
        }

        [TestMethod]
        public void CalculateExpressionTest_OperationPriorityCheck_ReturnValidNumber()
        {
            // arrange
            float expected = 4;
            string expression = "1+2/2*3";
            Calculator calculator = new Calculator();

            // act
            float actual = calculator.CalculateExpression(expression);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateExpressionTest_OperationPriorityCheckWithBrackets_ReturnValidNumber()
        {
            // arrange
            float expected = 15;
            string expression = "1+2/(1+1)*3+11";
            Calculator calculator = new Calculator();

            // act
            float actual = calculator.CalculateExpression(expression);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateExpressionTest_FractionalNumbers_ReturnValidNumber()
        {
            // arrange
            float expected = 5691.24F;
            string expression = "12.34+567.89*10";
            Calculator calculator = new Calculator();

            // act
            float actual = calculator.CalculateExpression(expression);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateExpressionTest_OperationPriorityCheckWithBracketsAndNegativeNumbers_ReturnValidNumber()
        {
            // arrange
            float expected = 93.35F;
            string expression = "11.5+(-2.33-3)*-4.55/5-(-5+-6)*7";
            Calculator calculator = new Calculator();

            // act
            float actual = calculator.CalculateExpression(expression);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}