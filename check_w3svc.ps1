
                    $w3svc = Get-Service -Name 'W3SVC' -ErrorAction SilentlyContinue
                    if ($w3svc -eq $null) {
                        Write-Host "W3SVC service is not installed."
                        exit 1
                    } else {
                        if ($w3svc.Status -ne 'Running') {
                            Write-Host "Starting W3SVC service..."
                            Start-Service -Name 'W3SVC'
                            Start-Sleep -Seconds 10
                        }
                        Write-Host "W3SVC service is running."
                    }
                    