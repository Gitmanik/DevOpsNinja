call docker-image.cmd

call kubectl delete all,configmap,ingress -l type=application --namespace=kalkulator || goto error
del kalkulator-kredytowy-charts\Chart.lock
del kalkulator-kredytowy-charts\charts\*.*
call helm dependency build kalkulator-kredytowy-charts

@REM call helm template kalkulator-kredytowy-charts > generated-charts/kalkulator.yaml || goto error

call helm install kalkulator kalkulator-kredytowy-charts -n kalkulator || goto error

@echo.
@echo Building cluster fully complete!
@goto :EOF

:error
@echo Error! %errorlevel%.
@exit /b %errorlevel%