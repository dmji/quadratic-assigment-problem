$location = Get-Location
$location = $location.ToString() + "\results\"
$files = Get-ChildItem -Path $location -Force -Recurse

foreach($file in $files)
{
    $path = $file.Fullname
    if($path.Contains(".xml"))
    {
        $excel = new-object -comobject Excel.Application
        $excel.Visible = $false
        $excel.DisplayAlerts = $false
        $excel.ReferenceStyle = "xlR1C1"
        $workbook = $excel.Workbooks.Open($path)
        $sh = $workbook.sheets | where {$_.name -eq "Results"}


        $pathNew = $path.ToString().Replace(".xml", ".xlsx")
        $pathNew = $pathNew.Replace("\\", "\")
        $sh.SaveAs($pathNew, 51)
        $workbook.Close()
        $excel.Quit()
        Remove-Item $path
    }
}