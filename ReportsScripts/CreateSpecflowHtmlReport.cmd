RMDIR "SpecflowWebDriver\bin\Debug\specflowHtmlReport" /S /Q
mkdir SpecflowWebDriver\bin\Debug\specflowHtmlReport
::"C:\Program Files (x86)\NUnit.org\nunit-console\nunit3-console.exe" --labels=all --out="SpecflowWebDriver\bin\Debug\specflowHtmlReport\TestResult.txt" --result="SpecflowWebDriver\bin\Debug\specflowHtmlReport\TestResult.xml"  SpecflowWebDriver\SpecflowWebDriver.csproj --where "cat==webtables" --result=nunit2
"C:\Program Files (x86)\NUnit 2.6.4\bin\nunit-console.exe" /labels /out="SpecflowWebDriver\bin\Debug\specflowHtmlReport\TestResult.txt" /xml="SpecflowWebDriver\bin\Debug\specflowHtmlReport\TestResult.xml"  SpecflowWebDriver\SpecflowWebDriver.csproj /include:webtables
::"packages\SpecFlow.2.0.0\tools\specflow.exe" nunitexecutionreport SpecflowWebDriver\SpecflowWebDriver.csproj /out:"SpecflowWebDriver\bin\Debug\specflowHtmlReport\SpecflowResults.html" /xmlTestResult:"SpecflowWebDriver\bin\Debug\specflowHtmlReport\TestResult.xml" /testOutput:"SpecflowWebDriver\bin\Debug\specflowHtmlReport\TestResult.txt"
"packages\SpecFlow.2.0.0\tools\specflow.exe" nunitexecutionreport SpecflowWebDriver\SpecflowWebDriver.csproj /out:"SpecflowWebDriver\bin\Debug\specflowHtmlReport\SpecflowResults.html" /xmlTestResult:"SpecflowWebDriver\bin\Debug\specflowHtmlReport\TestResult.xml" /testOutput:"SpecflowWebDriver\bin\Debug\specflowHtmlReport\TestResult.txt" /xsltFile:"SpecflowWebDriver\bin\Debug\specflowHtmlReport\ExecutionReport.xslt"
::"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" file:///%~dp0"\SpecflowWebDriver\bin\Debug\specflowHtmlReport\SpecflowResults.html"

:: Get path of current folder
set strPath=file:///%~dp0
:: Remove the string \SpecflowReportCommands\
set strPath=%strPath:\SpecflowReportCommands\=%

:: Change the character \ with /
set strPath=%strPath:\=/%

:: Join the end of the path
set strPath1=/bin/Debug/specflowHtmlReport/SpecflowResults.html
set strPathJoined=%strPath%%strPath1%

:: open the report with chrome
"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" %strPathJoined%