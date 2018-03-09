Param(
	[Parameter(Position=0)]
	#[ValidateSet("QA", "STAGE")]
	[string]$CONFIGURATION="QA",
	[string]$PROJECTNAME = 'TMX.Automation',
	[string]$SELECTTEST = 'cat == Regression',
	[boolean]$UPDATEBUILD = $false
)
Function ResizePSWindow
{	
	$size=New-Object System.Management.Automation.Host.Size(70,30)
    $host.ui.rawui.WindowSize=$size   
}

Function ReadTags
{
}

try
{
	$DateTime = get-date -format ddd_ddMMyyyy_HHmmss
	$DATEYYYYMMDD=(Get-Date).ToString('yyyyMMddHHmmss')

	Start-Transcript -path "C:\evidence\logs\ReportExecution$DATEYYYYMMDD.log"

	# PRECONDITIONS
	# *************
	# Usage: TESTSELECT is the test select language use by Nunit3 in order to filter execution

	# Change to the Project directory
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
	$DLL = "$PSScriptRoot\$PROJECTNAME\bin\$CONFIGURATION\$PROJECTNAME.dll"

	# AutoUpdate and Build code
	IF($UPDATEBUILD){
		git pull
		nuget restore $SOLUTION
		Invoke-Expression "msbuild.exe $SOLUTION /target:Clean /target:Build /p:Configuration=$CONFIGURATION"
	}

	ResizePSWindow

	# Run Tests
	nunit3-console --framework=net-4.5 --result="$RESULTXML;format=nunit2" --out=$RESULTTXT $DLL --err="$RESULTERR" --where $SELECTTEST --config="$CONFIGURATION"
   
	# Generate Html Report
    specflow nunitexecutionreport $PROJECT /xmlTestResult:$RESULTXML /xsltFile:$XSLTFILE /out:$RESULTHTML

	Stop-Transcript

	#.\sendReport.ps1 -file $RESULTHTML -browser "Chrome"
}
catch
{
}

	


<#	[xml]$xmlIncludeDocument = (Get-Content "$PSScriptRoot\featureTags.xml")
    [string]$include = "";
    foreach($tag in $xmlIncludeDocument.Tags)
    {
        if($tag.childNodes.Count -eq 0){
            $where = ""
        }
        if($tag.ChildNodes.Count -ne 0)
        {
            if($tag.ChildNodes[0].'#text' -ne "Include" -and $tag.ChildNodes[0].'#text' -ne "NA"){
                $include = $include + "|" + $tag.ChildNodes[0].'#text';        
        }
    }
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
	#.\sendReport.ps1 -file $RESULTHTML -browser "Chrome"
#>
	

