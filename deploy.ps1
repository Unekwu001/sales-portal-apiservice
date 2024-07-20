
                    $ErrorActionPreference = 'Stop'
                    $targetDir = "C:\inetpub\wwwroot"
                    
                    # Grant permissions to Jenkins user
                    & icacls "$targetDir" /grant 'admin:(OI)(CI)F'

                    
                    # Check if target directory exists
                    if (Test-Path -Path "C:\inetpub\wwwroot") {
                        # Check if there are files in the target directory
                        if (Test-Path -Path "C:\inetpub\wwwroot\*") {
                            # Remove all files and folders in the target directory
                            Remove-Item -Path "C:\inetpub\wwwroot\*" -Recurse -Force
                        }
                    }

                    # Copy new files from publish folder to target directory
                    Copy-Item -Path "C:\Users\theophilusu\.jenkins\workspace\TestPipeline\publish\*" -Destination "C:\inetpub\wwwroot" -Recurse
                    