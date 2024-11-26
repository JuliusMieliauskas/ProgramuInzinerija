## How to test the coverage by %
Firstly, use this command to generate an xml file
```
dotnet test --collect:"XPlat Code Coverage"
```

Use the command below ( in the command line as a single line ) to generate an HTML file, that checks the coverage percentage.

```
reportgenerator
 -reports:"X:\ProgramuInzinerija\Tests\TestResults\{ID}\coverage.cobertura.xml"
 -targetdir:"coveragereport"
 -reporttypes:Html
```

After that, open the "index.html" in "coveragereport" folder.
