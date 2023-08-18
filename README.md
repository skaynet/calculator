# Calculator

Console calculator, parses the formula according to the algorithm Abstract Syntax Tree (AST) 

## Task

Implement two modes for application “Calculator”:

1) Users work in a console application with simple operations (without brackets). Operations should be executed with math priority (* / + -)


Examples:

Input -> Output

“2+2*3” -> “8”

“2/0” -> Exception. Divide by zero.


2) Work with files. Application read data from file line by line. Implement calculation with brackets. Each line should be calculated and the result written to a different file.


Example:

Input file 

1+2*(3+2)

1+x+4

2+15/3+4*2


Output file

1+2*(3+2) = 11

1+x+4 = Exception. Wrong input.

2+15/3+4*2 = 15


Noties: implement parsing, calculation and math operation priorities without using third party libraries or components, which returns calculation result (like DataTable.Compute etc)