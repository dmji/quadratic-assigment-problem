Add-Type -AssemblyName System.Windows.Forms

#Dialog for input file
$FileBrowserIn = New-Object System.Windows.Forms.OpenFileDialog -Property @{ 
    InitialDirectory = [Environment]::GetFolderPath('Desktop') 
    Filter = 'XML (*.xml)|*.xml'
}
$dlgIn = $FileBrowserIn.ShowDialog()
if($dlgIn -ne "OK") { Write "Operation Canceled"; Pause; exit 0 }
$PathIn = $FileBrowserIn.FileName

if($args[0] -ne $null -and $args[1] -ne $null)
{
    C:\Users\leysh\source\repos\dmji\quadratic-assigment-problem\QAP-Solution\Console-Runner\bin\Release\netcoreapp3.1\Console-Runner.exe $args[0] $PathIn $args[1]
}


Pause