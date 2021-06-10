$file = Get-Content solves.txt
$ext = ".sop.exam"
foreach($str in $file)
{
    $aStr = $str.Split(':')
    $result = $aStr[1]
    $fileName = $aStr[0]
    $size = $aStr[0]
    while($size[0] -lt "0" -or $size[0] -gt "9")
    {
        $size = $size.Substring(1);
    }
    while($size[$size.Length-1] -lt "0" -or $size[$size.Length-1] -gt "9")
    {
        $size = $size.Substring(0, $size.Length-1);
    }
    $fileName = $fileName.Trim()
    "$size $result" | Out-File "$fileName$ext"
}