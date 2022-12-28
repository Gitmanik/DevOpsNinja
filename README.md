# Rozwiązanie zadania konkursowego DevOpsNinja

* Autor: Paweł Reich, https://gitmanik.dev
* E-mail użyty do rejestracji w konkursie: thegitman@wp.pl
* SonarQube: https://sonarcloud.io/project/overview?id=Gitmanik_kk-konkurs
* CycloneDX: W summary commitów w branchu main

# Opis działania poszczególnych skryptów:

## kind-create-cluster.cmd

Tworzy nowy klaster KinD oraz instaluje ArgoCD.

## docker-image.cmd

Buduje obraz aplikacji z daną wersją (plik VERSION), taguje go oraz ładuje do KinD.

## apply-app.cmd

Ładuje zasób aplikacji ArgoCD do klastra.

## pfw-argocd.cmd

Uruchamia port forwarding dla ArgoCD. Przekierowanie 443->8080. https://localhost:8080

## pfw-jaeger.cmd

Uruchamia port forwarding dla Jaeger UI. Przekierowanie 16686->16686. https://localhost:16686

## argocd-pass.cmd

Odczytuje domyślne hasło dla ArgoCD.