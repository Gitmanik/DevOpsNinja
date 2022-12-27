call docker-image.cmd

rmdir /q /s generated-charts
mkdir generated-charts 

call kubectl delete all,configmap,ingress -l type=application --namespace=kalkulator || goto error

call helm dependencies update

call helm template kalkulator-kredytowy-charts > generated-charts/kalkulator.yaml || goto error

call kubectl apply -f generated-charts || goto error

@echo.
@echo Building cluster fully complete!
@goto :EOF

:error
@echo Error! %errorlevel%.
@exit /b %errorlevel%