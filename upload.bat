del .\build\publish.zip
powershell Compress-Archive .\build\wwwroot\* .\build\publish.zip
ssh root@sytko.de rm -r /opt/docker/project/blazor_test/http/*
scp -r .\build\publish.zip root@sytko.de:/opt/docker/project/blazor_test/http
ssh root@sytko.de unzip /opt/docker/project/blazor_test/http/publish.zip -d /opt/docker/project/blazor_test/http/