
            $ErrorActionPreference = 'Stop'
            $publishDir = "C:\Users\theophilusu\.jenkins\workspace\TestPipeline\publish"
            $deployDir = "C:\inetpub\wwwroot\Demo"
            
            # Ensure target directory exists
            if (Test-Path -Path $deployDir) {
                # Remove all files and folders in the target directory
                Remove-Item -Path $deployDir\* -Recurse -Force
            } else {
                # Create target directory if it doesn't exist
                New-Item -Path $deployDir -ItemType Directory
            }

            # Copy new files from publish folder to target directory
            Copy-Item -Path $publishDir\* -Destination $deployDir -Recurse

            # Optional: Verify files were copied
            Write-Host "Files deployed successfully to $deployDir"
            