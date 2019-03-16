#$SpecflowReportNameAndPath = "C:\GIT\SeleniumWebDriver\ExecutionResults\2016_03_12___0808\SpecflowExecutionResults2016_03_12___0808.html"
$ScreenshotsPath = $env:Temp + "\SpecflowExecutionResults" +  (Get-Date -Format "yyyy_MM_dd")+"\"
$SpecflowReportPath = "ExecutionResults\" +  (Get-Date -Format "yyyy_MM_dd___HHmm")+"\"
$SpecflowReportName = "SpecflowExecutionResults" + (Get-Date -Format "yyyy_MM_dd___HHmm") + ".html"
$SpecflowReportNameAndPath = $SpecflowReportPath + "SpecflowExecutionResults" + `
								(Get-Date -Format "yyyy_MM_dd___HHmm") + ".html"
$SpecflowReportTemplatesPath = "SpecflowWebDriver\SpecflowReport\Templates\"
$SpecflowProjectAndPath = "SpecflowWebDriver\SpecflowWebDriver.csproj"

# create the folder
New-Item -ItemType directory -Path $SpecflowReportPath

# Nunit 3 execution of the tests
& "packages\NUnit.ConsoleRunner.3.2.0\tools\nunit3-console.exe" --labels=All `
--out=$SpecflowReportPath"TestResult.txt" --result=$SpecflowReportPath"TestResult.xml;format=nunit2" `
 $SpecflowProjectAndPath   --where "cat==webtables | cat==API" --workers=3 --workers=3

  #--where "cat==webtables | cat==API" --workers=3

 ## Nunit 2.6.4 execution of the tests
 #& "C:\Program Files (x86)\NUnit 2.6.4\bin\nunit-console.exe" `
 #/labels /out=$SpecflowReportPath"TestResult.txt" `
 #/xml=$SpecflowReportPath"TestResult.xml"  `
 #SpecflowWebDriver\bin\debug\SpecflowWebDriver.dll /include:"API|webtables"

 # replace the characters => with *****
(Get-Content $SpecflowReportPath"TestResult.txt").replace('=>', '*****') `
| Set-Content $SpecflowReportPath"TestResult.txt"

# xlst Template of specflow project
# Copy-Item -Path $SpecflowReportTemplatesPath"NUnitExecutionReport.xslt" `
#-Destination $SpecflowReportPath"NUnitExecutionReport.xslt"

 #Move the images obtained to the report path
Move-Item -Path $ScreenshotsPath\*.jpeg `
-Destination $SpecflowReportPath

# #Move the report to the results folder
#Move-Item -Path $ScreenshotsPath $SpecflowReportName `
#-Destination $SpecflowReportNameAndPath

# generate specflow html report
& "packages\SpecFlow.2.0.0\tools\specflow.exe" nunitexecutionreport $SpecflowProjectAndPath `
/out:$SpecflowReportNameAndPath /xmlTestResult:$SpecflowReportPath"TestResult.xml" `
/testOutput:$SpecflowReportPath"TestResult.txt" `
/xsltFile:$SpecflowReportTemplatesPath"NUnit-Dream-ExecutionReportTemplate.xslt"

Write-Host  "packages\SpecFlow.2.0.0\tools\specflow.exe" nunitexecutionreport $SpecflowProjectAndPath `
/out:$SpecflowReportNameAndPath /xmlTestResult:$SpecflowReportPath"TestResult.xml" `
/testOutput:$SpecflowReportPath"TestResult.txt" `
#/xsltFile:$SpecflowReportTemplatesPath"NUnit-Dream-ExecutionReportTemplate.xslt"
#/xsltFile:$SpecflowReportTemplatesPath"NUnitExecutionReport.xslt"

#Fix the images links in the the report
#$SpecflowReportNameAndPath = Get-Content  $SpecflowReportNameAndPath
$regex = [regex] '</span>file(?<=/span>file)([\s\S]*?)(?=.jpeg).jpeg'
#$Mathes1 = $regex.Matches($SpecflowReportNameAndPath)
(Get-Content  $SpecflowReportNameAndPath) | ForEach-Object{
		if ($regex.IsMatch($_))
		{
			$match = $regex.Match($_);
			#Write-Host "Found Match: " + $Match
			$matchEscaped = [Regex]::Escape($match)
			$NewValue = '<img class="manImg" src=".\' +
						$match.ToString().Substring( `
							$match.ToString().LastIndexOf("\") + 1, `
							$match.ToString().Length - $match.ToString().LastIndexOf("\") - 1) `
							+ '"></img>'
			#Write-Host "New Value: " + $NewValue
			($_ -replace $matchEscaped, $NewValue)
		}
		else
		{
			$_
		}
} | Out-File $SpecflowReportNameAndPath