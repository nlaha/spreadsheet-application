# CptS321-Nathan_Laha-HWs

## StyleCop Changes

Disabled "fields should not begin with underscore"
- I prefer naming my private fields starting with "_" as it makes assigning them in constructors easier (I can have the same name without an underscore for the parameters)

Disabled "documentation should end with a period"
- This is just not necessary, it doesn't make the code cleaner and it's one more thing that I have to remember to do

Disabled "fields should be private"
- We need to have a protected member field for the Cell abstract class

Disabled "Opening square brackets should be spaced correctly"
- This contradicts with another rule for spacing after equal signs

Disabled "Closing parenthesis should be spaced correctly"
- This causes problems with the `!` operator when using it after parentheses, it forces an extra space which looks bad

**Note:**
GitHub Copilot has been used to autocomplete some comments/documentation.
