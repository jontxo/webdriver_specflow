# Correct the images to make them appear correctly

#$subject =  "C:\GIT\SeleniumWebDriver\ExecutionResults\2016_03_09___1924\SpecflowExecutionResults2016_03_09___1924.html"
#$subjectModified = "C:\GIT\SeleniumWebDriver\ExecutionResults\2016_03_09___1924\SpecflowExecutionResults2016_03_09___1924_Modified.html"

## Get the matches in the file and create an array with them
#$subjectContent = Get-Content $subject
#$regex1 = [regex] '</span>file(?<=/span>file)([\s\S]*?)(?=.jpeg).jpeg'
#$Mathes1 = $regex1.Matches($subjectContent)

# Create a hash with the following elements:
# The indexes are the matches that are found in the file
# The regex value is the escaped value of the match that is needed to do the substitution
# The newValue is the litteral new value that is going to replace the index value
#$MatchesHash = @{}

#$Mathes1 | %{
#	 Write-Host "substring : " + $_.ToString().LastIndexOf("\") + 1 + " " + ($_.ToString().Length - $_.ToString().LastIndexOf("\"))
#	$MatchesHash.Add(($_),(@{
#		"regex" = [Regex]::Escape($_)
#		"newValue" = '<img class="manImg" src=".\' + $_.ToString().Substring($_.ToString().LastIndexOf("\") + 1, `
#					 $_.ToString().Length - $_.ToString().LastIndexOf("\") - 1) + '"></img>'
#	}))}

# </span>file:///C:\GIT\report\screenshot20160306202225.jpeg
#<img class="manImg" src=".screenshot20160306202225.jpeg"></img>

#$MatchesHash.GetEnumerator() | % {ForEach-Object{Write-Host + "key :" + $_.key + " value: " + $MatchesHash[$_.key]["regex"] }}
#$MatchesHash.GetEnumerator() | % {ForEach-Object{ $subjectContent = Get-Content $subject; `
#	$subjectContent -replace $MatchesHash[$_.key]["regex"], `
#	$MatchesHash[$_.key]["newValue"] | Out-File $subject -Enc Ascii}}

#% {ForEach-Object{$subjectContent = (Get-Content $subjectModified);
#	$match = [Regex]::Escape($_); Write-Host "match: " + $_.ToString();  `
#	($subjectContent -replace $match,$_.ToString().Substring($_.ToString().LastIndexOf("\") + 1, `
#		$_.ToString().Length - $_.ToString().LastIndexOf("\")) `
#	| Out-File $subject -Enc Ascii); Write-Host "found: " + $_.ToString().Substring($_.ToString().LastIndexOf("\") + 1,$_.ToString().Length - $_.ToString().LastIndexOf("\"))   }}

#(Get-Content $subject) | % {$_ -replace "</span>file(?<=/span>file)([\s\S]*?)(?=.jpeg).jpeg", `
#	"</span>file(?<=/span>file)([\s\S]*?)(?=.jpeg).jpeg".Substring(14,17)} | Out-File $subjectModified -Enc Ascii

#$pattern = "(?<=USE)([\s\S]*?)(?=GO)"
#$arr = (Get-Content $path -Raw | `
#Select-String -Pattern $pattern -AllMatches -CaseSensitive).Matches.Value | `
#ForEach-Object{$_.Trim()}

#$subjectModified = (Get-Content $subject)

#$Mathes = $regex.Matches($subject);

#$Mathes1 | %{
#	 Write-Host "substring : " + $_.ToString().LastIndexOf("\") + 1 + " " + ($_.ToString().Length - $_.ToString().LastIndexOf("\"))
#	$MatchesHash.Add(($_),(@{
#		"regex" = [Regex]::Escape($_)
#		"newValue" = '<img class="manImg" src=".\' + $_.ToString().Substring($_.ToString().LastIndexOf("\") + 1, `
#					 $_.ToString().Length - $_.ToString().LastIndexOf("\") - 1) + '"></img>'
#	}))}

#$subjectModified | ForEach-Object {
#	$Mathes = $regex.Matches($subject);  `
#	 $_ -replace '(?<=/span>)([\s\S]*?)(?=.jpeg).jpeg', `
#$string1 + `
#$_.ToString().Substring($_.ToString().LastIndexOf("\"), `
#						$_.ToString().Length) `
# + $string2} | `
#Out $subjectModified -Enc Ascii

#$regex = [regex] '</span>file(?<=/span>file)([\s\S]*?)(?=.jpeg).jpeg'
#(Get-Content  $subject) | ForEach-Object{
#		if ($regex.IsMatch($_))
#		{
#			$match = $regex.Match($_);
#			$matchEscaped = [Regex]::Escape($match)
#			$NewValue = '<img class="manImg" src=".\' +
#						$match.ToString().Substring( `
#							$match.ToString().LastIndexOf("\") + 1, `
#							$match.ToString().Length - $match.ToString().LastIndexOf("\") - 1) `
#							+ '"></img>'
#			($_ -replace $matchEscaped, $NewValue)
#			#Write-Host $_
#		}
#	else
#	{
#		$_
#	}
#		#Write-Host $_
#	} | Out-File $subjectModified

$ScreenshotsPath = $env:Temp + "\SpecflowExecutionResults" +  (Get-Date -Format "yyyy_MM_dd")+"\"
$SpecflowReportPath = "ExecutionResults\" +  (Get-Date -Format "yyyy_MM_dd___HHmm")+"\"
$SpecflowReportName = "SpecflowExecutionResults" + (Get-Date -Format "yyyy_MM_dd___HHmm") + ".html"
$SpecflowReportNameAndPath = $SpecflowReportPath + "SpecflowExecutionResults" + `
								(Get-Date -Format "yyyy_MM_dd___HHmm") + ".html"
$SpecflowReportTemplatesPath = "SpecflowWebDriver\SpecflowReport\Templates\"
$SpecflowProjectAndPath = "SpecflowWebDriver\SpecflowWebDriver.csproj"

# create the folder
New-Item -ItemType directory -Path $SpecflowReportPath

# Nunit execution of the tests
& "packages\NUnit.ConsoleRunner.3.2.0\tools\nunit3-console.exe" --labels=all `
--out=$SpecflowReportPath"TestResult.txt" --result=$SpecflowReportPath"TestResult.xml;format=nunit2" `
 $SpecflowProjectAndPath --where "cat==webtables"

 # replace the characters => with *****
#(Get-Content $SpecflowReportPath"TestResult.txt").replace('=>', '*****') `
#| Set-Content $SpecflowReportPath"TestResult.txt"

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
/xsltFile:$SpecflowReportTemplatesPath"NUnit-Dream-ExecutionReportTemplate.xslt"
#/xsltFile:$SpecflowReportTemplatesPath"NUnitExecutionReport.xslt"

#Fix the images links in the the report
$regex = [regex] '</span>file(?<=/span>file)([\s\S]*?)(?=.jpeg).jpeg'
(Get-Content  $SpecflowReportNameAndPath) | ForEach-Object{
		if ($regex.IsMatch($_))
		{
			$match = $regex.Match($_);
			$matchEscaped = [Regex]::Escape($match)
			$NewValue = '<img class="manImg" src=".\' +
						$match.ToString().Substring( `
							$match.ToString().LastIndexOf("\") + 1, `
							$match.ToString().Length - $match.ToString().LastIndexOf("\") - 1) `
							+ '"></img>'
			($_ -replace $matchEscaped, $NewValue)
		}
	else
	{
		$_
	}
} | Out-File $SpecflowReportNameAndPath