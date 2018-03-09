 Param(
	[string]$file,
    [string]$browser
 ) 
 
$From = "pradeep.chinthala@aeriestechnology.com"
$To = @("pradeep.chinthala@aeriestechnology.com")
$Cc = "pradeep.chinthala@epiqsystems.com"

$Attachment = $file
$Subject = "New Automation Execution Report on D06 Machine - $browser"
$Body = "New Automation Environment - Regression run on unity-tnetqa.eipqsystems.com has been completed. Please find the attached report for feature wise results."
$SMTPServer = "smtp.office365.com"
$SMTPPort = "587"
Write-Output $SMTPPort
Send-MailMessage -From $From -to $To -Cc $Cc -Subject $Subject `
-Body $Body -SmtpServer $SMTPServer -port $SMTPPort -UseSsl `
-Credential (New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList "pradeep.chinthala@aeriestechnology.com",("Welcome1234567!" | ConvertTo-SecureString -AsPlainText -Force)
) -Attachments $Attachment