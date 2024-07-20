
                    $wmsvc = Get-Service -Name 'WMSVC' -ErrorAction SilentlyContinue
                    if ($wmsvc -eq $null) {
                        Write-Host "WMSVC service is not installed."
                        exit 1
                    } else {
                        if ($wmsvc.Status -ne 'Running') {
                            Write-Host "Starting WMSVC service..."
                            Start-Service -Name 'WMSVC'
                            Start-Sleep -Seconds 10
                        }
                        Write-Host "WMSVC service is running."
                    }
                    