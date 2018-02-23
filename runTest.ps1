Param(
	[Parameter(Position=0)]
	#[ValidateSet("QA", "STAGE")]
	[string]$CONFIGURATION="STG",
	[string]$PROJECTNAME = 'TMX.Automation',
	[string]$SELECTTEST = 'cat == Regression',
	[string]$BROWSER = 'Chrome'
)
Function ResizePSWindow
{	
	$size=New-Object System.Management.Automation.Host.Size(70,30)
    $host.ui.rawui.WindowSize=$size   
}

try
{
	$DateTime = get-date -format ddd_ddMMyyyy_HHmmss
	$DATEYYYYMMDD=(Get-Date).ToString('yyyyMMddHHmmss')

    Set-Location $PSScriptRoot

	# Automation Directory
	$resultDir="C:\Automation.TMX\$DateTime"
	if (!(Test-Path $resultDir)){
		New-Item -ItemType directory -Path $resultDir
	}

	#Update Path
	$listDirectories = Get-ChildItem -Path .\packages -Include tools* -Recurse -Directory | Select-Object FullName
	foreach($directory in $listDirectories.FullName) { $env:Path+=";"+$directory }

	# File Names
	$PROJECT="$PSScriptRoot\$PROJECTNAME\$PROJECTNAME"+".csproj"
	$TEST = "$PSScriptRoot\$PROJECTNAME\bin\$CONFIGURATION\$PROJECTNAME.dll"
	$RESULTXML="$resultDir\$DATEYYYYMMDD"+"TestResult"+"$CONFIGURATION.xml"
	$RESULTTXT="$resultDir\$DATEYYYYMMDD"+"TestResult"+"$CONFIGURATION.txt"
	$RESULTERR="$resultDir\$DATEYYYYMMDD"+"StdErr"+"$CONFIGURATION.txt"
	$RESULTHTML = "$resultDir\$DATEYYYYMMDD"+"$CONFIGURATION.html"


	# Run Tests
	nunit3-console --framework=net-4.5 --result="$RESULTXML;format=nunit3" --out=$RESULTTXT $TEST --err="$RESULTERR" --where $SELECTTEST
    
	specflow nunitexecutionreport $PROJECT /xmlTestResult:$RESULTXML /out:$RESULTHTML

#&$specflowPath nunitexecutionreport $projectPath /xmlTestResult:$outputXmlPath /out:$outputHtmlPath#
#specflow nunitexecutionreport $PROJECT /testOutput:$RESULTLOG /xmlTestResult:$RESULTXML /xsltFile:$XSLTFILE /out:$EXECUTIONREP.html

	<# 
nunit3-console --framework=net-4.5 --out="$RESULTLOG" --result="$RESULTXML;format=nunit2" --err="$RESULTERR" --where "$TESTSELECT" "$PROJECT" --config="$CONFIGURATION"
 --noheader --framework=net-4.5 --out="$RESULTLOG" --result="$outputXmlPath;format=nunit3" --err="$RESULTERR" --work=$resulteRootDir --where "$TESTSELECT"

	& nunit-console.exe /labels /result=$outputXmlPath /output=$outputTxtPath $binPath /framework:net-4.5 /include:$include
	& $specflowPath nunitexecutionreport $projectPath /xmlTestResult:$outputXmlPath /out:$outputHtmlPath
	#>


	<#
	$outputTxtPath = "C:\E2ETests\Unity-OUT\Output-Chrome.txt"
	$outputXmlPath = "C:\E2ETests\Unity-OUT\Result-Chrome.xml"
	$outputHtmlPath = "C:\E2ETests\Unity-OUT\E2EReport-Chrome.html" 
	#>
	
}
catch
{

}