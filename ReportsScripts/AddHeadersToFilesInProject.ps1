param($target = "C:\GIT\SpecflowWebDriver\SpecflowWebDriver\", $companyname = 'demian INC')

#$target = "C:\GIT\SpecflowWebDriver\SpecflowWebDriver\";
#$companyname = 'demian INC';

#[System.Globalization.CultureInfo]
$ci = [System.Globalization.CultureInfo]::GetCurrentCulture

# Full date pattern with a given CultureInfo
# Look here for available String date patterns: http://www.csharp-examples.net/string-format-datetime/
$date = (Get-Date).ToString("F", $ci);

# Header template
$header = "// --------------------------------------------------------------------------------------------------------------------
// <Copyright file=""{0}"" company=""{1}"">
// Copyright (c) {1}. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------`r`n"

  function Write-Header ($file)
  {
	# Get the file content as as Array object that contains the file lines
	$content = Get-Content $file
	$content1 = ""

	# Getting the content as a String
	#$contentAsString = $content | Out-String
	$headerFinised = ""

	# remove the old header of the file
	((Get-Content  $file) | ForEach-Object{
		If (!$_.StartsWith("//") -and ($_.trim() -ne "") -and ([string]::IsNullOrWhitespace($headerFinised)))
		{
			$headerFinised = "Finished";
			$_
		}
		elseIf ($headerFinised.Equals("Finished"))
		{
			$_
		}
	}) | Tee-Object -Variable content

	# Splitting the file path and getting the leaf/last part, that is, the file name
	$filename = Split-Path -Leaf $file

	# $fileheader is assigned the value of $header with dynamic values passed as parameters after -f
	$fileheader = $header -f $filename, $companyname, $date

	# Writing the header to the file
	Set-Content $file $fileheader -encoding UTF8

	# Append the content to the file
	Add-Content $file $content
}

#Filter files getting only .cs ones and exclude specific file extensions
Get-ChildItem $target -Filter *.cs -Exclude TemporaryGeneratedFile*.cs, *.Designer.cs,T4MVC.cs,*.generated.cs,*.ModelUnbinder.cs -recurse | % `
{
	<# For each file on the $target directory that matches the filter, let's call the Write-Header function defined above passing the file as parameter #>
	Write-Header $_.PSPath.Split(":", 3)[2]
}

# In order to get run the script during a build phase, Visual Studio needs permissions to run power shell commands
#(http://stackoverflow.com/questions/5006619/call-powershell-script-in-post-built-with-parameters?rq=1).  Run the following batch script:

# This Script Must Be Run As An Administrator

# %SystemRoot%\SysWOW64\WindowsPowerShell\v1.0\powershell.exe "Set-ExecutionPolicy Unrestricted"
# %SystemRoot%\System32\WindowsPowerShell\v1.0\powershell.exe "Set-ExecutionPolicy Unrestricted"

#Then add the following to Visual Studio pre-build event:
#powershell.exe -file "$(SolutionDir)\Solution Items\AddCopyrightToAllClasses.ps1" -target $(ProjectDir)

#(In this case, I have saved the power shell script above as AddCopyrightToAllClasses.ps1 and saved it in a directory called scripts.
#The scripts directory is in the solution directory (not the project directory).