#固定语法
[CmdletBinding()]
#参数声明
param(
    [Parameter()][String] $DstDir,
    [Parameter()][string] $HttpUrl2Repo,
    [Parameter()][string] $Name ,
    [Parameter()][string] $SrcDir ,
    [Parameter()][string] $ZipName ,
    [Parameter()][string[]] $CompressPathList ,
    [Parameter()][string] $Comment,
    [Parameter()][string] $UserName,
    [Parameter()][string] $Mail
)
try 
{
    If(!(test-path -PathType container $DstDir))
    {
          New-Item -ItemType Directory -Path $DstDir
    }

    cd $DstDir

    git clone $HttpUrl2Repo

    cd $DstDir"\"$Name

    ROBOCOPY $SrcDir $DstDir"\"$Name /MIR   
   
    $compress = @{
        Path = $CompressPathList
        CompressionLevel = "Fastest"
        DestinationPath = $DstDir + "\" + $Name + "\" + $ZipName
    }
    Compress-Archive @compress

    git init

    git config --local user.name $UserName

    git config --local user.email $Mail

    git remote add origin $HttpUrl2Repo

    git add .

    git commit -m $Comment

    git push -u origin master
}
catch
{
        $ErrorMsg = $_.Exception.Message + " error raised while compress file!"
        Write-Host $ErrorMsg -BackgroundColor Red
}