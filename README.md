# Rozwiązanie zadania konkursowego DevOpsNinja

* Autor: Paweł Reich, https://gitmanik.dev
* E-mail użyty do rejestracji w konkursie: thegitman@wp.pl
* SonarQube: https://sonarcloud.io/project/overview?id=Gitmanik_kk-konkurs
* CycloneDX: W summary commitów w branchu main

# Rozwiązania:

* AUTOMATYZACJA BUDOWANIA I INSTALACJI APLIKACJI + TESTOWANIE I SKANOWANIE APLIKACJI:
    - Zmiany w repozytorium wywołują Workflow w GH Action, który traktuje go SonarQube, CycloneDX, a nastepnie buduje paczkę Helm Chart i buduje obraz kontenera, które hostowane są na GH Pages i GH Packages repo. 
* AUTOSCALING APLIKACJI NA PODSTAWIE OBCIĄŻENIA:
    - Skalowanie wykonuje HPA
* TRACING W APLIKACJI
    - Jaeger dostępny po sforwardowaniu portu skryptem **pfw-jaeger.cmd**
* WDRAŻANIE APLIKACJI W TRYBIE CANARY-DEPLOYMENT
    - Rozwiązanie działa na Argo Rollouts + ingress-nginx
* URUCHOMIENIE CAŁEJ APLIKACJI I KLASTRA Z KODU
    - Podczas tworzenia kontenera KinD instalowany jest ArgoCD z powodu buga race-condition, gdzie CRD nie zostały jeszcze zainstalowane przed zasobami korzystających z nich.
    - Pozostałe elementy instalują się z aplikacji instalowanej do ArgoCD
# Opis działania poszczególnych skryptów:

## kind-create-cluster.cmd

Tworzy nowy klaster KinD oraz instaluje ArgoCD.

## apply-app.cmd

Ładuje zasób aplikacji ArgoCD do klastra.

## pfw-argocd.cmd

Uruchamia port forwarding dla ArgoCD. Przekierowanie 443->8080. https://localhost:8080

## pfw-jaeger.cmd

Uruchamia port forwarding dla Jaeger UI. Przekierowanie 16686->16686. https://localhost:16686

## argocd-pass.cmd

Odczytuje domyślne hasło dla ArgoCD.