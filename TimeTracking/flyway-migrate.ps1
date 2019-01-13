$AppSettingsObject = (Get-Content .\FP.TimeTracking.Web\appsettings.json) -join "`n" | ConvertFrom-Json
$AppSettingsDbConnectionArray = $AppSettingsObject.ConnectionStrings.Storage -split ';'
$DbConnectionObject = New-Object PSObject;
foreach ($connectionSetting in $AppSettingsDbConnectionArray) { 
    $key, $value = $connectionSetting.split('=');
    if ($key) {
        $key = $key.replace(' ','')
        $DbConnectionObject | Add-Member -MemberType NoteProperty -Name $key -Value $value
    }
}

$env:PGPASSWORD=$DbConnectionObject.Password

$dbExist = psql.exe -U $DbConnectionObject.UserID -p $DbConnectionObject.Port -d postgres -tc "SELECT 1 FROM pg_database WHERE datname='$($DbConnectionObject.Database)'"

if($dbExist.Trim() -ne '1'){
   $createDb = 'CREATE DATABASE \"' + $DbConnectionObject.Database + '\"'; 
   $createDb
   psql.exe -U $DbConnectionObject.UserID -p $DbConnectionObject.Port -d postgres -c $createDb
}

$url = "-url=jdbc:postgresql://$($DbConnectionObject.Host):$($DbConnectionObject.Port)/$($DbConnectionObject.Database)";
$user = "-user=$($DbConnectionObject.UserID)"
$password = "-password=$($DbConnectionObject.Password)"
$locations = "-locations=filesystem:.\time-tracking\";

flyway $url $user $password $locations migrate
