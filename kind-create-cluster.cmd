kind create cluster --config kind-config.yaml

@REM Instalacja Contour
kubectl apply -f https://projectcontour.io/quickstart/contour.yaml
kubectl patch daemonsets -n projectcontour envoy -p {"""spec""":{"""template""":{"""spec""":{"""nodeSelector""":{"""ingress-ready""":"""true"""},"""tolerations""":[{"""key""":"""node-role.kubernetes.io/control-plane""","""operator""":"""Equal""","""effect""":"""NoSchedule"""},{"""key""":"""node-role.kubernetes.io/master""","""operator""":"""Equal""","""effect""":"""NoSchedule"""}]}}}}

@REM Instalacja ArgoCD
kubectl create namespace argocd
kubectl apply -n argocd -f https://raw.githubusercontent.com/argoproj/argo-cd/stable/manifests/install.yaml