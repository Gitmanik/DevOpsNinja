kubectl -n argocd get secret argocd-initial-admin-secret -o jsonpath="{.data.password}" > argocd-password-encoded
@certutil -decode argocd-password-encoded argocd-password-decoded >nul
@for /f "delims=" %%x in (argocd-password-decoded) do @set argocdpassword=%%x
@del argocd-password-*
@echo Domyslne haslo do ArgoCD: %argocdpassword%