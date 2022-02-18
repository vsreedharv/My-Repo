$RebootServers =get-content -path "c:\temp\RebootServersList.txt" 
  Foreach ($EveryServer in $RebootServers) 
    {

  Restart-Computer -ComputerName $EveryServer -force -AsJob

    }
