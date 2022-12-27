# Rozwiązanie zadania konkursowego DevOpsNinja

Autor: Paweł Reich, gitmanik.dev
E-mail użyty do rejestracji w konkursie: thegitman@wp.pl

# Opis działania poszczególnych skryptów:

## kind-create-cluster.cmd

Tworzy nowy klaster KinD oraz instaluje ArgoCD.

## docker-image.cmd

Buduje obraz aplikacji z daną wersją (plik VERSION), taguje go oraz ładuje do KinD.

## apply-app.cmd

Ładuje zasób aplikacji ArgoCD do klastra.

## pfw-argocd.cmd

Uruchamia port forwarding dla ArgoCD. Przekierowanie 443->8080. https://localhost:8080

## argocd-pass.cmd

Odczytuje domyślne hasło dla ArgoCD.