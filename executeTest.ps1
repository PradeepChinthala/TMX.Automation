Param(
	[Parameter(Position=0)]
	#[ValidateSet("QA", "STAGE","UAT","DEV")]
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
	Start-Transcript -path C:\Automation\logs\log_${DateTime}_$environment.log

	Set-Location $PSScriptRoot

	#Update Path
	$listDirectories = Get-ChildItem -Path .\packages -Include tools* -Recurse -Directory | Select-Object FullName
	foreach($directory in $listDirectories.FullName) {
		$env:Path+=";"+$directory
	}
	# File Names
	$SOLUTION="$PSScriptRoot\TMX.Automation.sln"
	$PROJECT="$PSScriptRoot\$PROJECTNAME\TMX.TestAutomation.csproj"
	$RESULTLOG="$env:USERPROFILE\$DATEYYYYMMDD"+"TestResult"+"$CONFIGURATION.log"
	$RESULTXML="$env:USERPROFILE\$DATEYYYYMMDD"+"TestResult"+"$CONFIGURATION.xml"
	$RESULTERR="$env:USERPROFILE\$DATEYYYYMMDD"+"StdErr"+"$CONFIGURATION.txt"
	$EXECUTIONREP="$env:USERPROFILE\$DATEYYYYMMDD"+"TMX.AutomationTest.ExecutionReport"+"$CONFIGURATION"
	$FEATURESDIR="$PSScriptRoot\$PROJECTNAME\Features"
	$XSLTFILE="$PSScriptRoot\NUnitExecutionReport.xslt"


    $resulteRootDir="C:\AutomationReports\$DateTime"
		if (!(Test-Path $resulteRootDir)){
			New-Item -ItemType directory -Path $resulteRootDir
		}


    $outputXmlPath = "$resulteRootDir\$DateTime.xml"

        $outputXmlPath = "$resulteRootDir\$DateTime.xml"
		$RESULTERR = "$resulteRootDir\$DateTime.txt"
		$RESULTLOG     ="$resulteRootDir\$DateTime"+".log"
		$outputHtmlPath = "$resulteRootDir\Report_${DateTime}_$environment.html"

		$projectPath = "$PSScriptRoot\$PROJECTNAME\$PROJECTNAME"+".csproj"
		$targetDll = "$PSScriptRoot\$PROJECTNAME\bin\Debug\$PROJECTNAME.dll"
		$OutDir = "$PSScriptRoot\$PROJECTNAME\bin\Debug"
		$resultsFile="$outputXmlPath;format=nunit2"

    $tests = (Get-ChildItem $OutDir -Recurse -Include *.TestAutomation.dll)

	# AutoUpdate and Build code
	IF($UPDATEBUILD){
		git pull
		nuget restore $SOLUTION
		Invoke-Expression "msbuild.exe $SOLUTION /target:Clean /target:Build /p:Configuration=$CONFIGURATION"
	}

	# Execute Tests
    $OUTPUT  = nunit3-console --framework=net-4.5 --out="$RESULTLOG" --result="$outputXmlPath;format=nunit3" --err="$RESULTERR" --where "$TESTSELECT" "$PROJECT"
	
    $OUTPUT | Out-File $RESULTLOG 
    Write-Host $OUTPUT -Separator "`n"
    $TESTRESULTS = $OUTPUT | Select-String 'Test Count'
    $DURATION = $OUTPUT | Select-String 'Duration: '

    (Get-Content $RESULTLOG) | ForEach-Object { $_ -replace '=>', '*****' } | Set-Content $RESULTLOG

    specflow nunitexecutionreport $PROJECT /testOutput:$RESULTLOG /xmlTestResult:$outputXmlPath /xsltFile:$XSLTFILE /out:$outputHtmlPath

    #$OUTPUT = nunit3-console --framework=net-4.5 --out="$RESULTLOG" --result="$RESULTXML;format=nunit2" --err="$RESULTERR" --where "$TESTSELECT" "$PROJECT" --config="$CONFIGURATION"
	#$OUTPUT | Out-File "C:\evidence\logs\LogNUnit$DATEYYYYMMDD.log"
	#Write-Host $OUTPUT -Separator "`n"
	#$TESTRESULTS = $OUTPUT | Select-String 'Test Count'
	#$DURATION = $OUTPUT | Select-String 'Duration: '

	# Workaround: Change Nunit3 Output Log Format in order to match with Nunit2
	#(Get-Content $RESULTLOG) | ForEach-Object { $_ -replace '=>', '*****' } | Set-Content $RESULTLOG

	# Generate Specflow Report
	#specflow nunitexecutionreport $PROJECT /testOutput:$RESULTLOG /xmlTestResult:$RESULTXML /xsltFile:$XSLTFILE /out:$EXECUTIONREP.html
}
Catch
{
    write-host $_.Exception.Message;
}



#try
#{
#    $DateTime = get-date -format ddd_ddMMyyyy_HHmmss

#	Add-Type -AssemblyName PresentationCore,PresentationFramework
#	$ButtonType = [System.Windows.MessageBoxButton]::YesNo
#	$MessageIcon = [System.Windows.MessageBoxImage]::Error
#	$MessageBody = "Are you sure you want to run script in Environment [$environment]"
#	$MessageTitle = "Confirm Script Execution"
 
#	$Result = [System.Windows.MessageBox]::Show($MessageBody,$MessageTitle,$ButtonType,$MessageIcon)
 
#    if($Result -eq "Yes"){
#		#CreateResultDirectory
#		$resulteRootDir="C:\AutomationReports\$DateTime"
#		if (!(Test-Path $resulteRootDir)){
#			New-Item -ItemType directory -Path $resulteRootDir
#		}
#		#BaseDirectory
#		Set-Location $PSScriptRoot

#		#Update Path
#		$listDirectories = Get-ChildItem -Path .\packages -Include tools* -Recurse -Directory | Select-Object FullName
#		foreach($directory in $listDirectories.FullName) {
#			$env:Path+=";"+$directory
#		}

#		#$baseDir=Get-Location
#		SetParameter $environment    
		
#		$outputXmlPath = "$resulteRootDir\$DateTime.xml"
#		$RESULTERR = "$resulteRootDir\$DateTime.txt"
#		$RESULTLOG     ="$resulteRootDir\$DateTime"+".log"
#		$TESTCATEGORYFILE = "$PSScriptRoot\TestCategory.xml"

#		$projectPath = "$PSScriptRoot\$PROJECTNAME\$PROJECTNAME"+".csproj"
#		$targetDll = "$PSScriptRoot\$PROJECTNAME\bin\Debug\$PROJECTNAME.dll"
#		$OutDir = "$PSScriptRoot\$PROJECTNAME\bin\Debug"

#		#Select test category
#		[xml]$categoryFile = (Get-Content $TESTCATEGORYFILE)
#		[string]$TESTSELECT = "";
#        $lenth = $categoryFile.TestCategory.ChildNodes.Count
#        For ($i=0; $i -le $lenth-1; $i++) {
#        if($i -eq 0){$TESTSELECT = "cat == "+$categoryFile.TestCategory.ChildNodes[$i].'#text'}
#        if($i -ge 1){$TESTSELECT+=" || cat == "+$categoryFile.TestCategory.ChildNodes[$i].'#text'}                            
#        }			
        		
#		#Find project dll from project directory
#		$tests = (Get-ChildItem $OutDir -Recurse -Include *Tests.dll)
#		Write-Host "********** Tests Started in Environmenr - [$environment]: ($DateTime) **********" -foregroundcolor "black" -backgroundcolor "green"			
#		ResizePSWindow	
#        nunit3-console $tests --noheader --framework=net-4.5 --out="$RESULTLOG" --result="$outputXmlPath;format=nunit3" --err="$RESULTERR" --work=$resulteRootDir --where "$TESTSELECT"
		
#        ReportUnit $resulteRootDir "$resulteRootDir\HTMLReport" 
#	}
#	else
#	{
#		Write-Host "ERROR: Test Execution stopped [$environment] " -foregroundcolor "black" -backgroundcolor "red"
#	}

#}
#Catch {
#    write-host $_.Exception.Message;
#}