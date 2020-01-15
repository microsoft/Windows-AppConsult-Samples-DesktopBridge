New-Item -Path $env:ProgramData -Name "MyEmployees" -ItemType "directory"
Copy-Item -Path "Employees.db" -Destination "$env:ProgramData\MyEmployees" -Recurse