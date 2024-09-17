# CptS321-Nathan_Laha-HWs

| Name     | WSU ID              | Email |
| ----------- | -------- | ------------------- |
| Nathan Laha            | 11762135         | nathan.laha@wsu.edu                    |

## StyleCop Changes

Disabled "fields should not begin with underscore"
- I prefer naming my private fields starting with "_" as it makes assigning them in constructors easier (I can have the same name without an underscore for the parameters)

Disabled "documentation should end with a period"
- This is just not necessary, it doesn't make the code cleaner and it's one more thing that I have to remember to do

Disabled "elements should be documented"
- Adding documentation to everything just adds clutter to the code. For example, the `Homework3Tests` project contains a class called `TestFibonacciReader`; Adding a documentation comment to that class won't help readability/maintainability. Especially since it's a test class so it probably won't be referenced in code directly. Similarly, for the test methods, Microsoft recommends having descriptive names such as `Add_MaximumSumResult_ThrowsOverflowException`, and these also do not require documentation comments.

Disabled "closing parenthesis should be spaced correctly"
- This conflicts with another rule