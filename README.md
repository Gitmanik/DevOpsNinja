# Rozwiązanie zadania konkursowego DevOpsNinja

Autor: Paweł Reich, gitmanik.dev
E-mail użyty do rejestracji w konkursie: thegitman@wp.pl

# Opis działania poszczególnych skryptów:

## kind-create-cluster.cmd

Tworzy nowy klaster KinD, instaluje Contour oraz ArgoCD.

Contour oraz ArgoCD są instalowane oddzielnie w wyniku istnienia błędu https://github.com/kubernetes/kubectl/issues/1117, gdzie jest race-condition między deployowaniem CRD, a deployowaniem innych zasobów wykorzystujących je w tym samym Charcie.

## docker-image.cmd

Buduje obraz aplikacji z daną wersją (plik VERSION), taguje go oraz ładuje do KinD.

## apply-app.cmd

Ładuje zasób aplikacji ArgoCD do klastra.

## pfw-argocd.cmd

Uruchamia port forwarding dla ArgoCD. Przekierowanie 443->8080. https://localhost:8080

## argocd-pass.cmd

Odczytuje domyślne hasło dla ArgoCD.