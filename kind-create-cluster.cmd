call kind create cluster --config kind-config.yaml || goto error

@REM Instalacja ArgoCD
call kubectl create namespace argocd || goto error
call kubectl apply -n argocd -f https://raw.githubusercontent.com/argoproj/argo-cd/stable/manifests/install.yaml || goto error

@echo.
@echo -- Klaster gotowy do utworzenia aplikacji. --

@goto :EOF

:error
@echo Blad! %errorlevel%.
@exit /b %errorlevel%