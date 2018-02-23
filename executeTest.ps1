Param(
	[Parameter(Position=0)]
	#[ValidateSet("QA", "STAGE")]
	[string]$CONFIGURATION="STG",
	[string]$PROJECTNAME = 'TMX.Automation',
	[string]$TESTSELECT = 'cat == Regression',
	[string]$BROWSER = 'Chrome'
)

# Example command 
# .\ExecuteTest.ps1 -environment STG

Function ResizePSWindow
{	
	$size=New-Object System.Management.Automation.Host.Size(70,30)
    $host.ui.rawui.WindowSize=$size   
}


try
{
	$DateTime = get-date -format ddd_ddMMyyyy_HHmmss

    $EXECUTIONLOG =$env:USERPROFILE

	Start-Transcript -path C:\Automation\logs\log_${DateTime}_$environment.log

	Set-Location $PSScriptRoot

	#Update Path
	$listDirectories = Get-ChildItem -Path .\packages -Include tools* -Recurse -Directory | Select-Object FullName
	foreach($directory in $listDirectories.FullName) {
		$env:Path+=";"+$directory
	}

    #$EXECUTIONLOG ="$env:USERPROFILE\log"
    $CheckPath = New-Object System.Collections.Generic.List[string] 
    $CheckPath.Add("$env:USERPROFILE\log")
    $CheckPath.Add("$env:USERPROFILE\Automation")

    $resulteRootDir="C:\AutomationReports\$DateTime"
	if (!(Test-Path $resulteRootDir)){
		New-Item -ItemType directory -Path $resulteRootDir
	}

    

	# File Names
	$PROJECT="$PSScriptRoot\$PROJECTNAME\$PROJECTNAME"+".csproj"
	$RESULTLOG="$env:USERPROFILE\$DATEYYYYMMDD"+"TestResult"+"$CONFIGURATION.log"
	$RESULTXML="$env:USERPROFILE\$DATEYYYYMMDD"+"TestResult"+"$CONFIGURATION.xml"
	$RESULTERR="$env:USERPROFILE\$DATEYYYYMMDD"+"StdErr"+"$CONFIGURATION.txt"
	$EXECUTIONREP="$env:USERPROFILE\$DATEYYYYMMDD"+"TMX.AutomationTest.ExecutionReport"+"$CONFIGURATION"
	$FEATURESDIR="$PSScriptRoot\$PROJECTNAME\Features"
	$XSLTFILE="$PSScriptRoot\NUnitExecutionReport.xslt"    


        $outputXmlPath = "$resulteRootDir\$DateTime.xml"

        $outputXmlPath = "$resulteRootDir\$DateTime.xml"
		$RESULTERR = "$resulteRootDir\$DateTime.txt"
		$RESULTLOG     ="$resulteRootDir\$DateTime"+".log"
		$outputHtmlPath = "$resulteRootDir\Report_${DateTime}_$environment.html"

		$projectPath = "$PSScriptRoot\$PROJECTNAME\$PROJECTNAME"+".csproj"
		$targetDll = "$PSScriptRoot\$PROJECTNAME\bin\Debug\$PROJECTNAME.dll"
		$OutDir = "$PSScriptRoot\$PROJECTNAME\bin\Debug"
		$resultsFile=$outputXmlPath

    $tests = (Get-ChildItem $OutDir -Recurse -Include *.TestAutomation.dll)

    }
Catch
{
    write-host $_.Exception.Message;
}
	
<#
	# Execute Tests
    $OUTPUT  = nunit3-console --framework=net-4.5 --out="$RESULTLOG" --result="$outputXmlPath;format=nunit2" --err="$RESULTERR" --where "$TESTSELECT" "$PROJECT" --config="$CONFIGURATION"
	
    $OUTPUT | Out-File $RESULTLOG 
    Write-Host $OUTPUT -Separator "`n"
    $TESTRESULTS = $OUTPUT | Select-String 'Test Count'
    $DURATION = $OUTPUT | Select-String 'Duration: '

    (Get-Content $RESULTLOG) | ForEach-Object { $_ -replace '=>', '*****' } | Set-Content $RESULTLOG

    #specflow nunitexecutionreport $PROJECT /testOutput:$RESULTLOG /xmlTestResult:$outputXmlPath /xsltFile:$XSLTFILE /out:$outputHtmlPath
	
	# Generate Specflow Report
	#specflow nunitexecutionreport $PROJECT /testOutput:$RESULTLOG /xmlTestResult:$RESULTXML /xsltFile:$XSLTFILE /out:$EXECUTIONREP.html


#>