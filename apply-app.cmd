call kubectl apply -f argocd-app.yaml || goto error

@echo.
@echo -- Aplikacja utworzona w klastrze --

@goto :EOF

:error
@echo Blad! %errorlevel%.
@exit /b %errorlevel%