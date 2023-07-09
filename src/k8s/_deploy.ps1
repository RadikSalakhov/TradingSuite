$target_file = "_deploy.yaml"
$separator = "---"
$PROD_VERSION = '1.0.0.0'

Clear-Content $target_file
Get-Content binance-worker.yaml | Add-Content $target_file

Add-Content $target_file $separator
Get-Content taapi-worker.yaml | Add-Content $target_file

Add-Content $target_file $separator
Get-Content assets-webapi.yaml | Add-Content $target_file

Add-Content $target_file $separator
Get-Content ticker-signalrhub.yaml | Add-Content $target_file

Add-Content $target_file $separator
Get-Content blazorapp.yaml | Add-Content $target_file

Add-Content $target_file $separator
Get-Content gateway.yaml | Add-Content $target_file

(Get-Content $target_file).replace('{PROD_VERSION}',$PROD_VERSION) | Set-Content $target_file

if($args[0] -eq "apply"){
    kubectl apply -f="environment.yaml" -f='sql-server.yaml' -f="rabbitmq.yaml" -f="$target_file"
}

if($args[0] -eq "delete"){
    kubectl delete -f="environment.yaml" -f='sql-server.yaml' -f="rabbitmq.yaml" -f="$target_file"
}