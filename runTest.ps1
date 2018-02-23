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
	$RESULTXML="$resultDir\$DATEYYYYMMDD"+"TestResult"+"$CONFIGURATION.xml"
	$RESULTTXT="$resultDir\$DATEYYYYMMDD"+"TestResult"+"$CONFIGURATION.txt"
	$RESULTERR="$resultDir\$DATEYYYYMMDD"+"StdErr"+"$CONFIGURATION.txt"
	$RESULTHTML = "$resultDir\$DATEYYYYMMDD"+"$CONFIGURATION.html"
    $XSLTFILE="$PSScriptRoot\NUnitExecutionReport.xslt"


	# Run Tests
	nunit3-console --framework=net-4.5 --result="$RESULTXML;format=nunit2" --out=$RESULTTXT $PROJECT --err="$RESULTERR" --where $SELECTTEST
    
    
    # Workaround: Change Nunit3 Output Log Format in order to match with Nunit2
    #(Get-Content $RESULTLOG) | ForEach-Object { $_ -replace '=>', '*****' } | Set-Content $RESULTLOG
    #specflow nunitexecutionreport $PROJECT /xmlTestResult:$RESULTXML /out:$RESULTHTML

    specflow nunitexecutionreport $PROJECT /xmlTestResult:$RESULTXML /xsltFile:$XSLTFILE /out:$RESULTHTML
	
}
catch
{

}